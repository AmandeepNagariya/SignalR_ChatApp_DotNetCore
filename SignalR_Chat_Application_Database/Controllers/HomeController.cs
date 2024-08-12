using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using SignalR_Chat_Application_Database.Data;
using SignalR_Chat_Application_Database.Models;
using System.Diagnostics;

namespace SignalR_Chat_Application_Database.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<AppUser> _userManager;
        public HomeController(ApplicationDBContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            // Check if the user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                // Try to get the current user
                var currentUser = await _userManager.GetUserAsync(User);

                // Handle the case where the user could not be retrieved
                if (currentUser != null)
                {
                    ViewBag.CurrentUserName = currentUser.UserName;
                }
                else
                {
                    // Optionally, you can log this issue or handle it in another way
                    ViewBag.CurrentUserName = "Unknown User";
                }
            }
            else
            {
                // If the user is not authenticated, you can set a default value or handle it accordingly
                ViewBag.CurrentUserName = "Guest";
            }

            // Retrieve messages from the 'Message' table
            var messages = await _context.Messages.ToListAsync();

            // Pass the messages to the view
            ViewBag.Messages = messages;

            return View(messages);
        }






        public async Task<IActionResult> Chat(string email)
        {
            // Pass the email to the view
            ViewBag.UserEmail = email;

            // Simulate fetching messages for the chat (replace with actual logic)
            var messages = await _context.Messages.OrderBy(m => m.When).ToListAsync();

            return View(messages);
        }

        



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
