using CESIZen.Shared.Data;
using CESIZen.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.DTO.CESIZen.Shared.DTOs;
using Shared.Models;
using Api.Services;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AuthController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly IEmailService _emailService;

        public AuthController(AppDBContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }


        [HttpPost("register")] // url = api/auth/register
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.email == request.Email);
            if (existingUser != null)
            {
                var existingProfile = await _context.Profiles.FirstOrDefaultAsync(p => p.id_user == existingUser.id_user);
                var existingUsername = existingProfile?.username ?? "Utilisateur";
                
                try
                {
                    await _emailService.SendMailUsed(existingUser.email, existingUsername);
                }
                catch (Exception ex)
                {
                    return StatusCode(201, new { message = "Compte créé, mais l'envoi de l'email de confirmation a échoué : " + ex.Message });
                }
                return StatusCode(201, new { message = "Utilisateur créé avec succès" });
            }

            // hash the password using BCrypt
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            string validationToken = Guid.NewGuid().ToString();

            var newUser = new User 
            {
                firstname = request.Firstname,
                lastname = request.Lastname,
                email = request.Email,
                password = passwordHash,
                birthday = request.Birthday,
                id_role = 2,
                statut = AcountStatut.Waiting,
                validation_token = validationToken,
                validation_token_expires_at = DateTime.UtcNow.AddHours(1),
                created_at = DateTime.UtcNow
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            var newProfile = new Profile
            {
                id_user = newUser.id_user, // link the profile to the newly created user
                username = request.Username,
                avatar = null,
                gender = request.Gender,
                created_at = DateTime.UtcNow,
            };

            _context.Profiles.Add(newProfile);
            await _context.SaveChangesAsync();

            try
            {
                await _emailService.SendConfirmationMail(newUser.email, newProfile.username, validationToken);
            }
            catch (Exception ex)
            {
                return StatusCode(201, new { message = "Compte créé, mais l'envoi de l'email de confirmation a échoué : " + ex.Message });
            }

            return StatusCode(201, new { message = "Utilisateur créé avec succès" });
        }

        [HttpGet("verify")] // GET api/auth/verify?token={token}
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Verify([FromQuery] string token)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.validation_token == token);

            if (user == null || user.validation_token_expires_at < DateTime.UtcNow)
            {
                return BadRequest(new { message = "Le jeton de validation est invalide ou a expiré." });
            }

            user.statut = AcountStatut.Active;

            user.validation_token = null;
            user.validation_token_expires_at = null;
            user.updated_at = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Votre compte a été activé avec succès ! Vous pouvez maintenant vous connecter." });
        }
    }
}
