namespace RegistrationWizard.DTOs
{
    public class RegistrationResult
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }

        public RegistrationResult()
        {
            Errors = new List<string>();
        }
    }
}
