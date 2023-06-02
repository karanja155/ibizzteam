﻿using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Saas.Admin.Service.Data;
using Saas.SignupAdministration.Web.Models;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Security.Claims;

namespace Saas.Admin.Service.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SadUserController : ControllerBase
{
    private readonly ISadUserService _sadUserService;
    private readonly IAdminGraphServices _graphservices;

    public SadUserController(ISadUserService sadUserService, IAdminGraphServices graphservices)
    {
        _sadUserService = sadUserService;
        _graphservices = graphservices;
    }

    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    //[ProducesResponseType(typeof(SadUser), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> OnboardTen(SadUser admin)
    {
        try
        {
            if (!ModelState.IsValid)
                throw new Exception("Error processing you request");

<<<<<<< Updated upstream
            SadUser user = await getUserinfo();
=======
            SadUser user = await getUserinfo(admin);
>>>>>>> Stashed changes

            user = await _sadUserService.AddSadUser(user, 0);

            return Ok(/*new { userId = createdUser.Id, createdUser, message = "success" }*/);
            //return CreatedAtAction("dashboard", new { userId = createdUser.Id }, createdUser);

        }
        catch (DbUpdateException ex)
        {
            return BadRequest((ex.InnerException ?? ex).Message);
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Update user information from graph
    /// </summary>
<<<<<<< Updated upstream
    private async Task<SadUser> getUserinfo()
=======
    private async Task<SadUser> getUserinfo(SadUser admin)
>>>>>>> Stashed changes
    {
        string email = User.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;

        if (string.IsNullOrEmpty(email))
        {
            throw new ArgumentNullException("User principal name does not exists");
        }

        SadUser user =await  _graphservices.GetUser(email);

<<<<<<< Updated upstream
        //Add user terminal before exiting
        user.Terminus = HttpContext.Connection.RemoteIpAddress?.ToString()??"Not captured";

        return user;
=======
        admin.FullNames = user.FullNames;
        admin.Email = user.Email;
        admin.Telephone = user.Telephone;

        //Add user terminal before exiting
        admin.Terminus = HttpContext.Connection.RemoteIpAddress?.ToString()??"Not captured";

        return admin;
>>>>>>> Stashed changes

    }
}
