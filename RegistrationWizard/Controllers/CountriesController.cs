using Microsoft.AspNetCore.Mvc;
using RegistrationWizard.Interfaces.IService; 

namespace RegistrationWizard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController(ICountryService countryService) : ControllerBase
    {
        private readonly ICountryService _countryService = countryService;

        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            var countries = await _countryService.GetAllCountriesAsync();
            if (countries == null) return NotFound();
            return Ok(countries);
        }
    }
}
