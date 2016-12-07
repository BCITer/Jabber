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
    [Authorize]
    public class ChatController : Controller
    {
        //Our connection to the database
        static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ChitterContext"].ConnectionString;
        SqlConnection conn = new SqlConnection(connectionString);

        /// <summary>
        /// Stores all conversations in a viewbag to be listed in the view.
        /// Also stores all members and messages of a particular conversation in their respective viewbags
        /// </summary>
        /// <param name="cg"></param>
        /// <returns></returns>
        public ActionResult Chat(ChatGroup cg)
        {
            conn.Open();

            if (!String.IsNullOrEmpty(cg.GroupName))
            {
                NewChatConversation(cg.GroupName);
                
                int chatID = GetChatID();

                try
                {
                    foreach (string member in cg.Members.Split('|'))
                    {
                        AddConversationMembers(chatID, GetUserID(member.Trim()));
                    }
                }
                catch (Exception e) {
                    Console.WriteLine(e.ToString());                }

                AddConversationMembers(chatID, User.Identity.GetUserId());

            }

            System.Diagnostics.Debug.WriteLine("bool: " + cg.IsCreateNew.ToString());

            if (!cg.IsCreateNew)
            {
                try
                {
                    foreach (string member in cg.Members.Split('|'))
                    {
                        AddConversationMembers(cg.ChatID, GetUserID(member.Trim()));
                    }
                }
                catch (Exception e) { }
            }

            ViewBag.Members = GetAllMembers(cg.ChatID);
            ViewBag.Messages = GetAllMessages(cg.ChatID);

            ViewBag.Chats = GetAllConversations();
            conn.Close();
            
            return View();
        }


        /// <summary>
        /// Inserts a new conversation record into the database.
        /// </summary>
        /// <param name="groupName"></param>
        public void NewChatConversation(string groupName)
        {
            string sqlQuery = "insert into ChatConversation (ChatName, UserID) values (@Name, @User)";

            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            cmd.Parameters.AddWithValue("@Name", groupName);
            cmd.Parameters.AddWithValue("@User", User.Identity.GetUserId());

            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Retreives the latest conversation the user created.
        /// </summary>
        /// <returns></returns>
        public int GetChatID()
        {
            string sqlQuery = "select ChatID from ChatConversation a left join (select UserID, max(\"Timestamp\") as \"Max\" from ChatConversation group by UserID) b on a.UserID=b.UserID where a.\"Timestamp\" = b.\"Max\" and a.UserID = @User";

            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            cmd.Parameters.AddWithValue("@User", User.Identity.GetUserId());

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return Int32.Parse(reader["ChatID"].ToString());
                }
            }
            
            return -1;
        }

        /// <summary>
        /// Add members to a specific conversation
        /// </summary>
        /// <param name="chatID"></param>
        /// <param name="member"></param>
        public void AddConversationMembers(int chatID, string member)
        {
            string sqlQuery = "insert into ChatConversationMembers (ChatID, UserID) values (@ChatID, @Member)";
            
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            cmd.Parameters.AddWithValue("@ChatID", chatID);
            cmd.Parameters.AddWithValue("@Member", member);
            
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
            
        }

        /// <summary>
        /// Gets the UserID of the user currently logged in
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string GetUserID(string userName)
        {
            string sqlQuery = "select Id from AspNetUsers where UserName = @Name";

            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            cmd.Parameters.AddWithValue("@Name", userName);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return reader["Id"].ToString();
                }
            }

            return null;
        }

        /// <summary>
        /// Queries the database for all possible users you can add to a conversation.
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public ActionResult Autocomplete(string term)
        {
            List<string> users = new List<string>();
            DataTable dtNames = new DataTable();

            string sqlQuery = "select UserName from AspNetUsers";

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

        /// <summary>
        /// Retreives all conversations the user is a part of
        /// </summary>
        /// <returns></returns>
        public string GetAllConversations()
        {
            string html = "";

            DataTable dtNames = new DataTable();

            string sqlQuery = "select a.ChatID, ChatName from ChatConversation a left join ChatConversationMembers b on a.ChatID=b.ChatID where b.UserID = '" + User.Identity.GetUserId() + "'";

            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            da.Fill(dtNames);

            foreach (DataRow row in dtNames.Rows)
            {
                string chatID = row["ChatID"].ToString();
                string chatName = row["ChatName"].ToString();

                html += "<a href=\"#\" class=\"chats\" id=\"" + chatID + "\">" + chatName + "</a>";
            }
            return html;
        }

        /// <summary>
        /// Gets all members of a conversation
        /// </summary>
        /// <param name="chatID"></param>
        /// <returns></returns>
        public string GetAllMembers(int chatID)
        {
            string html = "";

            DataTable dtNames = new DataTable();

            string sqlQuery = "select UserName from ChatConversationMembers a left join AspNetUsers b on a.UserID=b.Id where ChatID = '" + chatID + "'";

            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            da.Fill(dtNames);

            foreach (DataRow row in dtNames.Rows)
            {
                string userName = row["UserName"].ToString();

                html += "<a href=\"#\">" + userName + "</a>";
            }
            return html;
        }

        /// <summary>
        /// Gets all messages in a conversation
        /// </summary>
        /// <param name="chatID"></param>
        /// <returns></returns>
        public string GetAllMessages(int chatID)
        {
            string html = "";

            DataTable dtNames = new DataTable();

            string sqlQuery = "select UserName, \"Message\" from ChatMessages a left join AspNetUsers b on a.UserID=b.Id where ChatID = '" + chatID + "' order by \"Timestamp\" asc";

            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            da.Fill(dtNames);

            foreach (DataRow row in dtNames.Rows)
            {
                string userName = row["UserName"].ToString();
                string message = row["Message"].ToString();
                string compare = User.Identity.GetUserName();
                if (userName.Equals(compare)) {
                    html += "<div id =\"chatmessage\"><div id=\"right\"><span><strong>" + userName + ": </strong>" + message + "</span></div></div>";
                } else {
                    html += "<div id =\"chatmessage\"><div id=\"left\"><span><strong>" + userName + ": </strong>" + message + "</span></div></div>";
                }

            }
            return html;
        }
    }
}