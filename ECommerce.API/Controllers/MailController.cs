using ECommerce.API.DTOs;
using ECommerce.API.ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailRepo _mailService;

        public MailController(IMailRepo mailService)
        {
            _mailService = mailService;
        }

        [HttpPost]
        public async Task<IActionResult> Send([FromBody] MailRequestDTO mailRequest)
        {
            try
            {
                // Validate the MailRequestDTO
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // If validation passes, send the email
                await _mailService.SendEmailAsync(mailRequest.ToEmail, mailRequest.Subject, mailRequest.Body);

                return Ok("Email sent successfully");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as required
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}