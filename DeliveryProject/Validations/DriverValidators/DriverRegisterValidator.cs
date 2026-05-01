using DeliveryProject.DTOs.DriverDTOs;
using FluentValidation;

namespace DeliveryProject.Validations.DriverValidators
{
    public class DriverRegisterValidator : AbstractValidator<DriverRegisterDTO>
    {
        public DriverRegisterValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.").EmailAddress().WithMessage("Invalid email format.");
            
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.").MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
            
            RuleFor(x => x.LicenseNumber).NotEmpty().WithMessage("License Number is required.").MinimumLength(10).WithMessage("License Number must be at least 10 characters long.");
            
            RuleFor(x => x.VehicleType).NotEmpty().WithMessage("Vehicle Type is required.");

            RuleFor(x => x.VehicleStatus).NotEmpty().WithMessage("Vehicle Status is required.").MinimumLength(5).WithMessage("Vehicle Status must be at least 5 characters long.");

            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone Number is required.").Matches(@"^\d{10}$").WithMessage("Phone Number must be 10 digits.");

            RuleFor(x => x.VehicleType).Must(type => type == "Car" || type == "Motorcycle" || type == "Truck").WithMessage("Vehicle Type must be either 'Car', 'Motorcycle', or 'Truck'.");

        }
    }
}
