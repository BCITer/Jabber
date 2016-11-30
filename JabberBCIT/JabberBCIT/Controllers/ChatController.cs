using JabberBCIT.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Configuration;

namespace JabberBCIT.Controllers
{
    public class ChatController : Controller
    {
        // GET: Chat
        public ActionResult Chat(ChatGroup cg)
        {
            if (!String.IsNullOrEmpty(cg.GroupName))
            {
                string groupName = cg.GroupName;
                string[] members = cg.Members;
                int chatID;

                //Create the new chat
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ChitterContext"].ConnectionString;
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                string sqlQuery = "insert into ChatConversation (ChatName, UserID) values (@Name, @User)";

                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@Name", cg.GroupName);
                cmd.Parameters.AddWithValue("@User", User.Identity.GetUserId());

                cmd.ExecuteNonQuery();

                //Get the new ChatID

                sqlQuery = "select ChatID from ChatConversation a left join (select UserID, max(\"Timestamp\") as \"Max\" from ChatConversation group by UserID) b on a.UserID=b.UserID where a.\"Timestamp\" = b.\"Max\" and a.UserID = @User";

                cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@User", User.Identity.GetUserId());

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        chatID = Int32.Parse(reader["ChatID"].ToString());
                    }
                }

                conn.Close();

                //Return the ID as well in the autocomplete
                /* foreach(string member in cg.Members)
                 {
                     sqlQuery = "insert into ChatConversation (\"ChatName\", \"UserID\") values ('" + chatID + "', '" + User.Identity.GetUserId() + "')";
                 }*/
            }
            return View(cg);
        }

        public ActionResult Autocomplete(string term)
        {
            List<string> users = new List<string>();

            DataTable dtNames = new DataTable();

            string sqlQuery = "select UserName from AspNetUsers";

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ChitterContext"].ConnectionString;
            
            SqlConnection conn = new SqlConnection(connectionString);
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);

            da.Fill(dtNames);

            foreach (DataRow row in dtNames.Rows)
            {
                string name = Convert.ToString(row["UserName"]);
                users.Add(name);
             }

            var filteredItems = users.Where(
                item => item.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0
                );

            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }
    }
}