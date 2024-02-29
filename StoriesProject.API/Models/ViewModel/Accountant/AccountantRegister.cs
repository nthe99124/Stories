﻿using System.ComponentModel.DataAnnotations;

namespace StoriesProject.API.Models.ViewModel.Accountant
{
    public class AccountantRegister
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Language { get; set; } = "vi-VN";
        public int Coin { get; set; }
    }
}