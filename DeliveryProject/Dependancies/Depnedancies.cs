using DeliveryProject.Data;
using DeliveryProject.Repositories.AuthenticationRepositories;
using DeliveryProject.Repositories.ClientRepositories;
using DeliveryProject.Repositories.DriverRepositories;
using DeliveryProject.Repositories.ShipmentRepositories;
using DeliveryProject.Validations.ClientValidators;
using DeliveryProject.Validations.DriverValidators;
using DeliveryProject.Validations.ShipmentValidators;
using FluentValidation.AspNetCore;

namespace DeliveryProject.Dependancies
{
    public static class Depnedancies
    {
        public static IServiceCollection AddDbContextFile(this IServiceCollection Service)
        {
            Service.AddDbContext<AppDbContext>();
            return Service;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection Service)
        {
            Service.AddScoped<IClientRepo, ClientRepo>();
            Service.AddScoped<IShipmentRepo, ShipmentRepo>();
            Service.AddScoped<IDriverRepo, DriverRepo>();
            Service.AddScoped<IAuthRepo, AuthRepo>();
            return Service;
        }

        public static IServiceCollection AddValidators(this IServiceCollection Services)
        {
           Services.AddControllers().AddFluentValidation(x => {
                x.RegisterValidatorsFromAssemblyContaining<AddNewClientValidator>();
                x.RegisterValidatorsFromAssemblyContaining<ClientLoginDTOValidator>();
                x.RegisterValidatorsFromAssemblyContaining<MakeShipmentValidator>();
               x.RegisterValidatorsFromAssemblyContaining<UpdateClientDataValidator>();
               x.RegisterValidatorsFromAssemblyContaining<RateShipmentValidator>();
               x.RegisterValidatorsFromAssemblyContaining<DriverRegisterValidator>();
               x.RegisterValidatorsFromAssemblyContaining<DriverLoginDTOValidator>();
           });
            return Services;
        }
    }
}