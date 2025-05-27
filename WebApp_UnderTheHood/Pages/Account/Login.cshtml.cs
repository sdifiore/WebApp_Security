using System.ComponentModel.DataAnnotations;

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

        public void OnPost()
        {
            if (!ModelState.IsValid)
                return;

            //verify the credentials hardcoded for simplicity
            if (LoginCredential.Username == "admin" && LoginCredential.Password == "password")
            {
                //Creatting a secure context

                // Simulate successful login
                TempData["Message"] = "Login successful!";
                Response.Redirect("/Index");
            }
            else
            {
                // Simulate failed login
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
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