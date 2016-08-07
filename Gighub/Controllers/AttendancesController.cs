﻿using Gighub.Dtos;
using Gighub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace Gighub.Controllers
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var attendeeId = User.Identity.GetUserId();

            if (_context.Attendances.Any(a => a.AttendeeId == attendeeId && a.GigId == dto.GigId))
            {
                return BadRequest("The attendance already exists");
            }

            var attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = attendeeId
            };
            _context.Attendances.Add(attendance);
            _context.SaveChangesAsync();

            return Ok();
        }
    }
}
