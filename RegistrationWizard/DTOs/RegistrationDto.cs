namespace RegistrationWizard.Models.DTOs
{
    public class RegistrationDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool AgreeToTerms { get; set; }
        public int CountryId { get; set; }
        public int ProvinceId { get; set; }
    }
}
