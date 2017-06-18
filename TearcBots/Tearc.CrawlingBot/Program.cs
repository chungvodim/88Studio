using Abot.Crawler;
using Abot.Poco;
using DryIoc;
using log4net;
using System;
using System.Net;
using Tearc.Data.Entity;
using Tearc.Repository;

namespace Tearc.CrawlingBot
{
    class Program
    {
        static IRepository repository;
        static ILog logger = LogManager.GetLogger(typeof(Program));
        const int NEWEST_ID = 1000;//6209153

        static void Main(string[] args)
        {
            try
            {
                log4net.Config.XmlConfigurator.Configure();
                PrintDisclaimer();

                var container = new Container(Rules.Default.WithTrackingDisposableTransients());
                //var container = new Container();
                Bootstrapper.DryIoC.Configure(container);
                repository = container.Resolve<IMongoRepository>();

                IWebCrawler crawler;

                //Uncomment only one of the following to see that instance in action
                //crawler = GetDefaultWebCrawler();
                //crawler = GetManuallyConfiguredWebCrawler();
                crawler = GetCustomBehaviorUsingLambdaWebCrawler();

                //Subscribe to any of these asynchronous events, there are also sychronous versions of each.
                //This is where you process data about specific events of the crawl
                crawler.PageCrawlStartingAsync += crawler_ProcessPageCrawlStarting;
                crawler.PageCrawlCompletedAsync += crawler_ProcessPageCrawlCompleted;
                crawler.PageCrawlDisallowedAsync += crawler_PageCrawlDisallowed;
                crawler.PageLinksCrawlDisallowedAsync += crawler_PageLinksCrawlDisallowed;



                //Start the crawl
                //This is a synchronous call
                Uri uriToCrawl = GetSiteToCrawl(args);
                CrawlResult result = crawler.Crawl(uriToCrawl);

                //Now go view the log.txt file that is in the same directory as this executable. It has
                //all the statements that you were trying to read in the console window :).
                //Not enough data being logged? Change the app.config file's log4net log level from "INFO" TO "DEBUG"

                PrintDisclaimer();
            }
            catch (Exception ex)
            {
                PrintAttentionText(ex.ToString());
            }
        }

        private static IWebCrawler GetDefaultWebCrawler()
        {
            return new PoliteWebCrawler();
        }

        private static IWebCrawler GetManuallyConfiguredWebCrawler()
        {
            //Create a config object manually
            CrawlConfiguration config = new CrawlConfiguration();
            config.CrawlTimeoutSeconds = 0;
            config.DownloadableContentTypes = "text/html, text/plain";
            config.IsExternalPageCrawlingEnabled = false;
            config.IsExternalPageLinksCrawlingEnabled = false;
            config.IsRespectRobotsDotTextEnabled = false;
            config.IsUriRecrawlingEnabled = false;
            config.MaxConcurrentThreads = 10;
            config.MaxPagesToCrawl = 10;
            config.MaxPagesToCrawlPerDomain = 0;
            config.MinCrawlDelayPerDomainMilliSeconds = 1000;

            //Add you own values without modifying Abot's source code.
            //These are accessible in CrawlContext.CrawlConfuration.ConfigurationException object throughout the crawl
            config.ConfigurationExtensions.Add("Somekey1", "SomeValue1");
            config.ConfigurationExtensions.Add("Somekey2", "SomeValue2");

            //Initialize the crawler with custom configuration created above.
            //This override the app.config file values
            return new PoliteWebCrawler(config, null, null, null, null, null, null, null, null);
        }

        private static IWebCrawler GetCustomBehaviorUsingLambdaWebCrawler()
        {
            IWebCrawler crawler = GetDefaultWebCrawler();

            //Register a lambda expression that will make Abot not crawl any url that has the word "ghost" in it.
            //For example http://a.com/ghost, would not get crawled if the link were found during the crawl.
            //If you set the log4net log level to "DEBUG" you will see a log message when any page is not allowed to be crawled.
            //NOTE: This is lambda is run after the regular ICrawlDecsionMaker.ShouldCrawlPage method is run.
            crawler.ShouldCrawlPage((pageToCrawl, crawlContext) =>
            {
                //if (!pageToCrawl.Uri.AbsoluteUri.Contains("showthread.php?t="))
                //    return new CrawlDecision { Allow = false };

                return new CrawlDecision { Allow = true };
            });

            //Register a lambda expression that will tell Abot to not download the page content for any page after 5th.
            //Abot will still make the http request but will not read the raw content from the stream
            //NOTE: This lambda is run after the regular ICrawlDecsionMaker.ShouldDownloadPageContent method is run
            crawler.ShouldDownloadPageContent((crawledPage, crawlContext) =>
            {
                if (crawlContext.CrawledCount >= 5)
                    return new CrawlDecision { Allow = false, Reason = "We already downloaded the raw page content for 5 pages" };

                //if (crawledPage.Uri.AbsoluteUri.Contains("showthread.php?t="))
                //    return new CrawlDecision { Allow = true };

                return new CrawlDecision { Allow = true };
            });

            //Register a lambda expression that will tell Abot to not crawl links on any page that is not internal to the root uri.
            //NOTE: This lambda is run after the regular ICrawlDecsionMaker.ShouldCrawlPageLinks method is run
            crawler.ShouldCrawlPageLinks((crawledPage, crawlContext) =>
            {
                if (!crawledPage.IsInternal)
                    return new CrawlDecision { Allow = false, Reason = "We dont crawl links of external pages" };

                //if (crawledPage.Uri.AbsoluteUri.Contains("showthread.php?t="))
                //    return new CrawlDecision { Allow = true };

                return new CrawlDecision { Allow = true };
            });

            return crawler;
        }

