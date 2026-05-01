using DeliveryProject.DTOs.ClientDTOs;
using FluentValidation;

namespace DeliveryProject.Validations.ClientValidators
{
    public class AddNewClientValidator : AbstractValidator<AddClientDTO>
    {
        public AddNewClientValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email Is Required").EmailAddress();

            RuleFor(x => x.CompanyName).NotEmpty().WithMessage("Company Name Is Required");
                
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password Is Required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long");
            
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone Is Required")
                .Matches(@"^\d{10}$").WithMessage("Phone number must be 10 digits");
        }
    }
}
