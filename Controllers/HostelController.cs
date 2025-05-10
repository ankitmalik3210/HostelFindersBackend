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
    public class HostelController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HostelController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> AddHostel([FromBody] AddHostelDto hostelDto)
        {
            if (hostelDto == null)
            {
                return BadRequest("Hostel data is invalid.");
            }

            // Check if the HostelId already exists
            var existingHostel = await _context.Hostels
                .FirstOrDefaultAsync(h => h.HostelId == hostelDto.HostelId);

            if (existingHostel != null)
            {
                return Conflict(new { message = "Hostel with this id already exists" });
            }

            // Map the DTO to the Hostel model
            var hostel = new Hostel
            {
                HostelId = hostelDto.HostelId,
                HostelName = hostelDto.HostelName,
                ManagerName = hostelDto.ManagerName,
                MobileNumber = hostelDto.MobileNumber,
                ASA = hostelDto.ASA,
                ASN = hostelDto.ASN,
                ADA = hostelDto.ADA,
                ADN = hostelDto.ADN,
                NSA = hostelDto.NSA,
                NSN = hostelDto.NSN,
                NDA = hostelDto.NDA,
                NDN = hostelDto.NDN,
                ASAPRICE = hostelDto.ASAPRICE,
                ASNPRICE = hostelDto.ASNPRICE,
                ADAPRICE = hostelDto.ADAPRICE,
                ADNPRICE = hostelDto.ADNPRICE,
                NSAPRICE = hostelDto.NSAPRICE,
                NSNPRICE = hostelDto.NSNPRICE,
                NDAPRICE = hostelDto.NDAPRICE,
                NDNPRICE = hostelDto.NDNPRICE,

                // Automatically set Available seats equal to total seats
                AvailableASA = hostelDto.ASA,
                AvailableASN = hostelDto.ASN,
                AvailableADA = hostelDto.ADA,
                AvailableADN = hostelDto.ADN,
                AvailableNSA = hostelDto.NSA,
                AvailableNSN = hostelDto.NSN,
                AvailableNDA = hostelDto.NDA,
                AvailableNDN = hostelDto.NDN
            };

            // Add hostel to database
            _context.Hostels.Add(hostel);
            await _context.SaveChangesAsync();

            // Return the newly created hostel
            return Ok(new { message = "Hostel Registered Successfully" });
        }

        [Authorize]
        [HttpGet("FilterHostels")]
        public async Task<IActionResult> FilterHostels([FromQuery] string state, [FromQuery] string? district = null)
        {
            if (string.IsNullOrWhiteSpace(state))
                return BadRequest("State is required.");

            var normalizedState = state.Trim().ToLower();
            var normalizedDistrict = district?.Trim().ToLower();

            var filteredStudents = _context.Students
                .Where(s => s.State.ToLower() == normalizedState);

            if (!string.IsNullOrEmpty(normalizedDistrict))
            {
                filteredStudents = filteredStudents
                    .Where(s => s.District.ToLower() == normalizedDistrict);
            }

            // Step 1: Get hostelId → student count mapping
            var hostelStudentCounts = await filteredStudents
                .GroupBy(s => s.HostelId)
                .Select(g => new
                {
                    HostelId = g.Key,
                    NumberOfStudents = g.Count()
                })
                .ToListAsync();

            // Create a dictionary for fast lookup
            var hostelCountMap = hostelStudentCounts
                .ToDictionary(x => x.HostelId, x => x.NumberOfStudents);

            // Step 2: Fetch hostels that are in the above list
            var hostelIds = hostelStudentCounts.Select(x => x.HostelId).ToList();

            var hostels = await _context.Hostels
                .Where(h => hostelIds.Contains(h.HostelId))
                .ToListAsync();

            // Step 3: Project to DTOs in memory
            var result = hostels
                .Select(h => new HostelFilterDto
                {
                    HostelId = h.HostelId,
                    HostelName = h.HostelName,
                    ManagerName = h.ManagerName,
                    ManagerMobile = h.MobileNumber,
                    AvailableRooms = new Dictionary<string, RoomInfoDto>
{
    { "ASA", new RoomInfoDto { Available = h.AvailableASA, Price = h.ASAPRICE } },
    { "ASN", new RoomInfoDto { Available = h.AvailableASN, Price = h.ASNPRICE } },
    { "ADA", new RoomInfoDto { Available = h.AvailableADA, Price = h.ADAPRICE } },
    { "ADN", new RoomInfoDto { Available = h.AvailableADN, Price = h.ADNPRICE } },
    { "NSA", new RoomInfoDto { Available = h.AvailableNSA, Price = h.NSAPRICE } },
    { "NSN", new RoomInfoDto { Available = h.AvailableNSN, Price = h.NSNPRICE } },
    { "NDA", new RoomInfoDto { Available = h.AvailableNDA, Price = h.NDAPRICE } },
    { "NDN", new RoomInfoDto { Available = h.AvailableNDN, Price = h.NDNPRICE } },
},
                    NumberOfStudents = hostelCountMap.ContainsKey(h.HostelId) ? hostelCountMap[h.HostelId] : 0
                })
                .OrderByDescending(h => h.NumberOfStudents)
                .ToList();

            return Ok(result);
        }

        


    }
}
