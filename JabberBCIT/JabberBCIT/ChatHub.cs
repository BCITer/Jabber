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
        /// This method is called on the client side to broadcast a message to al clients
        /// </summary>
        /// <param name="message">message recieved from the client to be broadcasted to all clients</param>
        public void Send(string roomName, string sender, string msg, string senderID) {

            string sqlQuery = "insert into ChatMessages (ChatID, UserID, \"Message\", \"Timestamp\") values (@ChatID, @UserID, @Message, @Now)";

            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            cmd.Parameters.AddWithValue("@ChatID", roomName);
            cmd.Parameters.AddWithValue("@UserID", senderID);
            cmd.Parameters.AddWithValue("@Message", msg);
            cmd.Parameters.AddWithValue("@Now", DateTime.Now);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            Clients.Group(roomName).AddMessage(sender, msg);
        }
        public void ConnectToGroups(string sender, string chatID)
        {
            /*DataTable dtNames = new DataTable();

            string sqlQuery = "select ChatID from ChatConversationMembers where UserID = '" + 
                               sender + "'";

            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            da.Fill(dtNames);
            if(dtNames.Rows.Count > 0)
            {
                foreach (DataRow row in dtNames.Rows)
                {
                    string chatID = row["ChatID"].ToString();
                    Groups.Add(Context.ConnectionId, chatID);
                }
            }*/
            Groups.Add(Context.ConnectionId, chatID);
        }
    }
}