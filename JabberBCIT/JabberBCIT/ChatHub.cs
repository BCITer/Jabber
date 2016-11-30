using Microsoft.AspNet.SignalR;

namespace JabberBCIT
{
    public class ChatHub : Hub {
        
        /// <summary>
        /// This method is called on the client side to broadcast a message to al clients
        /// </summary>
        /// <param name="message">message recieved from the client to be broadcasted to all clients</param>
        public void Send(string message) {
            
            if (message != "")
                Clients.All.addNewMessageToPage(Context.User.Identity.Name, message);
        }
    }
}