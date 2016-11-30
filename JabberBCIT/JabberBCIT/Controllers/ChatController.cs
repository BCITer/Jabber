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
            string groupName = cg.GroupName;
            string[] members = cg.Members;

            DataTable dtNames = new DataTable();

            string sqlQuery = "insert into ChatConversation (\"ChatName\", \"UserID\") values ('" + cg.GroupName + "', '" + User.Identity.GetUserId() + "')";

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ChitterContext"].ConnectionString;

            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);

                da.Fill(dtNames);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            string chatID;

            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ChitterContext"].ConnectionString;

                using (SqlCommand com = new SqlCommand())
                {
                    com.CommandText = "select ChatID from ChatConversation a left join (select UserID, max(\"Timestamp\") as \"Max\" from ChatConversation group by UserID) b on a.UserID=b.UserID where a.\"Timestamp\" = b.\"Max\" and a.UserID = '" + User.Identity.GetUserId() + "'";
                    
                    com.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = com.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            chatID = sdr["ChatID"].ToString();
                        }
                    }

                    con.Close();
                }
            }


            //Return the ID as well in the autocomplete
           /* foreach(string member in cg.Members)
            {
                sqlQuery = "insert into ChatConversation (\"ChatName\", \"UserID\") values ('" + chatID + "', '" + User.Identity.GetUserId() + "')";
            }*/

            return View(cg);
        }

        public ActionResult Autocomplete(string term)
        {
            List<string> users = new List<string>();
            
            DataTable dtNames = new DataTable();

            string sqlQuery = "select UserName from AspNetUsers";

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ChitterContext"].ConnectionString;

            try {
                SqlConnection conn = new SqlConnection(connectionString);
                SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);

                da.Fill(dtNames);

                foreach (DataRow row in dtNames.Rows)
                {
                    string name = Convert.ToString(row["UserName"]);
                    users.Add(name);
                }
            } catch (Exception ex) {
                throw ex;
            }

            var filteredItems = users.Where(
                item => item.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0
                );
            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }

       
    
    }
}