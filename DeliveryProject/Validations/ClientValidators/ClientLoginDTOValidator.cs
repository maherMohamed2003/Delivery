using DeliveryProject.DTOs.ClientDTOs;
using FluentValidation;

namespace DeliveryProject.Validations.ClientValidators
{
    public class ClientLoginDTOValidator : AbstractValidator<ClientLoginDTO>
    {
        public ClientLoginDTOValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}
