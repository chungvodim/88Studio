using _88Studio.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using _88Studio.Utils.Common;
using _88Studio.Dto;
using _88Studio.Entity;
using _88Studio.Resource;

namespace _88Studio.Web
{
    public static class HtmlHelperExtension
    {
        public static IHtmlString ValidationSummary(this HtmlHelper html, BaseDto viewModel)
        {
            if(viewModel != null && !string.IsNullOrWhiteSpace(viewModel.Message))
            {
                var p = new TagBuilder("p");
                p.SetInnerText(viewModel.Message);
                if (viewModel.Status == System.Net.HttpStatusCode.OK)
                {
                    p.AddCssClass("alert alert-success");
                }
                else
                {
                    p.AddCssClass("alert alert-error");
                }
                return html.Raw(HttpUtility.HtmlDecode(p.ToString(TagRenderMode.Normal)));
            }
            else
            {
                return null;
            }
        }

        public static IHtmlString DisplayResource(this HtmlHelper html, string rs)
        {
            return html.Raw(rs);
        }

        public static IHtmlString DisplayResource(this HtmlHelper html, MvcHtmlString rs)
        {
            return html.Raw(HttpUtility.HtmlDecode(rs.ToString()));
        }

        public static IHtmlString Resource(this HtmlHelper html, string rs, bool editable = true)
        {
            var controllerName = html.ViewContext.RouteData.Values["Controller"].ToString();
            var actionName = html.ViewContext.RouteData.Values["Action"].ToString();
            if (editable)
            {
                return new MvcHtmlString(DatabaseResourceManager.GetHtmlString(null, rs, rs.Replace(" ","_"), controllerName + "_" + actionName));
            }
            else
            {
                return new MvcHtmlString(DatabaseResourceManager.GetString(null, rs, rs.Replace(" ", "_"), controllerName + "_" + actionName));
            }
        }

        public static string EnumParseToDescription<TEnum>(this HtmlHelper html, string input) where TEnum : struct
        {
            TEnum result;
            var isSuccess = System.Enum.TryParse(input, out result);
            if (isSuccess)
            {
                return result.GetDescription();
            }
            else
            {
                return "";
            }
        }
    }
}