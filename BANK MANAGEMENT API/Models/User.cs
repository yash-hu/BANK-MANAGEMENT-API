﻿namespace BANK_MANAGEMENT_API.Models
{
    public class User
    {
        public string Username { get; set; } = null!;

        public byte[] PasswordHash { get; set; }=null!;
        public byte[] PasswordSalt { get; set; } = null!;
    }
}
