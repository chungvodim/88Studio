using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ScrapySharp.Core;
using ScrapySharp.Html.Parsing;
using ScrapySharp.Network;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Html.Forms;

namespace Tearc.ScrapingBot
{
    class Program
    {
        static void Main(string[] args)
        {
            // setup the browser
            ScrapingBrowser Browser = new ScrapingBrowser();
            Browser.AllowAutoRedirect = true; // Browser has many settings you can access in setup
            Browser.AllowMetaRedirect = true;
            //go to the home page
            WebPage PageResult = Browser.NavigateToPage(new Uri("https://vozforums.com/showthread.php?t=6134837"));
            // get first piece of data, the page title
            //HtmlNode TitleNode = PageResult.Html.CssSelect(".navbar-brand").First();

            // get all comment nodes using XPATH
            RemoveComment(PageResult.Html);

            var navbars = PageResult.Html.CssSelect(".navbar");

            if(navbars != null && navbars.Any())
            {
                PrintAttentionText(navbars.Last().InnerText.Trim());
            }

            //string PageTitle = TitleNode.InnerText;
            //// get a list of data from a table
            //List<String> Names = new List<string>();
            //var Table = PageResult.Html.CssSelect("#PersonTable").First();
            //foreach (var row in Table.SelectNodes("tbody/tr"))
            //{
            //    foreach (var cell in row.SelectNodes("td[1]"))
            //    {
            //        Names.Add(cell.InnerText);
            //    }
            //}
            //// find a form and send back data
            //PageWebForm form = PageResult.FindFormById("dataForm");
            //// assign values to the form fields
            //form["UserName"] = "AJSON";
            //form["Gender"] = "M";
            //form.Method = HttpVerb.Post;
            //WebPage resultsPage = form.Submit();
        }

        private static void RemoveComment(HtmlNode html)
        {
            foreach (HtmlNode comment in html.SelectNodes("//comment()"))
            {
                comment.ParentNode.RemoveChild(comment);
            }
        }

        private static void PrintAttentionText(string text)
        {
            ConsoleColor originalColor = System.Console.ForegroundColor;
            System.Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.WriteLine(text);
            System.Console.ForegroundColor = originalColor;
        }
    }
}
