using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using InTurn_Model;

namespace InTurn.HTMLHelpers
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString Email(this HtmlHelper html, IContact email)


        {
            var emailTag = new TagBuilder("a") { InnerHtml = $"{email.Email}" };
            emailTag.MergeAttribute("id", "Email");
            emailTag.MergeAttribute("href", $"mailto:{email.Email}?subject=");
            string emailLink = emailTag.ToString(TagRenderMode.Normal);

            return new MvcHtmlString(emailLink);

        }
    }
}