using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DryIoc;
using ScrapySharp.Core;
using ScrapySharp.Html.Parsing;
using ScrapySharp.Network;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Html.Forms;
using Tearc.Data.Entity;
using Tearc.Repository;
using log4net;
using System.Diagnostics;

namespace Tearc.ScrapingBot
{
    class Program
    {
        static IRepository repository;
        static ILog logger = LogManager.GetLogger(typeof(Program));
        const int NEWEST_ID = 6209153;//6209153

        static void Main(string[] args)
        {
            Config();
            Stopwatch stw = new Stopwatch();
            stw.Start();
            // setup the browser

            Scrape(NEWEST_ID, 100, "https://vozforums.com/showthread.php");
            stw.Stop();
            logger.WarnFormat("Time elapsed: {0}", stw.Elapsed.Seconds);
        }

        private static void Config()
        {
            log4net.Config.XmlConfigurator.Configure();

            var container = new Container(Rules.Default.WithTrackingDisposableTransients());
            //var container = new Container();
            Bootstrapper.DryIoC.Configure(container);
            repository = container.Resolve<IMongoRepository>();
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

        static void Scrape(int newestId, int range, string baseUrl = "https://vozforums.com/showthread.php")
        {
            var degreeOfParallelism = Environment.ProcessorCount;
            var tasks = new Task[degreeOfParallelism];
            int throttle = newestId - range;

            for (int taskNumber = 0; taskNumber < degreeOfParallelism; taskNumber++)
            {
                // capturing taskNumber in lambda wouldn't work correctly
                int taskNumberCopy = taskNumber;

                tasks[taskNumber] = Task.Factory.StartNew(
                    () =>
                    {
                        ScrapingBrowser Browser = new ScrapingBrowser();
                        Browser.AllowAutoRedirect = true; // Browser has many settings you can access in setup
                        Browser.AllowMetaRedirect = true;
                        WebPage PageResult;

                        var max = throttle + (newestId - throttle) * (taskNumberCopy + 1) / degreeOfParallelism;
                        var min = throttle + (newestId - throttle) * (taskNumberCopy) / degreeOfParallelism;
                        logger.InfoFormat("max-min:{0}-{1}",max, min);
                        for (int i = min; i < max; i++)
                        {
                            var url = $"{baseUrl}?t={i}";
                            PageResult = Browser.NavigateToPage(new Uri(url));
                            RemoveComment(PageResult.Html);
                            var navbars = PageResult.Html.CssSelect(".navbar");

                            string title = "";
                            if (navbars != null && navbars.Any())
                            {
                                title = navbars.Last().InnerText.Trim();
                                logger.Warn(title);
                            }

                            var rawPosts = PageResult.Html.CssSelect(".voz-post-message");

                            List<Post> Posts = new List<Post>();
                            foreach (var rawPost in rawPosts)
                            {
                                Posts.Add(new Post(rawPost.InnerHtml));
                            }

                            if (!string.IsNullOrEmpty(title) && Posts.Any())
                            {
                                repository.Create<Advert>(new Advert()
                                {
                                    Title = title,
                                    URL = "https://vozforums.com/showthread.php?t=6134837",
                                    Posts = Posts
                                });

                            }
                        }
                    });
            }

            Task.WaitAll(tasks);
        }
    }
}
