﻿namespace Library_DAL_2.Models
{
    public abstract class User
    {
        public string Login { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        public string Email { get; set; } = string.Empty;
        public UserRole Role { get; set; }
    }
}
