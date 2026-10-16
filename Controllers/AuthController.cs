using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using RaceDay_POE.Models;
using Microsoft.IdentityModel.Tokens;

namespace RaceDay_POE.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly string string_connection = @"Data source=(localdb)\api_endpoint;DATABASE=RaceDayDB;";

        public class RegisterDto
        {
            public string? UserType { get; set; }
            public string? Name { get; set; }
            public string? Email { get; set; }
            public string? Password { get; set; }
            public string? Description { get; set; }
            public string? Gender { get; set; }
        }

        public class LoginDto
        {
            public string? Email { get; set; }
            public string? Password { get; set; }
            public string? UserType { get; set; }
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto dto)
        {
            try
            {
                using SqlConnection connect = new SqlConnection(string_connection);
                connect.Open();

                // Check if the user type is explicitly an "Organiser" (case-insensitive)
                if (dto.UserType?.Equals("Organiser", StringComparison.OrdinalIgnoreCase) == true)
                {
                    string hashedPassword = HashPassword(dto.Password ?? "");

                    // Query for registering an Organiser entity
                    string query = "INSERT INTO Organiser (OrganiserID, OrganiserName, Description, Email, Password) VALUES ((SELECT ISNULL(MAX(OrganiserID), 0) + 1 FROM Organiser), @Name, @Description, @Email, @Password)";
                    using SqlCommand cmd = new SqlCommand(query, connect);

                    cmd.Parameters.AddWithValue("@Name", dto.Name ?? "");
                    cmd.Parameters.AddWithValue("@Description", dto.Description ?? "");
                    cmd.Parameters.AddWithValue("@Email", dto.Email ?? "");
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);
                    cmd.ExecuteNonQuery();
                }
                else // Default branch handles general participants
                {
                    string hashedPassword = HashPassword(dto.Password ?? "");

                    // Query for registering a Participant entity
                    string query = "INSERT INTO Participant (ParticipantID, FullName, Email, Gender, Password) VALUES ((SELECT ISNULL(MAX(ParticipantID), 0) + 1 FROM Participant), @FullName, @Email, @Gender, @Password)";
                    using SqlCommand cmd = new SqlCommand(query, connect);

                    cmd.Parameters.AddWithValue("@FullName", dto.Name ?? "");
                    cmd.Parameters.AddWithValue("@Email", dto.Email ?? "");
                    cmd.Parameters.AddWithValue("@Gender", dto.Gender ?? "");
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);
                    cmd.ExecuteNonQuery();
                }

                return StatusCode(201, new { message = "User Account has been created successfully" });
            }
            catch (Exception error)
            {
                return BadRequest(new { message = error.Message });
            }
        }

        [HttpPost("logins")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            try
            {
                HttpContext.Session.SetString("UserEmail", dto.Email ?? "");
                HttpContext.Session.SetString("UserRole", dto.UserType ?? "Participant");
                return Ok(new { message = "Login Approved", role = dto.UserType });
            }
            catch (Exception error)
            {
                return BadRequest(new { message = error.Message });
            }
        }

        // Added Logout Endpoint
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            try
            {
                HttpContext.Session.Clear();
                return Ok(new { message = "Logout Successful" });
            }
            catch (Exception error)
            {
                return BadRequest(new { message = error.Message });
            }
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}