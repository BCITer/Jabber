using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace JabberBCIT
{
    [HubName("ChatServer")]
    public class ChatHub : Hub {
        static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ChitterContext"].ConnectionString;
        SqlConnection conn = new SqlConnection(connectionString);
        /// <summary>
        /// This method is called on the client side to broadcast a message to al clients.
        /// It also stores a copy of the message in the database.
        /// </summary>
        /// <param name="message">message recieved from the client to be broadcasted to all clients</param>
        public void Send(string roomName, string sender, string msg, string senderID) {

            // If no room is selected do nothing 
            if (roomName == "")
            {
                return;
            }

            string sqlQuery = "insert into ChatMessages (ChatID, UserID, \"Message\", \"Timestamp\") values (@ChatID, @UserID, @Message, @Now)";

            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            cmd.Parameters.AddWithValue("@ChatID", roomName);
            cmd.Parameters.AddWithValue("@UserID", senderID);
            cmd.Parameters.AddWithValue("@Message", msg);
            cmd.Parameters.AddWithValue("@Now", DateTime.UtcNow);
            try { 
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            } finally
            {
                conn.Close();
            }

            Clients.Group(roomName).AddMessage(sender, msg);
        }
        /// <summary>
        /// Connects users to a specific conversation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="chatID"></param>
        public void ConnectToGroups(string sender, string chatID)
        {
            Groups.Add(Context.ConnectionId, chatID);
        }
    }
}