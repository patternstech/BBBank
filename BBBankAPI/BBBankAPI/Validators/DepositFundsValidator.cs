using Entites;
using Entites.RequestModels;
using FluentValidation;
using Infrastructure.Contracts;

namespace BBBankAPI.Validators
{
    public class DepositFundsValidator : AbstractValidator<DepositRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DepositFundsValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(x => x).Custom((dto, context) =>
            {
                try
                {

                    var accountExists = _unitOfWork.AccountRepository.Exists(x => x.AccountNumber == dto.AccountNumber);
                    if (!accountExists.Result)
                    {
                        context.AddFailure("Account with this Account Number does not Exists");
                    }
                    var account = _unitOfWork.AccountRepository.FindAsync(x => x.AccountNumber == dto.AccountNumber);
                    if (account.Result.AccountStatus == AccountStatus.InActive)
                    {
                        context.AddFailure("Cannot Desposit in InActive Account.");
                    }

                }
                catch (Exception ex)
                {

                    context.AddFailure(ex.Message);
                }


            });

        }
    }
}
