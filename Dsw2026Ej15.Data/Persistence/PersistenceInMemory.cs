using System.Text.Json;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
namespace Dsw2026Ej15.Data;

public class PersistenceInMemory : IPersistence
{
    private List<Speciality> _specialities = [];
    private List<Doctor> _doctors = [];

    public PersistenceInMemory()
    {
        LoadSpecialities();
    }

    private void LoadSpecialities()
    {
        try
        {
            string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "Sources", "specialities.json");
            var json = File.ReadAllText(jsonPath);

            var specialities = JsonSerializer.Deserialize<List<Speciality>>(json,
                new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                }) ?? [];

            _specialities = [.. specialities];
        }
        catch (Exception)
        {

        }
    }

    public List<Doctor> Doctors => _doctors;

    public List<Speciality> Specialities => _specialities;

    public Doctor AddDoctor(Doctor doctor)
    {
        _doctors.Add(doctor);
        return doctor;
    }

    public List<Doctor> GetDoctors()
    {
        return _doctors;
    }

    public Doctor? GetDoctorById(Guid id)
    {
        return _doctors.FirstOrDefault(d => d.Id == id);
    }

    public Speciality? GetSpecialityById(Guid id)
    {
        return _specialities.FirstOrDefault(s => s.Id == id);
    }
}
