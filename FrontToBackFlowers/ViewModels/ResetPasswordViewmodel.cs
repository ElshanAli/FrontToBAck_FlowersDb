﻿using System.ComponentModel.DataAnnotations;

namespace FrontToBackFlowers.ViewModels
{
    public class ResetPasswordViewmodel
    {
       
        public string Email { get; set; }   
        public string Token { get; set; }
        public string Password { get; set; }
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
