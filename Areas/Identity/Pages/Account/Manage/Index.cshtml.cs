// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BookWorn.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookWorn.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private readonly IWebHostEnvironment _env ; 

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager
            , IWebHostEnvironment environment
            )
        {
            _env = environment ; 
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }
        
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }
        public string ImagePath { get; set; }
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            [Display(Name = "Description")]
            public string Description { get; set; }
            
            public IFormFile formFile { get; set; } 

            public string Location { get; set; }
        }

        private void LoadAsync(User user)
        {
            var userName = user.UserName;
            var phoneNumber = user.PhoneNumber;

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber  
                , Description = user.Description 
                , Location = user.Location 
            };
            ImagePath = user.Image ; 
        }

        public async Task<IActionResult> OnGetAsync(string? returnUrl)
        {
             Console.WriteLine("returnUrl+ " + returnUrl );
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
        
             LoadAsync(user);
            return Page();
        }

   
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
             Console.WriteLine( "onpost" + "returnUrl+ " + returnUrl);
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                Console.WriteLine("Invalid");
                 LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            user.Location = Input.Location ; 
            user.Description = Input.Description ; 
            if(Input.formFile != null){
                FileStream stream = new FileStream( _env.WebRootPath + "/images/" + Input.formFile.FileName , FileMode.Create) ; 
               Input.formFile.CopyTo(stream);
               stream.Close()  ; 
               user.Image =  Input.formFile.FileName ; 
            }
            await _userManager.UpdateAsync(user); 
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            
              return RedirectToPage();
        }
    }
}
