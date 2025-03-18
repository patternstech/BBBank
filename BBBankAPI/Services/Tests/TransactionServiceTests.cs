using Entites;
using Entites.RequestModels;
using Infrastructure.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Tests
{
    [TestFixture]
    public class TransactionServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IHubContext<UpdateHub>> _hubContextMock;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private Mock<IRulesEngineService> _rulesEngineServiceMock;
        private Mock<IOptionsSnapshot<Settings>> _settingsMock;
        private TransactionService _transactionService;
        private Mock<IHubClients> _mockClients;
        private Mock<IClientProxy> _mockClientProxy;
        private Mock<INotificationService> _mockNotificationService;
        private Mock<FeatureService> _mockFeatureService;
        [SetUp]
        public void SetUp()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _hubContextMock = new Mock<IHubContext<UpdateHub>>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _rulesEngineServiceMock = new Mock<IRulesEngineService>();
            _settingsMock = new Mock<IOptionsSnapshot<Settings>>();
            _settingsMock.Setup(s => s.Value).Returns(new Settings());

            _mockClients = new Mock<IHubClients>();
            _mockClientProxy = new Mock<IClientProxy>();
            _mockClients.Setup(c => c.User(It.IsAny<string>())).Returns(_mockClientProxy.Object);
            _hubContextMock.Setup(h => h.Clients).Returns(_mockClients.Object);

            _transactionService = new TransactionService(
    _hubContextMock.Object,
    _unitOfWorkMock.Object,
    _settingsMock.Object,
    _httpContextAccessorMock.Object,
    _rulesEngineServiceMock.Object,
    _mockNotificationService.Object,
    _mockFeatureService.Object
);

        }

        [Test]
        public async Task GetLast12MonthBalances_NoTransactions_ReturnsEmptyGraph()
        {
            //Arrange
            _unitOfWorkMock.Setup(u => u.TransactionRepository.FindAllAsync(It.IsAny<Expression<Func<Transaction, bool>>>(), null))
    .ReturnsAsync(new List<Transaction>());

            //Act
            var result = await _transactionService.GetLast12MonthBalances("userId");

            //Assert
            Assert.That(result.TotalBalance, Is.EqualTo(0));
            Assert.That(result.Figures.Count, Is.EqualTo(0));

        }
        [Test]
        public async Task TransferFunds_ValidAccounts_TransfersFundsCorrectly()
        {
            //Arrange
            var sender = new Account { AccountNumber = "123", CurrentBalance = 1000, Transactions = new List<Transaction>() };
            var receiver = new Account { AccountNumber = "456", Transactions = new List<Transaction>() };
            var transferRequest = new TransferRequest { SenderAccountNumber = "123", ReceiverAccountNumber = "456", TransferAmount = 200 };

            _unitOfWorkMock.Setup(u => u.AccountRepository.FindAsync(It.IsAny<Expression<Func<Account, bool>>>(), "Transactions"))
    .ReturnsAsync((Expression<Func<Account, bool>> predicate, string _) =>
    {
        var compiledPredicate = predicate.Compile();
        return compiledPredicate(sender) ? sender : receiver;
    });
            _rulesEngineServiceMock.Setup(r => r.ValidateTransferRules(It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<int>()))
    .Returns(Task.CompletedTask);

            //Act
            await _transactionService.TransferFunds(transferRequest);

            //Assert

            Assert.That(sender.Transactions.Count, Is.EqualTo(1));
            Assert.That(sender.Transactions.ToList()[0].TransactionAmount, Is.EqualTo(-200));
            Assert.That(receiver.Transactions.Count, Is.EqualTo(1));
            Assert.That(receiver.Transactions.ToList()[0].TransactionAmount, Is.EqualTo(200));

            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);


        }

        [Test]
        public void TransferFunds_InvalidAccounts_ThrowsException()
        {
            var transferRequest = new TransferRequest { SenderAccountNumber = "123", ReceiverAccountNumber = "456", TransferAmount = 200 };

            _unitOfWorkMock.Setup(u => u.AccountRepository.FindAsync(It.IsAny<Expression<Func<Account, bool>>>(), "Transactions"))
    .ReturnsAsync((Account)null);

            Assert.ThrowsAsync<NullReferenceException>(() => _transactionService.TransferFunds(transferRequest));



        }


        [Test]
        public async Task GetAllTransactions_NoUserId_ReturnsAllTransactions()
        {
            // Arrange
            var transactions = new List<Transaction>
        {
            new Transaction { TransactionAmount = 300, TransactionDate = DateTime.UtcNow }
        };

            _unitOfWorkMock.Setup(u => u.TransactionRepository.GetAllAsync())
                .ReturnsAsync(transactions);

            // Act
            var result = await _transactionService.GetAllTransactions(null);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task GetAllTransactions_UserId_ReturnsUserTransactions()
        {
            // Arrange
            var transactions = new List<Transaction>
        {
            new Transaction { TransactionAmount = 100, TransactionDate = DateTime.UtcNow },
            new Transaction { TransactionAmount = 200, TransactionDate = DateTime.UtcNow }
        };

            _unitOfWorkMock.Setup(u => u.TransactionRepository.FindAllAsync(It.IsAny<Expression<Func<Transaction, bool>>>(), null))
                .ReturnsAsync(transactions);

            // Act
            var result = await _transactionService.GetAllTransactions("userId");

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task DepositFunds_ValidAccount_AddsTransaction()
        {
            // Arrange
            var account = new Account { AccountNumber = "123", Transactions = new List<Transaction>() };
            var depositRequest = new DepositRequest { AccountNumber = "123", Amount = 500 };

            _unitOfWorkMock.Setup(u => u.AccountRepository.FindAsync(It.IsAny<Expression<Func<Account, bool>>>(), "Transactions"))
                .ReturnsAsync(account);
        _mockNotificationService.Setup(u => u.SendEmailNotification(It.IsAny<EmailNotificationDto>()))
    .Returns(Task.CompletedTask);

            // Act
            await _transactionService.DepositFunds(depositRequest);

            // Assert
            Assert.That(account.Transactions.Count, Is.EqualTo(1));
            Assert.That(account.Transactions.ToList()[0].TransactionAmount, Is.EqualTo(500));

            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        }


    }

}
