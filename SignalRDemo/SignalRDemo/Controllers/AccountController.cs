using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRDemo.DBContext;
using SignalRDemo.Hubs;
using SignalRDemo.Repos;

namespace SignalRDemo.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserRepo _userRepo;
        private readonly NotificationDBContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;
        public AccountController(UserRepo userRepo, NotificationDBContext context, IHubContext<NotificationHub> hubContext)
        {
            this._userRepo = userRepo;
            _context = context;
            _hubContext = hubContext;
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(string username, string password)
        {
            var userFromDb = await _userRepo.GetUserDetails(username, password);

            if (userFromDb == null)
            {
                ModelState.AddModelError("Login", "Invalid credentials");
                return View(0);
            }

            HttpContext.Session.SetString("Username", userFromDb.RecipientName);
            HttpContext.Session.SetString("RecipientId", userFromDb.RecipientId.ToString());
            HttpContext.Session.SetString("UserGroupId", userFromDb.UserGroupId.ToString());
            //TempData["Id"] = userFromDb.UserGroupId;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult SignOut()
        {

            HttpContext.Session.Remove("Username");
            HttpContext.Session.Remove("RecipientId");
            HttpContext.Session.Remove("UserGroupId");



            return RedirectToAction(nameof(SignIn));
        }
    }
}
