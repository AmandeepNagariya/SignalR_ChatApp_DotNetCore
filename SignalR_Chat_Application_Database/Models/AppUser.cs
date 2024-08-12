using Microsoft.AspNetCore.Identity;

namespace SignalR_Chat_Application_Database.Models
{
    public class AppUser : IdentityUser
    {
        //1 to many relationship AppUser - Message
        public AppUser()
        {
            Messages = new HashSet<Message>();
        }
        public virtual ICollection<Message> Messages { get; set; }

    }
}
