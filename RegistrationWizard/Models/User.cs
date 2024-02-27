using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace RegistrationWizard.Models
{
    public class User: IdentityUser
    {
        [Required]
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        public int? ProvinceId { get; set; }
        public virtual Province Province { get; set; }
    }
}
