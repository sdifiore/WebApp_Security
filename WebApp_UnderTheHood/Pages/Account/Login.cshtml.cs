using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp_UnderTheHood.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential? LoginCredential { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // Verify the credentials hardcoded for simplicity
            if (LoginCredential?.Username == "admin" && LoginCredential.Password == "password")
            {
                // Creating the secure context
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Email, "admin@difiore.com.br")
                };

                var claimsIdentity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);
                // Will serialize the claimsPrincipal into a string and store them in a cookie in the HTTP context.
                // HTTP context is the current request context, which contains information about the request and response.

                // Simulate successful login
                TempData["Message"] = "Login successful!";
                return RedirectToPage("/Index");
            }
            else
            {
                // Simulate failed login
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return Page();
            }
        }

        public class Credential
        {
            [Required]
            [Display(Name = "User Name")]
            public string? Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string? Password { get; set; }
        }
    }
}