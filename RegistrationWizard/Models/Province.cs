using System.Text.Json.Serialization;

namespace RegistrationWizard.Models
{
    public class Province
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CountryId { get; set; }
        [JsonIgnore]
        public Country Country { get; set; }
    }
}
