using DeliveryProject.Data;
using DeliveryProject.DTOs.ShipmentsDTOs;
using DeliveryProject.DTOs.ShipmentStatusDTOs;
using DeliveryProject.Models;
using Microsoft.EntityFrameworkCore;

namespace DeliveryProject.Repositories.ShipmentRepositories
{
    public class ShipmentRepo : IShipmentRepo
    {
        private readonly AppDbContext _context;

        public ShipmentRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<DisplayShipmentDetails>> GetAllShipmentsAsync()
        {
            var shipments = await _context.Shipment.Select(x => new DisplayShipmentDetails
            {
                Id = x.ShipmentId,
                SenderAddress = x.SenderAddress,
                SenderName = x.SenderName,
                SenderPhone = x.SenderPhone,
                ReceiverAddress = x.ReceiverAddress,
                ReceiverName = x.ReceiverName,
                ReceiverPhone = x.ReceiverPhone,
                CreateAt = x.CreateAt,
                DeliveredAt = x.DeliveredAt,
                ClientName = x.client.CompanyName,
                DriverName = x.driver.Name,
                EGPAmount = x.EGPAmount,
                NowStatus = x.shipmentStatuses.OrderByDescending(s => s.ChangeAt).Select(s => s.StatusValue).FirstOrDefault()
                
            }).ToListAsync();

            return shipments;
        }

        public Task<List<DisplayShipmentDetails>> GetAllShipmentsPending()
        {
            var shipments = _context.Shipment.Where(s => s.DriverID == null).Select(x => new DisplayShipmentDetails
            {
                Id = x.ShipmentId,
                SenderAddress = x.SenderAddress,
                SenderName = x.SenderName,
                SenderPhone = x.SenderPhone,
                ReceiverAddress = x.ReceiverAddress,
                ReceiverName = x.ReceiverName,
                ReceiverPhone = x.ReceiverPhone,
                CreateAt = x.CreateAt,
                DeliveredAt = x.DeliveredAt,
                ClientName = x.client.CompanyName,
                DriverName = x.driver.Name,
                EGPAmount = x.EGPAmount,
                NowStatus = x.shipmentStatuses.OrderByDescending(s => s.ChangeAt).Select(s => s.StatusValue).FirstOrDefault()
            }).ToListAsync();
            return shipments;
        }

        public async Task<DisplayShipmentDetails> GetShipmentByIdAsync(int id)
        {
            var shipment = await _context.Shipment.Where(x => x.ShipmentId == id).Select(x => new DisplayShipmentDetails
            {
                Id = x.ShipmentId,
                SenderAddress = x.SenderAddress,
                SenderName = x.SenderName,
                SenderPhone = x.SenderPhone,
                ReceiverAddress = x.ReceiverAddress,
                ReceiverName = x.ReceiverName,
                ReceiverPhone = x.ReceiverPhone,
                CreateAt = x.CreateAt,
                DeliveredAt = x.DeliveredAt,
                ClientName = x.client.CompanyName,
                DriverName = x.driver.Name,
                EGPAmount = x.EGPAmount,
                NowStatus = x.shipmentStatuses.OrderByDescending(s => s.ChangeAt).Select(s => s.StatusValue).FirstOrDefault()
            }).FirstOrDefaultAsync();
            if (shipment == null)
            {
                return null;
            }
            return shipment;

        }

        public async Task<List<DisplayShipmentStatusDTO>> GetShipmentHistoryAsync(int id)
        {
            var shipmentStatus = await _context.ShipmentStatus.Where(s => s.ShipmentID == id).Select(s => new DisplayShipmentStatusDTO
            {
                StatusID = s.StatusID,
                StatusValue = s.StatusValue,
                ChangeAt = s.ChangeAt,
                shipment = new DisplayShipmentDetails
                {
                    Id = s.shipment.ShipmentId,
                    ReceiverAddress = s.shipment.ReceiverAddress,
                    ReceiverName = s.shipment.ReceiverName,
                    ReceiverPhone = s.shipment.ReceiverPhone,
                    CreateAt = s.shipment.CreateAt,
                    DeliveredAt = s.shipment.DeliveredAt,
                    ClientName = s.shipment.client.CompanyName,
                    DriverName = s.shipment.driver.Name,
                    EGPAmount = s.shipment.EGPAmount,
                    SenderAddress = s.shipment.SenderAddress,
                    SenderName = s.shipment.SenderName,
                    SenderPhone = s.shipment.SenderPhone,
                    NowStatus = s.shipment.shipmentStatuses.OrderByDescending(st => st.ChangeAt).Select(st => st.StatusValue).FirstOrDefault()
                }
            }).ToListAsync ();
            if (shipmentStatus == null)
            {
                return null;
            }
            return shipmentStatus;
        }

        public async Task<DisplayShipmentDetails> MakeShipmentAsync(MakeShipmentDTO makeShipmentDTO)
        {

            var shipment = new Shipment
            {
                ReceiverName = makeShipmentDTO.ReceiverName,
                ReceiverAddress = makeShipmentDTO.ReceiverAddress,
                ReceiverPhone = makeShipmentDTO.ReceiverPhone,
                CreateAt = DateTime.Now,
                DeliveredAt = DateTime.Now.AddDays(4),
                ClientID = makeShipmentDTO.ClientID,
                EGPAmount = makeShipmentDTO.EGPAmount,
                SenderPhone = makeShipmentDTO.SenderPhone,
                SenderName = makeShipmentDTO.SenderName,
                SenderAddress = makeShipmentDTO.SenderAddress

            };
            await _context.Shipment.AddAsync(shipment);
            await _context.SaveChangesAsync();
            
            
            
            var shipmentStatus = new ShipmentStatus
            {
                StatusValue = "Pending",
                ShipmentID = shipment.ShipmentId,
                ChangeAt = DateTime.Now
            };
            await _context.ShipmentStatus.AddAsync(shipmentStatus);
            await _context.SaveChangesAsync();

            var res = await _context.Shipment.Where(x => x.ShipmentId == shipment.ShipmentId).Select(x => new DisplayShipmentDetails
            {
                Id = x.ShipmentId,
                SenderAddress = x.SenderAddress,
                SenderName = x.SenderName,
                SenderPhone = x.SenderPhone,
                ReceiverAddress = x.ReceiverAddress,
                ReceiverName = x.ReceiverName,
                ReceiverPhone = x.ReceiverPhone,
                CreateAt = x.CreateAt,
                DeliveredAt = x.DeliveredAt,
                ClientName = x.client.CompanyName,
                DriverName = x.driver.Name,
                EGPAmount = x.EGPAmount,
                NowStatus = "Pending"
            }).FirstOrDefaultAsync();

            return res;
       


        }

        public async Task<bool> RateTheShipmentAsync(int id,int rate)
        {
            var shipment = await _context.Shipment.FirstOrDefaultAsync(s => s.ShipmentId == id);
            if (shipment == null)
            {
                return false;
            }
            shipment.Rate = rate;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
