using DeliveryProject.DTOs.DriverDTOs;
using FluentValidation;

namespace DeliveryProject.Validations.DriverValidators
{
    public class DriverLoginDTOValidator : AbstractValidator<DriverLoginDTO>
    {
        public DriverLoginDTOValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.").EmailAddress().WithMessage("Invalid email format.");
                
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.").MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        }
    }
}
