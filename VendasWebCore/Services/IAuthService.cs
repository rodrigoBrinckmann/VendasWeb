﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendasWebCore.Services
{
    public interface IAuthService
    {
        string GenerateJwtToken(string email, string role);
        string ComputeSha256Hash(string password);
    }
}
