using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace StoriesProject.API.Models.DTO.Accountant
{
    public class AccountAuthenDTO
    {
        public ClaimsIdentity ClaimsIdentity { get; set; }
        public AuthenticationProperties AuthProperties { get; set; }
        public string UserName { get; set; }
        public int Coin { get; set; }
    }
}
