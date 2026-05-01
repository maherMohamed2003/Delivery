using DeliveryProject.DTOs.ClientDTOs;
using DeliveryProject.Repositories.AuthenticationRepositories;
using DeliveryProject.Repositories.ClientRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepo _repo;
        private readonly IAuthRepo _auth;

        public ClientController(IClientRepo repo, IAuthRepo auth)
        {
            _repo = repo;
            _auth = auth;
        }

        [HttpPost]
        [Route("RegisterAsClient")]
        public async Task<IActionResult> RegisterAsClient(AddClientDTO DTO)
        {
            var result = await _repo.AddNewClientAsync(DTO);
            if (result == null)
                return BadRequest("This Email Is Already Exists");
            await _auth.SendEmailAsync(result.Email, "Welcome In Our Community", "Aura Welcome Message");
            return Ok(result);
        }


        [HttpPost]
        [Route("LoginAsClient")]
        public async Task<IActionResult> LoginAsClient(ClientLoginDTO DTO)
        {
            var result = await _repo.LoginAsClientAsync(DTO);
            if (result == null)
                return BadRequest("Your Account Is Blocked");
            else
                return Ok(result);
        }

        [HttpPost]
        [Route("BlockClient/{id}")]
        public async Task<IActionResult> BlockClient(int id)
        {
            var result = await _repo.BlockedClientAsync(id);
            if (!result)
                return NotFound();
            var client = await _repo.GetClientByIdAsync(id);
            await _auth.SendEmailAsync(client.Email, "Your account has been blocked due to violation of our terms and conditions. Please contact support for more information.", "Account Blocked");

            return Ok("Blocked Successfully");
        }

        [HttpPost]
        [Route("UnBlockClient/{id}")]
        public async Task<IActionResult> UnBlockClient(int id)
        {
            var result = await _repo.UnBlockedClientAsync(id);
            if (!result)
                return NotFound();
            var client = await _repo.GetClientByIdAsync(id);
            await _auth.SendEmailAsync(client.Email, "Your Account Is Un Blocked Now", "Account Un Blocked");
            return Ok("UnBlocked Successfully");
        }

        [HttpGet]
        [Route("GetClientById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _repo.GetClientByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllClients")]
        public async Task<IActionResult> GetAllClients()
        {
            var result = await _repo.GetAllClientsAsync();
            if (result == null)
                return NotFound();
            return Ok(result);

        }

        [HttpPut]
        [Route("UpdateClientData")]
        public async Task<IActionResult> UpdateClientData(UpdateClientDTO DTO)
        {
            var result = await _repo.UpdateClientDataAsync(DTO);
            if (result == null)
                return NotFound("Client Not Found");
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteClient/{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var result = await _repo.DeleteClientAsync(id);
            if (!result)
                return NotFound("Client Not Found");
            return Ok("Deleted Successfully");

        }
    }
}
