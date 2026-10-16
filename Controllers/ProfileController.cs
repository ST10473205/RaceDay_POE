using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace RaceDay_POE.Controllers
{
    [Route("api/profile")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        // Direct connection string field
        private readonly string string_connection = @"Data source=(localdb)\api_endpoint;DATABASE=RaceDayDB;";

        // 1. Get Profile (View profile details for logged-in user)
        [HttpGet]
        public IActionResult GetProfile()
        {
            string? email = HttpContext.Session.GetString("UserEmail");
            string? role = HttpContext.Session.GetString("UserRole");

            if (string.IsNullOrEmpty(email))
                return Unauthorized(new { message = "User not logged in." });

            var profileData = new Dictionary<string, object>();

            try
            {
                using SqlConnection connect = new SqlConnection(string_connection);
                connect.Open();

                string query = role == "Organiser"
                    ? "SELECT * FROM Organiser WHERE Email = @Email"
                    : "SELECT * FROM Participant WHERE Email = @Email";

                using SqlCommand cmd = new SqlCommand(query, connect);
                cmd.Parameters.AddWithValue("@Email", email);

                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        profileData[reader.GetName(i)] = reader.GetValue(i);
                    }
                    return Ok(profileData);
                }

                return NotFound(new { message = "Profile not found." });
            }
            catch (Exception error)
            {
                return BadRequest(new { message = error.Message });
            }
        }

        // 2. Update Profile (Updates either Organiser or Participant based on session role)
        [HttpPut]
        public IActionResult UpdateProfile([FromBody] Dictionary<string, object> updateData)
        {
            string? email = HttpContext.Session.GetString("UserEmail");
            string? role = HttpContext.Session.GetString("UserRole");

            if (string.IsNullOrEmpty(email))
                return Unauthorized(new { message = "User not logged in." });

            try
            {
                using SqlConnection connect = new SqlConnection(string_connection);
                connect.Open();

                string query = role == "Organiser"
                    ? "UPDATE Organiser SET OrganiserName = @Name, Description = @Description WHERE Email = @Email"
                    : "UPDATE Participant SET FullName = @FullName, Gender = @Gender WHERE Email = @Email";

                using SqlCommand cmd = new SqlCommand(query, connect);
                cmd.Parameters.AddWithValue("@Email", email);

                if (role == "Organiser")
                {
                    cmd.Parameters.AddWithValue("@Name", updateData.ContainsKey("organiserName") ? updateData["organiserName"] : updateData["name"]);
                    cmd.Parameters.AddWithValue("@Description", updateData["description"]);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@FullName", updateData.ContainsKey("fullName") ? updateData["fullName"] : updateData["name"]);
                    cmd.Parameters.AddWithValue("@Gender", updateData["gender"]);
                }

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return Ok(new { message = "Profile Update Confirmed" });
                }

                return NotFound(new { message = "Profile not found." });
            }
            catch (Exception error)
            {
                return BadRequest(new { message = error.Message });
            }
        }

        // 3. Delete Profile (Deletes account and clears active session)
        [HttpDelete]
        public IActionResult DeleteProfile()
        {
            string? email = HttpContext.Session.GetString("UserEmail");
            string? role = HttpContext.Session.GetString("UserRole");

            if (string.IsNullOrEmpty(email))
                return Unauthorized(new { message = "User not logged in." });

            try
            {
                using SqlConnection connect = new SqlConnection(string_connection);
                connect.Open();

                string query = role == "Organiser"
                    ? "DELETE FROM Organiser WHERE Email = @Email"
                    : "DELETE FROM Participant WHERE Email = @Email";

                using SqlCommand cmd = new SqlCommand(query, connect);
                cmd.Parameters.AddWithValue("@Email", email);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    HttpContext.Session.Clear(); // Log user out by clearing session
                    return Ok(new { message = "Account Deleted Successfully" });
                }

                return NotFound(new { message = "Profile not found." });
            }
            catch (Exception error)
            {
                return BadRequest(new { message = error.Message });
            }
        }
    }
}