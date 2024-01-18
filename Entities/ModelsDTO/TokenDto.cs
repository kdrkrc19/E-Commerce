using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ModelsDTO
{
    public class TokenDto
    {
        public String AccessToken { get; init; }
        public String RefreshToken { get; init; }
    }
}