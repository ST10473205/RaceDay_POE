using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace RaceDay_POE.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        // Direct connection string field
        private readonly string string_connection = @"Data source=(localdb)\api_endpoint;DATABASE=RaceDayDB;";

        // 1. Get All Events
        [HttpGet]
        public IActionResult GetAllEvents()
        {
            var eventsList = new List<Dictionary<string, object>>();

            try
            {
                using SqlConnection connect = new SqlConnection(string_connection);
                connect.Open();

                string query = "SELECT * FROM Event";
                using SqlCommand cmd = new SqlCommand(query, connect);
                using SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var ev = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        ev[reader.GetName(i)] = reader.GetValue(i);
                    }
                    eventsList.Add(ev);
                }

                return Ok(eventsList);
            }
            catch (Exception error)
            {
                return BadRequest(new { message = error.Message });
            }
        }

        // 2. Create an Event
        [HttpPost]
        public IActionResult CreateEvent([FromBody] Dictionary<string, object> newEvent)
        {
            try
            {
                using SqlConnection connect = new SqlConnection(string_connection);
                connect.Open();

                string query = "INSERT INTO Event (EventID, OrganiserID, EventName, EventDate, EventType) VALUES ((SELECT ISNULL(MAX(EventID), 0) + 1 FROM Event), @OrganiserID, @EventName, @EventDate, @EventType)";
                using SqlCommand cmd = new SqlCommand(query, connect);
                cmd.Parameters.AddWithValue("@OrganiserID", newEvent["organiserID"]);
                cmd.Parameters.AddWithValue("@EventName", newEvent["eventName"]);
                cmd.Parameters.AddWithValue("@EventDate", Convert.ToDateTime(newEvent["eventDate"]));
                cmd.Parameters.AddWithValue("@EventType", newEvent["eventType"]);

                cmd.ExecuteNonQuery();
                return StatusCode(201, new { message = "The Event is created sucessfully" });
            }
            catch (Exception error)
            {
                return BadRequest(new { message = error.Message });
            }
        }

        // 3. Update an Event (PUT)
        [HttpPut("{eventId}")]
        public IActionResult UpdateEvent(int eventId, [FromBody] Dictionary<string, object> updateEvent)
        {
            try
            {
                using SqlConnection connect = new SqlConnection(string_connection);
                connect.Open();

                string query = "UPDATE Event SET EventName = @EventName, EventDate = @EventDate, EventType = @EventType WHERE EventID = @EventID";
                using SqlCommand cmd = new SqlCommand(query, connect);
                cmd.Parameters.AddWithValue("@EventID", eventId);
                cmd.Parameters.AddWithValue("@EventName", updateEvent["eventName"]);
                cmd.Parameters.AddWithValue("@EventDate", Convert.ToDateTime(updateEvent["eventDate"]));
                cmd.Parameters.AddWithValue("@EventType", updateEvent["eventType"]);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return Ok(new { message = "Event Updated Successfully" });
                }

                return NotFound(new { message = "Event not found." });
            }
            catch (Exception error)
            {
                return BadRequest(new { message = error.Message });
            }
        }

        // 4. Delete an Event (DELETE)
        [HttpDelete("{eventId}")]
        public IActionResult DeleteEvent(int eventId)
        {
            try
            {
                using SqlConnection connect = new SqlConnection(string_connection);
                connect.Open();

                string query = "DELETE FROM Event WHERE EventID = @EventID";
                using SqlCommand cmd = new SqlCommand(query, connect);
                cmd.Parameters.AddWithValue("@EventID", eventId);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return Ok(new { message = "Event Deleted Successfully" });
                }

                return NotFound(new { message = "Event not found." });
            }
            catch (Exception error)
            {
                return BadRequest(new { message = error.Message });
            }
        }

        // 5. Get Categories for a Specific Event
        [HttpGet("{eventId}/categories")]
        public IActionResult GetCategories(int eventId)
        {
            var categories = new List<Dictionary<string, object>>();

            try
            {
                using SqlConnection connect = new SqlConnection(string_connection);
                connect.Open();

                string query = "SELECT * FROM Category WHERE EventID = @EventID";
                using SqlCommand cmd = new SqlCommand(query, connect);
                cmd.Parameters.AddWithValue("@EventID", eventId);

                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var cat = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        cat[reader.GetName(i)] = reader.GetValue(i);
                    }
                    categories.Add(cat);
                }

                return Ok(categories);
            }
            catch (Exception error)
            {
                return BadRequest(new { message = error.Message });
            }
        }

        // 6. Enrol Participant into an Event Category
        [HttpPost("{eventId}/enrol")]
        public IActionResult EnrolInEvent(int eventId, [FromBody] Dictionary<string, object> enrolmentData)
        {
            try
            {
                using SqlConnection connect = new SqlConnection(string_connection);
                connect.Open();

                string query = "INSERT INTO Entry (EntryID, ParticipantID, CategoryID) VALUES ((SELECT ISNULL(MAX(EntryID), 0) + 1 FROM Entry), @ParticipantID, @CategoryID)";
                using SqlCommand cmd = new SqlCommand(query, connect);
                cmd.Parameters.AddWithValue("@ParticipantID", enrolmentData["participantID"]);
                cmd.Parameters.AddWithValue("@CategoryID", enrolmentData["categoryID"]);

                cmd.ExecuteNonQuery();
                return StatusCode(201, new { message = "Enrolment Successful" });
            }
            catch (Exception error)
            {
                return BadRequest(new { message = error.Message });
            }
        }
    }
}