        private static Uri GetSiteToCrawl(string[] args)
        {
            string userInput = "";
            if (args.Length < 1)
            {
                //System.Console.WriteLine("Please enter ABSOLUTE url to crawl:");
                userInput = "https://vozforums.com/forumdisplay.php?f=68";
                //userInput = System.Console.ReadLine();
            }
            else
            {
                userInput = args[0];
            }

            if (string.IsNullOrWhiteSpace(userInput))
                throw new ApplicationException("Site url to crawl is as a required parameter");

            return new Uri(userInput);
        }

        private static void PrintDisclaimer()
        {
            PrintAttentionText("The demo is configured to only crawl a total of 10 pages and will wait 1 second in between http requests. This is to avoid getting you blocked by your isp or the sites you are trying to crawl. You can change these values in the app.config or Abot.Console.exe.config file.");
        }

        private static void PrintAttentionText(string text)
        {
            ConsoleColor originalColor = System.Console.ForegroundColor;
            System.Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.WriteLine(text);
            System.Console.ForegroundColor = originalColor;
        }

        static void crawler_ProcessPageCrawlStarting(object sender, PageCrawlStartingArgs e)
        {
            PageToCrawl pageToCrawl = e.PageToCrawl;
            Console.WriteLine("About to crawl link {0} which was found on page {1}", pageToCrawl.Uri.AbsoluteUri, pageToCrawl.ParentUri.AbsoluteUri);
        }

        static void crawler_ProcessPageCrawlCompleted(object sender, PageCrawlCompletedArgs e)
        {
            CrawledPage crawledPage = e.CrawledPage;

            if (crawledPage.WebException != null || crawledPage.HttpWebResponse.StatusCode != HttpStatusCode.OK)
                Console.WriteLine("Crawl of page failed {0}", crawledPage.Uri.AbsoluteUri);
            else
                Console.WriteLine("Crawl of page succeeded {0}", crawledPage.Uri.AbsoluteUri);

            if (string.IsNullOrEmpty(crawledPage.Content.Text))
                Console.WriteLine("Page had no content {0}", crawledPage.Uri.AbsoluteUri);

            var htmlAgilityPackDocument = crawledPage.HtmlDocument; //Html Agility Pack parser
            var angleSharpHtmlDocument = crawledPage.AngleSharpHtmlDocument; //AngleSharp parser

            if(!htmlAgilityPackDocument.DocumentNode.InnerHtml.Contains("Máy tính để bàn"))
            {
                return;
            }

            var titleNode = htmlAgilityPackDocument.DocumentNode.SelectSingleNode("/html/body/else/div[2]/div/div/table[1]/tbody/tr/td[1]/table/tbody/tr[2]/td/strong");
            var title = titleNode != null ? titleNode.InnerText : "Unknown Title";
            var URL = crawledPage.Uri.AbsoluteUri;

            repository.Create<Advert>(new Advert()
            {
                Title = title,
                URL = URL,
            });
        }

        static void crawler_ProcessPageCrawlCompleted2(object sender, PageCrawlCompletedArgs e)
        {
            CrawledPage crawledPage = e.CrawledPage;

            if (crawledPage.WebException != null || crawledPage.HttpWebResponse.StatusCode != HttpStatusCode.OK)
                Console.WriteLine("Crawl of page failed {0}", crawledPage.Uri.AbsoluteUri);
            else
                Console.WriteLine("Crawl of page succeeded {0}", crawledPage.Uri.AbsoluteUri);

            if (string.IsNullOrEmpty(crawledPage.Content.Text))
                Console.WriteLine("Page had no content {0}", crawledPage.Uri.AbsoluteUri);

            var htmlAgilityPackDocument = crawledPage.HtmlDocument; //Html Agility Pack parser
            var angleSharpHtmlDocument = crawledPage.AngleSharpHtmlDocument; //AngleSharp parser

            var adverts = htmlAgilityPackDocument.DocumentNode.SelectNodes(@"//*[@id=""threadbits_forum_68""]/tr");
            if (adverts != null && adverts.Count > 0)
            {
                PrintAttentionText(string.Format("number of adverts: {0}", adverts.Count));
                foreach (var advert in adverts)
                {
                    try
                    {
                        var fields = advert.SelectNodes("./td");
                        if (fields == null) continue;
                        var mainField = fields[2];
                        var subfileds = mainField.SelectNodes("./div");
                        if (subfileds == null) continue;
                        var links = subfileds[0].SelectNodes("./a");
                        if (links == null) continue;
                        var advertLink = links[0];
                        if (!advertLink.GetAttributeValue("id", "").Contains("thread_title"))
                        {
                            advertLink = links[1];
                            if (!advertLink.GetAttributeValue("id", "").Contains("thread_title"))
                            {
                                advertLink = links[2];
                            }
                        }
                        PrintAttentionText(string.Format("advert Link: {0}", advertLink.InnerHtml));
                        repository.Create<Advert>(new Advert()
                        {
                            Title = advertLink.InnerHtml,
                        });

                    }
                    catch (Exception ex)
                    {
                        PrintAttentionText(ex.ToString());
                    }
                }
            }
        }

        static void crawler_PageLinksCrawlDisallowed(object sender, PageLinksCrawlDisallowedArgs e)
        {
            CrawledPage crawledPage = e.CrawledPage;
            Console.WriteLine("Did not crawl the links on page {0} due to {1}", crawledPage.Uri.AbsoluteUri, e.DisallowedReason);
        }

        static void crawler_PageCrawlDisallowed(object sender, PageCrawlDisallowedArgs e)
        {
            PageToCrawl pageToCrawl = e.PageToCrawl;
            Console.WriteLine("Did not crawl page {0} due to {1}", pageToCrawl.Uri.AbsoluteUri, e.DisallowedReason);
        }
    }
}
