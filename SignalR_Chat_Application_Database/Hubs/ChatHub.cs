using Microsoft.AspNetCore.SignalR;
using SignalR_Chat_Application_Database.Data;
using SignalR_Chat_Application_Database.Models;
using System;
using System.Threading.Tasks;

public class ChatHub : Hub
{
    private readonly ApplicationDBContext _context;

    public ChatHub(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task SendMessage(string userName, string text, string when)
    {
        // Convert the when string to DateTime
        if (!DateTime.TryParse(when, out DateTime whenDateTime))
        {
            whenDateTime = DateTime.UtcNow; // Fallback to UTC if parsing fails
        }

        // Create a new message
        var message = new Message
        {
            UserName = userName,
            Text = text,
            When = whenDateTime
        };

        // Add the new message to the database
        _context.Messages.Add(message);
        await _context.SaveChangesAsync();

        // Send the message to all connected clients
        await Clients.All.SendAsync("receiveMessage", message);
    }
}
