﻿namespace SchoolRepairSystem.Models.ViewModels
{
    public class TokenInfoViewModel
    {
        public string token { get; set; }
        public double expires_in { get; set; }
        public string token_type { get; set; }
    }
}