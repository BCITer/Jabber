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
        static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ChitterContext"].ConnectionString;
        SqlConnection conn = new SqlConnection(connectionString);

        // GET: Chat
        public ActionResult Chat(ChatGroup cg)
        {
            if (!String.IsNullOrEmpty(cg.GroupName))
            {
                conn.Open();

                NewChatConversation(cg.GroupName);
                
                int chatID = GetChatID();

                try
                {
                    foreach (string member in cg.Members.Split('|'))
                    {
                        AddConversationMembers(chatID, GetUserID(member.Trim()));
                    }
                }
                catch (Exception e) { }

                AddConversationMembers(chatID, User.Identity.GetUserId());

                conn.Close();
            }

            ModelState.Clear();
            //ModelState.Remove("Members");
            ViewBag.HtmlStr = GetAllConversations();
            return View();
        }

        public void NewChatConversation(string groupName)
        {
            string sqlQuery = "insert into ChatConversation (ChatName, UserID) values (@Name, @User)";

            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            cmd.Parameters.AddWithValue("@Name", groupName);
            cmd.Parameters.AddWithValue("@User", User.Identity.GetUserId());

            cmd.ExecuteNonQuery();
        }

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
            catch (Exception e) { }
            
        }

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

                html += "<div class=\"chats\" id=\"" + chatID + "\"><br /><h3>" + chatName + "</h3><br /></div>";
            }
            return html;
        }
    }
}