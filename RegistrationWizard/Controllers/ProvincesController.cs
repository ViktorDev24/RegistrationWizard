using Microsoft.AspNetCore.Mvc;
using RegistrationWizard.Interfaces.IService;
using RegistrationWizard.Models;

namespace RegistrationWizard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProvincesController(IProvinceService provinceService) : ControllerBase
    {
        private readonly IProvinceService _provinceService = provinceService;

        [HttpGet("{countryId}")]
        public async Task<ActionResult<IEnumerable<Province>>> GetProvinces(int countryId)
        {
            var provinces = await _provinceService.GetProvincesByCountryIdAsync(countryId);
            if (provinces == null || !provinces.Any())
            {
                return NotFound();
            }
            return Ok(provinces);
        }
    }
}
