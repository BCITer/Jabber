using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JabberBCIT.Models
{
    public class ChatGroup
    {
        public string GroupName { get; set; }
        public string Members { get; set; }
        public int ChatID { get; set; }
        public string NewMessage { get; set; }
        public Boolean IsCreateNew { get; set; }
    }
}