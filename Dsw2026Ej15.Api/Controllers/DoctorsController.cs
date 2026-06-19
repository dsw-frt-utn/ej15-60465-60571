using Dsw2026Ej15.Api.Dtos;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Controllers;

[ApiController]
[Route("api/doctors")]
public class DoctorsController : ControllerBase
{
    private readonly IPersistence _persistence;

    public DoctorsController(IPersistence persistence)
    {
        _persistence = persistence;
    }

    // POST: api/doctors
    [HttpPost]
    public IActionResult CreateDoctor(DoctorRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ValidationException("Name es requerido.");

        if (string.IsNullOrWhiteSpace(request.LicenseNumber))
            throw new ValidationException("LicenseNumber es requerido.");

        var speciality = _persistence.GetSpecialityById(request.SpecialityId);

        if (speciality == null)
            throw new ValidationException("La especialidad no existe.");

        var doctor = new Doctor
        {
            Name = request.Name,
            LicenseNumber = request.LicenseNumber,
            IsActive = true,
            Speciality = speciality
        };

        _persistence.AddDoctor(doctor);

        return CreatedAtAction(
            nameof(GetDoctorById),
            new { id = doctor.Id },
            doctor);
    }

    // GET: api/doctors
    [HttpGet]
    public IActionResult GetActiveDoctors()
    {
        var doctors = _persistence.GetDoctors()
            .Where(d => d.IsActive)
            .Select(d => new
            {
                d.Id,
                d.Name,
                d.LicenseNumber,
                SpecialityName = d.Speciality.Name
            })
            .ToList();

        return Ok(doctors);
    }

    // GET: api/doctors/{id}
    [HttpGet("{id}")]
    public IActionResult GetDoctorById(Guid id)
    {
        var doctor = _persistence.GetDoctorById(id);

        if (doctor == null || !doctor.IsActive)
            return NotFound();

        return Ok(new
        {
            doctor.Name,
            doctor.LicenseNumber,
            SpecialityName = doctor.Speciality.Name
        });
    }

    // DELETE: api/doctors/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteDoctor(Guid id)
    {
        var doctor = _persistence.GetDoctorById(id);

        if (doctor == null || !doctor.IsActive)
            return NotFound();

        doctor.IsActive = false;

        return NoContent();
    }
}