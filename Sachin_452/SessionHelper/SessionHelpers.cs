using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sachin_452.SessionHelper
{
    public class SessionHelpers
    {
        public static int UserID
        {

            set
            {
                HttpContext.Current.Session["UserID"] = value;
            }

            get
            {
                return HttpContext.Current.Session["UserID"] == null ? 0 : (int)HttpContext.Current.Session["UserID"];

            }
        }
        public static string UserName
        {
            get
            {
                return HttpContext.Current.Session["UserName"] == null ? "" : (string)HttpContext.Current.Session["UserName"];

            }

            set
            {
                HttpContext.Current.Session["UserName"] = value;
            }
        }

        public static string EmailID
        {
            get
            {
                return HttpContext.Current.Session["EmailID"] == null ? "" : (string)HttpContext.Current.Session["EmailID"];
            }
            set
            {
                HttpContext.Current.Session["EmailID"] = value;
            }
        }
    }
}