using DeliveryProject.DTOs.ShipmentsDTOs;
using DeliveryProject.Repositories.ShipmentRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentRepo _repo;

        public ShipmentController(IShipmentRepo repo)
        {
            _repo = repo;
        }

        [HttpPost]
        [Route("MakeShipment")]
        public async Task<IActionResult> MakeShipment(MakeShipmentDTO makeShipmentDTO)
        {
            var result = await _repo.MakeShipmentAsync(makeShipmentDTO);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllShipments")]
        public async Task<IActionResult> GetAllShipments()
        {
            var result = await _repo.GetAllShipmentsAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetShipmentbyId/{id}")]
        public async Task<IActionResult> GetShipmentById(int id)
        {
            var result = await _repo.GetShipmentByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("GetShipmentHistory/{id}")]
        public async Task<IActionResult> GetShipmentHistory(int id)
        {
            var result = await _repo.GetShipmentHistoryAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllShipmentsPending")]
        public async Task<IActionResult> GetAllShipmentsPending()
        {
            var result = await _repo.GetAllShipmentsPending();
            return Ok(result);
        }

        [HttpPut]
        [Route("RateShipment/")]
        public async Task<IActionResult> RateShipment(RateShipmentDTO rateShipmentDTO)
        {
            var result = await _repo.RateTheShipmentAsync(rateShipmentDTO.ShipmentId, rateShipmentDTO.Rating);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}