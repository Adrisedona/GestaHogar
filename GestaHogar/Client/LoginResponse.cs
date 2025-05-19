using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaHogar.Client
{
    public class LoginResponse
    {
        public required string TokenType { get; set; }
        public required string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public required string RefreshToken { get; set; }
    }
}
