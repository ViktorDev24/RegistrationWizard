using Microsoft.AspNetCore.Mvc;
using RegistrationWizard.Interfaces.IService;
using RegistrationWizard.Models.DTOs;

[Route("api/[controller]")]
[ApiController]
public class RegistrationController(IRegistrationService registrationService) : ControllerBase
{
    private readonly IRegistrationService _registrationService = registrationService;

    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromBody] RegistrationDto registrationDto)
    {
        var result = await _registrationService.RegisterUserAsync(registrationDto);

        if (!result.Success)
        {
            return BadRequest(result.Errors);
        }

        return Ok(result);
    }
}
