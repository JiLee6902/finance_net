﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IPasswordHashingService
    {
        public byte[] GenerateSalt();
        public string HashPassword(string password, byte[] salt);
        public bool VerifyPassword(string password, string hashedPassword);
    }
}