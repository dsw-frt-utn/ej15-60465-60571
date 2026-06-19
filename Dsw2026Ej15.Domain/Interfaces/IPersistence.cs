using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Domain.Interfaces;

public interface IPersistence
{
    List<Doctor> Doctors { get; }

    List<Speciality> Specialities { get; }

    Doctor AddDoctor(Doctor doctor);

    List<Doctor> GetDoctors();

    Doctor? GetDoctorById(Guid id);

    Speciality? GetSpecialityById(Guid id);
}