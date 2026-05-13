using DeliveryProject.DTOs.DriverDTOs;
using DeliveryProject.DTOs.NotificationDTOs;
using DeliveryProject.Repositories.DriverRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IDriverRepo _driverRepo;

        public DriverController(IDriverRepo driverRepo)
        {
            _driverRepo = driverRepo;
        }

        [HttpPost("register")]
        public async Task<ActionResult<DisplayDriverDTO>> RegisterAsDriver(DriverRegisterDTO driverRegisterDTO)
        {
            var result = await _driverRepo.RegisterAsDriver(driverRegisterDTO);
            if (result == null)
            {
                return BadRequest("Email already exists.");
            }
            return Ok(result);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(DriverLoginDTO login)
        {
            var result = await _driverRepo.Login(login);
            if (result == null)
            {
                return BadRequest("Invalid email or password.");
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("AssignShipment")]
        public async Task<IActionResult> AssignShipment(AssignShipmentDTO dto)
        {
            var result = await _driverRepo.AssignShipmentAsync(dto);
            if (result == null)
            {
                return NotFound("Shipment not found.");
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("DeliveredOrder/{shipmentId}")]
        public async Task<IActionResult> MarkShipmentAsDelivered(int shipmentId)
        {
            var result = await _driverRepo.MarkShipmentAsDeliveredAsync(shipmentId);
            if (result == null)
            {
                return NotFound("Shipment not found.");
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllDrivers")]
        public async Task<IActionResult> GetAllDrivers()
        {
            var result = await _driverRepo.GetAllDriversAsync();
            return Ok(result);
        }

        [HttpPost]
        [Route("BlockDriver/{id}")]
        public async Task<IActionResult> BlockDriver(int id)
        {
            var result = await _driverRepo.BlockDriver(id);
            if (!result)
            {
                return NotFound("Driver not found.");
            }
            return Ok("Driver blocked successfully.");
        }

        [HttpPost]
        [Route("UnBlockDriver/{id}")]
        public async Task<IActionResult> UnBlockDriver(int id)
        {
            var result = await _driverRepo.UnBlockDriver(id);
            if (!result)
            {
                return NotFound("Driver not found.");
            }
            return Ok("Driver unblocked successfully.");

        }

        [HttpGet]
        [Route("DisplayDriverNotifications/{id}")]
        public async Task<IActionResult> DisplayDriverNotifications(int id)
        {
            var result = await _driverRepo.DisplayNotificationsPerDriverAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetDriverById")]
        public async Task<IActionResult> GetDriverById(int id)
        {
            var result = await _driverRepo.GetDriverByIdAsync(id);
            if (result == null)
                return NotFound("Driver Is Not Exists");
            return Ok(result);
        }
        [HttpPut]
        [Route("UpdateDriver")]
        public async Task<IActionResult> UpdateDriver(DriverUpdateDTO dto)
        {
            var result = await _driverRepo.UpdateDriverAsync(dto);
            if (result == null)
                return NotFound("Driver Is Not Exists");
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteDriver/{id}")]
        public async Task<IActionResult> DeleteDriver(int id)
        {
            var result = await _driverRepo.DeleteDriverAsync(id);
            if (!result)
            {
                return NotFound("Driver not found.");
            }
            return Ok("Driver deleted successfully.");
        }

        [HttpGet]
        [Route("DrviersOverview")]
        public async Task<IActionResult> DriversOverview()
        {
            var result = await _driverRepo.DisplayDriversOverviewAsync();
            return Ok(result);
        }

        [HttpPost]
        [Route("SendNotification")]
        public async Task<IActionResult> SendNotification(SendNotificationDTO dto)
        {
            var result = await _driverRepo.SendNotificationAsync(dto);
            if (result == null)
            {
                return BadRequest("Failed to send notification.");
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("GetOneDriverOverview/{driverId}")]
        public async Task<IActionResult> GetOneDriverOverview(int driverId)
        {
            var result = await _driverRepo.GetOneDriverOverviewAsync(driverId);
            if (result == null)
            {
                return NotFound("Driver not found.");
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("GetDriverShipments/{driverId}")]
        public async Task<IActionResult> GetDriverShipments(int driverId)
        {
            var result = await _driverRepo.GetDriverShipmentsAsync(driverId);
            if (result == null)
            {
                return NotFound("Driver not found.");
            }
            return Ok(result);
        }

        }
    }