using DeliveryProject.DTOs.ClientDTOs;
using FluentValidation;

namespace DeliveryProject.Validations.ClientValidators
{
    public class UpdateClientDataValidator : AbstractValidator<UpdateClientDTO>
    {
        public UpdateClientDataValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Client ID is required.");
            RuleFor(x => x.CompanyName).NotEmpty().WithMessage("Company Name is required.")
                                       .MaximumLength(100).WithMessage("Company Name cannot exceed 100 characters.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.")
                                 .EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone number is required.")
                                 .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid phone number format.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.")
                                 .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        }
    }
}
