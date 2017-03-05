using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using _88Studio.Utils.Common;
using _88Studio.Resource;

namespace _88Studio.Web
{
    public static class LabelExtensions
    {
        public static IHtmlString Label<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return Label(html, expression, new RouteValueDictionary());
        }
        public static IHtmlString Label<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            return Label(html, expression, new RouteValueDictionary(htmlAttributes));
        }
        public static IHtmlString Label<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            if (String.IsNullOrEmpty(labelText))
            {
                return MvcHtmlString.Empty;
            }

            TagBuilder tag = new TagBuilder("label");
            tag.MergeAttributes(htmlAttributes);
            tag.Attributes.Add("for", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));

            TagBuilder span = new TagBuilder("span");
            span.InnerHtml += GetResource(labelText, metadata);
            if (expression.IsRequired())
            {
                TagBuilder i = new TagBuilder("i");
                i.AddCssClass("fa fa-asterisk red");
                i.MergeAttribute("aria-hidden", "true");
                span.InnerHtml += " " + i.ToString(TagRenderMode.Normal);
                //labelText += " <i class='fa fa-asterisk' aria-hidden='true'></i>";
            }

            // assign <span> to <label> inner html
            tag.InnerHtml = span.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }

        private static string GetResource(string labelText, ModelMetadata metadata)
        {
            //return labelText;
            return DatabaseResourceManager.GetHtmlString(null, labelText, labelText.Replace(" ","_"), metadata.ContainerType.FullName);
        }
    }
}