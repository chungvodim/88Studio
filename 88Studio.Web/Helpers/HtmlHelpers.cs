using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Mvc.Html;
using _88Studio.Resource;
using _88Studio.Utils.Common;

namespace _88Studio.Web.Code
{
    public static class HtmlHelpers
    {
        /// <summary>
        /// Creates a checkbox list for flag enums.
        /// </summary>
        /// <typeparam name="TModel">The model type.</typeparam>
        /// <typeparam name="TValue">The model property type.</typeparam>
        /// <param name="html">The html helper.</param>
        /// <param name="expression">The model expression.</param>
        /// <param name="htmlAttributes">Optional html attributes.</param>
        /// <param name="sortAlphabetically">Indicates if the checkboxes should be sorted alfabetically.</param>
        /// <returns></returns>
        public static MvcHtmlString CheckBoxListForEnum<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null, bool sortAlphabetically = true)
        {
            var fieldName = ExpressionHelper.GetExpressionText(expression);
            var fullBindingName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);
            var fieldId = TagBuilder.CreateSanitizedId(fullBindingName);

            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var value = metadata.Model;

            // Get all enum values
            IEnumerable<TValue> values = System.Enum.GetValues(typeof(TValue)).Cast<TValue>();

            // Sort them alphabetically by resource name or default enum name
            if (sortAlphabetically)
                values = values.OrderBy(i => i.ToString());

            // Create checkbox list
            var sb = new StringBuilder();
            foreach (var item in values)
            {
                TagBuilder builder = new TagBuilder("input");
                long targetValue = Convert.ToInt64(item);
                long flagValue = Convert.ToInt64(value);

                if ((targetValue & flagValue) == targetValue)
                    builder.MergeAttribute("checked", "checked");

                builder.MergeAttribute("type", "checkbox");
                builder.MergeAttribute("value", item.ToString());
                builder.MergeAttribute("name", fieldId);

                // Add optional html attributes
                if (htmlAttributes != null)
                    builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
                builder.InnerHtml = item.ToString();

                sb.Append(builder.ToString(TagRenderMode.Normal));

                // Seperate checkboxes by new line
                sb.Append("<br/>");
            }

            return new MvcHtmlString(sb.ToString());
        }

        public static MvcHtmlString DropdownListForEnum<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string optionLabel = null, object htmlAttributes = null)
        {
            var selectList = System.Enum.GetValues(typeof(TValue)).Cast<TValue>().Where(x => (x as System.Enum).GetAttribute<IgnoredEnumAttribute>() == null).Select(x => new SelectListItem()
            {
                Value = (Convert.ToInt64(x)).ToString(),
                Text = (x as System.Enum).GetEnumDisplayText()
            });

            return html.DropDownListFor<TModel, TValue>(expression, selectList, optionLabel, htmlAttributes);
        }

        public static MvcHtmlString DropdownListForEnumNullable<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, Nullable<TValue>>> expression, string optionLabel = null, object htmlAttributes = null) where TValue : struct, IComparable
        {
            var selectList = System.Enum.GetValues(typeof(TValue)).Cast<TValue>().Select(x => new SelectListItem()
            {
                Value = (Convert.ToInt64(x)).ToString(),
                Text = (x as System.Enum).GetEnumDisplayText()
            });

            return html.DropDownListFor<TModel, TValue?>(expression, selectList, optionLabel, htmlAttributes);
        }
    }
}