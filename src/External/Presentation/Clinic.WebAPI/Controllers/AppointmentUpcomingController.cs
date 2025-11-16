using System;
using System.Linq;
using System.Security.Claims;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Clinic.WebAPI.Controllers;

/// <summary>
///     OData Controller for Appointment Upcoming feature
/// </summary>
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("odata/[controller]")]
public class AppointmentUpcomingController : ODataController
{
    private readonly IUnitOfWork _unitOfWork;

    public AppointmentUpcomingController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    ///     Get upcoming appointments with OData query support
    /// </summary>
    [HttpGet]
    [EnableQuery(MaxTop = 100, AllowedQueryOptions = AllowedQueryOptions.All)]
    public IActionResult Get()
    {
        // Get userId from JWT token
        var userId = Guid.Parse(
            User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub)
        );

        // Check role
        var role = User.FindFirstValue(claimType: "role");
        if (!role.Equals("user"))
        {
            return Unauthorized(new { Message = "Role is not user" });
        }

        // Get queryable appointments
        var appointments = _unitOfWork.GetAppointmentUpcomingRepository
            .GetAppointmentsQueryable(userId);

        return Ok(appointments);
    }

    /// <summary>
    ///     Get single appointment by ID
    /// </summary>
    [HttpGet("{key}")]
    [EnableQuery]
    public IActionResult Get([FromRoute] Guid key)
    {
        // Get userId from JWT token
        var userId = Guid.Parse(
            User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub)
        );

        // Check role
        var role = User.FindFirstValue(claimType: "role");
        if (!role.Equals("user"))
        {
            return Unauthorized(new { Message = "Role is not user" });
        }

        // Get specific appointment
        var appointment = _unitOfWork.GetAppointmentUpcomingRepository
            .GetAppointmentsQueryable(userId)
            .FirstOrDefault(a => a.Id == key);

        if (appointment == null)
        {
            return NotFound();
        }

        return Ok(appointment);
    }
}
