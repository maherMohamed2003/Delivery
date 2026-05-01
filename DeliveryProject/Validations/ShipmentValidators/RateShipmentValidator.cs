using DeliveryProject.DTOs.ShipmentsDTOs;
using FluentValidation;

namespace DeliveryProject.Validations.ShipmentValidators
{
    public class RateShipmentValidator :AbstractValidator<RateShipmentDTO>
    {
        public RateShipmentValidator()
        {
            RuleFor(x => x.ShipmentId).GreaterThan(0).WithMessage("Shipment ID must be greater than 0.");
            RuleFor(x => x.Rating).InclusiveBetween(1, 5).WithMessage("Rate must be between 1 and 5.");
        }
    }
}
