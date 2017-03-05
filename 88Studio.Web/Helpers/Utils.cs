using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace _88Studio.Web.Code
{
    public static class Utils
    {
        /// <summary>
        /// Convert an enumeration to a Mvc.SelectList for use in dropdowns. Note that the enumeration values must all be added to a resource file.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="enumObj">The enum obj.</param>
        /// <param name="sortAlphabetically">If set to <c>true</c> [the list is sorted alphabetically].</param>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> ToSelectList<TEnum>(this TEnum enumObj, bool sortAlphabetically = true)
        {
            IList<SelectListItem> values =
                        (from TEnum e in System.Enum.GetValues(typeof(TEnum))
                         select new SelectListItem
                         {
                             Text = e.ToString(),
                             Value = Convert.ToInt32(e).ToString()
                         }).ToList();

            if (sortAlphabetically)
                values = values.OrderBy(v => v.Text).ToList();

            return new SelectList(values, "Value", "Text", enumObj);
        }

        /// <summary>
        /// Get Errors From ModelState
        /// </summary>
        /// <param name="modelStates"></param>
        /// <returns></returns>
        public static string GetErrorsFromModelState(ModelStateDictionary modelStates)
        {
            StringBuilder sb = new StringBuilder();
            foreach (ModelState modelState in modelStates.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    sb.Append(error.ErrorMessage + "<br>");
                }
            }
            return sb.ToString();
        }
    }
}