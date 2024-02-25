﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace StoriesProject.Common.Attribute
{
    public class CustomAuthorizeAttribute: IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Kiểm tra xem người dùng đã được xác thực chưa
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                // Nếu không, chuyển hướng đến trang đăng nhập
                context.Result = new RedirectToActionResult("Login", "Account", null);
            }
        }
    }
}
