using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace StoriesProject.Model.ViewModel.Accountant
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public List<string> RoleList { get; set; }
        public string ImgAvatar { get; set; }
        public long Coin { get; set; }

    }
}
