using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalR_Chat_Application_Database.Models
{
    public class Message
    {
        public int Id { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string Text { get; set; }

        public DateTime When { get; set; }

        [NotMapped]
        public string? UserID { get; set; }
        public virtual AppUser Sender { get; set; }
        [NotMapped]
        public string? Email { get; set; }
        [NotMapped]
        public string? Password { get; set; }
        [NotMapped]
        public string? ConfirmPassword { get; set; }

        public Message()
        {
            When = DateTime.Now;
        }


        
    }
}
