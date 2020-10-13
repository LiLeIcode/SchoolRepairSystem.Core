﻿using System.ComponentModel.DataAnnotations;

namespace SchoolRepairSystemModels.ViewModels
{
    public class LoginUser
    {
        [Required,StringLength(20)]
        public string UserName { get; set; }
        [Required, StringLength(20, MinimumLength = 2)]
        public string Password { get; set; }
    }
}