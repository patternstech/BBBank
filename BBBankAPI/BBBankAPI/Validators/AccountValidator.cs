using Entites;
using Entites.RequestModels;
using FluentValidation;
using Infrastructure.Contracts;

namespace BBBankAPI.Validators
{
    public class AccountValidator : AbstractValidator<OpenAccountRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AccountValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //        RuleFor(x => x.AccountNumber).NotEmpty();

            //        RuleFor(account => account.CurrentBalance)
            //.GreaterThan(0)
            //.When(account => account.AccountStatus == AccountStatus.Active)
            //.WithMessage("Current balance must be greater than 0 when the account is active.");

            RuleFor(x => x).Custom(async (dto, context) =>
            {
                var accountExists = _unitOfWork.AccountRepository.Exists(x => x.AccountNumber == dto.AccountNumber);
                if (accountExists.Result)
                {
                    context.AddFailure("Account with this Account Number already Exists");
                }

            });
        }
    }
}
