using HostelFinder.Data;
using HostelFinder.Dtos;
using HostelFinder.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HostelFinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterStudent([FromBody] AddStudentDto student)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid input data", errors = ModelState });

            var existingStudent = await _context.Students.FindAsync(student.MobileNumber);
            if (existingStudent != null)
                return Conflict(new { message = "Student with this mobile number already exists." });

            var newStudent = new Student
            {
                MobileNumber = student.MobileNumber,
                StudentName = student.StudentName,
                GuardianName = student.GuardianName,
                State = student.State,
                District = student.District,
                Tehsil = student.Tehsil,
                Pincode = student.Pincode,
                PreparationFor = student.PreparationFor,
                Class = student.Class,
                CoachingInstitute = student.CoachingInstitute,
                Medium = student.Medium,
                HostelId = student.HostelId,
                SeatPreferenceCode = student.SeatPreferenceCode
            };

            _context.Students.Add(newStudent);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Student registered successfully." });
        }

        [Authorize]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var students = await _context.Students
                .Include(s => s.Hostel)
                .Select(s => new
                {
                    
                    s.StudentName,
                    s.MobileNumber,
                    s.GuardianName,
                    s.HostelId,
                    s.District,
                    s.Money,
                    HostelName = s.Hostel.HostelName,
                    s.SeatPreferenceCode,
                })
                .ToListAsync();

            return Ok(students);
        }


        [HttpPut("UpdateHostelInfo/{mobile}")]
        public async Task<IActionResult> UpdateStudentHostelInfo(string mobile, [FromBody] HostelAssignmentDto dto)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.MobileNumber == mobile);
            if (student == null)
                return NotFound(new { message = "Student not found." });

            var previousHostelId = student.HostelId;
            var previousSeatCode = student.SeatPreferenceCode;
            var previousMoney = student.Money;

            // VACATE CASE
            if (string.IsNullOrEmpty(dto.HostelId) && string.IsNullOrEmpty(dto.SeatPreferenceCode))
            {
                if (!string.IsNullOrEmpty(previousHostelId) && !string.IsNullOrEmpty(previousSeatCode))
                {
                    var oldHostel = await _context.Hostels.FirstOrDefaultAsync(h => h.HostelId == previousHostelId);
                    if (oldHostel != null) IncreaseSeat(oldHostel, previousSeatCode);
                }

                student.HostelId = null;
                student.SeatPreferenceCode = null;

                await _context.SaveChangesAsync();
                return Ok(new { message = "Student has been vacated from hostel." });
            }

            // HOSTEL SWITCH OR FIRST TIME ASSIGNMENT
            if (dto.HostelId != previousHostelId)
            {
                if (!string.IsNullOrEmpty(previousHostelId) && !string.IsNullOrEmpty(previousSeatCode))
                {
                    var oldHostel = await _context.Hostels.FirstOrDefaultAsync(h => h.HostelId == previousHostelId);
                    if (oldHostel != null) IncreaseSeat(oldHostel, previousSeatCode);
                }

                var newHostel = await _context.Hostels.FirstOrDefaultAsync(h => h.HostelId == dto.HostelId);
                if (newHostel == null) return NotFound("New hostel not found.");
                if (!DecreaseSeat(newHostel, dto.SeatPreferenceCode)) return BadRequest(new { message = "Selected seat not available in new hostel." });

                student.HostelId = dto.HostelId;
                student.SeatPreferenceCode = dto.SeatPreferenceCode;
                student.Money = dto.Money;

                await _context.SaveChangesAsync();
                return Ok(new { message = "Student reassigned to new hostel." });
            }

            // INTERNAL SEAT SWITCH IN SAME HOSTEL
            if (dto.SeatPreferenceCode != previousSeatCode)
            {
                var hostel = await _context.Hostels.FirstOrDefaultAsync(h => h.HostelId == dto.HostelId);
                if (hostel == null) return NotFound("Hostel not found.");
                if (!DecreaseSeat(hostel, dto.SeatPreferenceCode)) return BadRequest("New seat not available.");

                IncreaseSeat(hostel, previousSeatCode);
                student.SeatPreferenceCode = dto.SeatPreferenceCode;
                student.Money = dto.Money;
                await _context.SaveChangesAsync();
                return Ok(new { message = "Student seat preference changed." });
            }

            //only Money change or add
            if (dto.Money != previousMoney)
            {
                student.Money = dto.Money;
                await _context.SaveChangesAsync();
                return Ok(new { message = "Student Money amount changed" });

            }

                return Ok("No changes detected.");
        }

        //Helper function

        private bool DecreaseSeat(Hostel hostel, string code)
        {
            switch (code.ToUpper())
            {
                case "ASA": if (hostel.AvailableASA > 0) { hostel.AvailableASA--; return true; } break;
                case "ASN": if (hostel.AvailableASN > 0) { hostel.AvailableASN--; return true; } break;
                case "ADA": if (hostel.AvailableADA > 0) { hostel.AvailableADA--; return true; } break;
                case "ADN": if (hostel.AvailableADN > 0) { hostel.AvailableADN--; return true; } break;
                case "NSA": if (hostel.AvailableNSA > 0) { hostel.AvailableNSA--; return true; } break;
                case "NSN": if (hostel.AvailableNSN > 0) { hostel.AvailableNSN--; return true; } break;
                case "NDA": if (hostel.AvailableNDA > 0) { hostel.AvailableNDA--; return true; } break;
                case "NDN": if (hostel.AvailableNDN > 0) { hostel.AvailableNDN--; return true; } break;
            }
            return false;
        }

        private void IncreaseSeat(Hostel hostel, string code)
        {
            switch (code.ToUpper())
            {
                case "ASA": hostel.AvailableASA++; break;
                case "ASN": hostel.AvailableASN++; break;
                case "ADA": hostel.AvailableADA++; break;
                case "ADN": hostel.AvailableADN++; break;
                case "NSA": hostel.AvailableNSA++; break;
                case "NSN": hostel.AvailableNSN++; break;
                case "NDA": hostel.AvailableNDA++; break;
                case "NDN": hostel.AvailableNDN++; break;
            }
        }


    }
}