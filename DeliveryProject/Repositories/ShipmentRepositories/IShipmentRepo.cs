using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryProject.DTOs.ShipmentsDTOs;
using DeliveryProject.DTOs.ShipmentStatusDTOs;

namespace DeliveryProject.Repositories.ShipmentRepositories
{
    public interface IShipmentRepo
    {
        public Task<DisplayShipmentDetails> MakeShipmentAsync(MakeShipmentDTO makeShipmentDTO);
        public Task<List<DisplayShipmentDetails>> GetAllShipmentsAsync();
        public Task<DisplayShipmentDetails> GetShipmentByIdAsync(int id);
        public Task<List<DisplayShipmentStatusDTO>> GetShipmentHistoryAsync(int id);
        public Task<List<DisplayShipmentDetails>> GetAllShipmentsPending();
        public Task<bool> RateTheShipmentAsync(int id, int rate);
        public Task<ShipmentOverviewDTO> ShipmentOverviewAsync();
        public Task<List<DisplayShipmentDetails>> GetRecent5ShipmentsAsync();


    }

}
