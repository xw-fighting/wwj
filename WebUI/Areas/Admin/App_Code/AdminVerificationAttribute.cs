﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EFClassLibrary;
using System.Web.Mvc;

/// <summary>
/// 登录过滤
/// </summary>
public class AdminVerificationAttribute : ActionFilterAttribute
{
    D8WeChatEntities db = new D8WeChatEntities();
    public override void OnActionExecuting(ActionExecutingContext context)
    {

        if (HttpContext.Current.Request.Cookies["uname"] == null || HttpContext.Current.Request.Cookies["upwd"] == null)
        {
            context.Result = new RedirectResult("/Admin/Login");
            return;
        }
        else
        {
            var query = db.ant_admin;

            var uname = TDESHelper.DecryptString(HttpContext.Current.Request.Cookies["uname"].Value);
            var upwd = HttpContext.Current.Request.Cookies["upwd"].Value;
            var sys_admin = query.Where(u => u.ant_admin_name == uname & u.ant_admin_pwd == upwd).SingleOrDefault();
            string result = string.Empty;
            if (sys_admin == null)
            {
                context.Result = new RedirectResult("/Admin/Login");
                return;
            }
            else
            {
                if (sys_admin.ant_admin_role == "-1")
                {
                    context.Result = new RedirectResult("/Admin/Login");
                    return;

                }
                else
                {
                    base.OnActionExecuting(context);
                }

            }
        }
    }
}
