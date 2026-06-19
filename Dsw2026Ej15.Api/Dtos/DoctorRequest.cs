using Dsw2026Ej15.Api.Dtos;

 namespace Dsw2026Ej15.Api.Dtos
{
    public class DoctorRequest
    {
        public string Name { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public Guid SpecialityId { get; set; }
    }
}