using FrontToBackFlowers.Data;
using FrontToBackFlowers.Models.IdentityModels;
using FrontToBackFlowers.Services;
using FrontToBackFlowers.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FrontToBackFlowers.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityOfUser> _userManager;
        private readonly SignInManager<IdentityOfUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMailService _mailManager;

        public AccountController(UserManager<IdentityOfUser> userManager, SignInManager<IdentityOfUser> signInManager, RoleManager<IdentityRole> roleManager, IMailService mailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mailManager = mailService;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View();

            var existUsers = await _userManager.FindByNameAsync(model.UserName);
            if (existUsers is not null)
            {
                ModelState.AddModelError("", "Username cannot be duplicated");
                return View();
            }
            var user = new IdentityOfUser
            {
                FullName = model.FullName,
                UserName = model.UserName,
                Email = model.Email,
            };

            //var role = await _roleManager.CreateAsync(new IdentityRole
            //{
            //    Name = "Admin"
            //});

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                   
                }
                return View();
            }
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var resetLink = Url.Action(nameof(ConfirmEmail), "Account", new { email = model.Email, token }, Request.Scheme, Request.Host.ToString());

            var requestEmail = new RequestEmail
            {
                ToEmail = model.Email,
                Body = resetLink,
                Subject = "Confirmation of Email"
            };

            await _mailManager.SendEmailAsync(requestEmail);
          

            //await _userManager.AddToRoleAsync(user, "Admin");

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            await _signInManager.SignInAsync(user, false);
            return Redirect(nameof(Login));
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existUser = await _userManager.FindByNameAsync(model.UserName);
                if(existUser is null)
                {
                    ModelState.AddModelError("", "Username isn't correct");
                    return View();
                }
               
                var result = await _signInManager.PasswordSignInAsync(existUser, model.Password, false, true);

                if (result.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Email address must be confirmed");
                    return View();
                }

                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "This user locked out");
                    return View();
                }

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Invalid credential");
                    return View();
                }
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }

        public IActionResult ChangePassword()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction(nameof(Login));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var existUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (existUser is null) return BadRequest();

            var result = await _userManager.ChangePasswordAsync(existUser, model.OldPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPassword(ForgetViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Must be write email address");
                return View();
            }

            var existUser = await _userManager.FindByEmailAsync(model.Mail);

            if(existUser is null)
            {
                ModelState.AddModelError("", "So the email is not available");
                return View();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(existUser);

            var resetlink = Url.Action(nameof(ResetPassword), "Account", 
                new { email = model.Mail, token }, Request.Scheme, Request.Host.ToString());

            var mailRequest = new RequestEmail
            {
                ToEmail = model.Mail,
                Body = resetlink,
                Subject = "Reset Link"
            };

            await _mailManager.SendEmailAsync(mailRequest);

            return RedirectToAction(nameof(ResetPassword));
        }

        public async Task<IActionResult> ConfirmEmail(string email , string token)
        {
            var user = await _userManager.FindByEmailAsync(email);

            await _userManager.ConfirmEmailAsync(user, token);

            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction(nameof(Login));
        }

        public IActionResult ResetPassword(string email, string token)
        {
            return View(new ResetPasswordViewmodel { Email = email, Token = token });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewmodel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Must be filled in correctly");

                return View();
            
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return BadRequest();

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Login));
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
               
            }

            return View();
        }

    }  
}
