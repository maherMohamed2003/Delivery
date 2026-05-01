using DeliveryProject.DTOs.ShipmentsDTOs;
using FluentValidation;

namespace DeliveryProject.Validations.ShipmentValidators
{
    public class MakeShipmentValidator : AbstractValidator<MakeShipmentDTO>
    {
        public MakeShipmentValidator()
        {
            RuleFor(x => x.ReceiverName)
                .NotEmpty().WithMessage("Receiver name is required.")
                .MaximumLength(100).WithMessage("Receiver name cannot exceed 100 characters.");

            RuleFor(x => x.ReceiverAddress)
                .NotEmpty().WithMessage("Receiver address is required.")
                .MaximumLength(200).WithMessage("Receiver address cannot exceed 200 characters.");

            RuleFor(x => x.ReceiverPhone)
                .NotEmpty().WithMessage("Receiver phone is required.")
                .Matches(@"^\d{10}$").WithMessage("Receiver phone must be a valid phone number.");
            
           

            RuleFor(x => x.EGPAmount)
                .GreaterThan(0).WithMessage("EGP amount must be greater than zero.");
        }
    }
}
