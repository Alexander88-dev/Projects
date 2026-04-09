using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolHub.Data;
using SchoolHub.Models;

namespace SchoolHub.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        public IndexModel(AppDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public string RegisterName { get; set; }
        [BindProperty]
        public string RegisterLogin { get; set; }
        [BindProperty]
        public string RegisterPassword { get; set; }
        [BindProperty]
        public string LoginPassword { get; set; }
        [BindProperty]
        public string LoginLogin { get; set; }
        public bool IsAuthorized { get; set; }
        public string CurrentUserName { get; set; }
        public string Message { get; set; }

        public void OnGet()
        {
            LoadUser();
        }
        public IActionResult OnPostRegister()
        {
            LoadUser();
            if (string.IsNullOrEmpty(RegisterName) || string.IsNullOrEmpty(RegisterLogin) || string.IsNullOrEmpty(RegisterPassword))
            {
                Message = "Заполните все поля регистрации";
                return Page();
            }
    
            if (_context.Users.Any(u => u.Login == RegisterLogin))
            {
                Message = "Пользователь с таким логином уже существует";
                return Page();
            }

            var user = new User
            {
                Name = RegisterName,
                Login = RegisterLogin,
                Password = RegisterPassword
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserName", user.Name);

            return RedirectToPage();
        }
        public IActionResult OnPostLogin() 
        {
            LoadUser();
            if (string.IsNullOrEmpty(LoginLogin) || string.IsNullOrEmpty(LoginPassword)) 
            {
                Message = "Введите логин и пароль";
                return Page();
            }
            var user = _context.Users.FirstOrDefault(u => u.Login == LoginLogin && u.Password == LoginPassword);

            if (user == null)
            {
                Message = "Неверный логин или праоль.";
                return Page();
            }

            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserName", user.Name);

            return RedirectToPage();
        }
        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();

            return RedirectToPage();
        }
        private void LoadUser() 
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var userName = HttpContext.Session.GetString("UserName");

            IsAuthorized = userId != null;

            if (!string.IsNullOrEmpty(userName)) 
            {
                CurrentUserName = userName;
            }

        }
    }
}
