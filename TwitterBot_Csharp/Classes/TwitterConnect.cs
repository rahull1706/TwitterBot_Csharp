using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Drawing.Imaging;
using TweetSharp;

namespace TwitterBot_Csharp.Classes
{
    public static class TwitterConnect
    {
        public static Stream ToStream(this Image image, ImageFormat format)
        {
            var stream = new System.IO.MemoryStream();
            image.Save(stream, format);
            stream.Position = 0;
            return stream;
        }

        public static HtmlAgilityPack.HtmlDocument LinkToHtmlDoc(string url, bool utf8 = false) // default to non-utf8 unless made explicit
        {
            WebClient client = new WebClient();
            if (utf8 == true)
            {
                client.Encoding = Encoding.UTF8;
            }// to deal w/ odd chars
            string htmlString = client.DownloadString(url);
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(htmlString);
            return doc;
        }

        public static Image PublicDomainPoetryRndmToImage()
        {

            //get random page
            Random rnd = new Random();
            int page = rnd.Next(1, 500);
            string url = "http://www.public-domain-poetry.com/listpoetry.php?letter=All&page=" + page.ToString();

            var doc = TwitterConnect.LinkToHtmlDoc(url);

            HtmlNodeCollection poemLinks = doc.DocumentNode.SelectNodes("//a"); // get all links
            List<string> strPoems = new List<string>();

            foreach (var link in poemLinks)
            {
                var href = link.Attributes["href"].Value;
                if (href.ToString().Contains("php") == false
                    && href.ToString().Contains("http") == false
                    && href.ToString().Substring(href.Length - 1) != @"/"
                    && href.ToString().Any(char.IsDigit))
                {
                    strPoems.Add(href); // get just hrefs
                }
            }

            //get random poem
            int rndPoem = rnd.Next(1, strPoems.Count);
            url = "http://www.public-domain-poetry.com/" + strPoems[rndPoem];
            doc = TwitterConnect.LinkToHtmlDoc(url);

            HtmlNode title = doc.DocumentNode.SelectSingleNode("//font[@class='t0']");
            HtmlNode poem = doc.DocumentNode.SelectSingleNode("//font[@class='t3a']");

            string justTitle = title.InnerText.Substring(0, title.InnerText.LastIndexOf(" by "));
            string justAuthor = title.InnerText.Substring(title.InnerText.LastIndexOf(" by ")+1);

            string htmlConcat = @"<div style=""text-indent: -1em; padding-left: 1em;""> <b>" +
                            justTitle.Replace("Public Domain Poetry - ", "").ToUpper() + @"</b> " + 
                            justAuthor.ToUpper() + @"<br><br>" + 
                            poem.OuterHtml + @"</div>";

            Image image = TheArtOfDev.HtmlRenderer.WinForms.HtmlRender.RenderToImage(htmlConcat);

            return image;

        }

        public static Image PoetryFoundationRndmToImage()
        {

            Random rnd = new Random();
            int poemInt = rnd.Next(50100, 58800);

            string url = "https://www.poetryfoundation.org/poems-and-poets/poems/detail/" + poemInt.ToString(); //90621


            var doc = LinkToHtmlDoc(url, true);

            HtmlNode poemDiv = doc.DocumentNode.SelectSingleNode("//div[@class='poem']");
            HtmlNode titleSpan = doc.DocumentNode.SelectSingleNode("//span[@class='hdg hdg_1']");
            HtmlNode authSpan = doc.DocumentNode.SelectSingleNode("//span[@class='hdg hdg_utility']");

            string htmlConcat =

                    @"<div style=""text-indent: -1em; padding-left: 1em;""> <b>" + titleSpan.InnerText.ToString().ToUpper() + @"</b>"
                + authSpan.InnerText.ToString().ToUpper() + @"</div><br>"
                + poemDiv.OuterHtml.ToString();


            Image image = TheArtOfDev.HtmlRenderer.WinForms.HtmlRender.RenderToImage(htmlConcat);

            return image;

        }


        public static void PostToBot(string post, Stream stream)
        {
            /* TweetSharp is no longer being updated but is necessary. TweetMoaSharp is the current
            updated package. Use Update-Package TweetMoaSharp to update it. Use Update-Package -reinstall TweetMoaSharp
            to reinstall the entire package. It must be installed ON TOP of existing final TweetSharp package.
             */

/*            string _consumerKey = "licUTuwmj4J16pMy30UFQZofI";
            string _consumerSecret = "LuLn34qrT7iczsga7RvtJnHf1jncDH4oJU70Btzj9LTz8rjA5E";
            string _accessToken = "801871349434187776-mcvC35KftjhlR7doV6l6UQzrAWIFbdz";
            string _accessTokenSecret = "AWClOy005tBXiyxnFUlLVAuDhu269YI5vyh5HSW1mUA2D";*/

            string _consumerKey = ConfigurationManager.AppSettings["consumerKey"];
            string _consumerSecret = ConfigurationManager.AppSettings["consumerSecret"];
            string _accessToken = ConfigurationManager.AppSettings["accessToken"];
            string _accessTokenSecret = ConfigurationManager.AppSettings["accessTokenSecret"];


            var service = new TwitterService(_consumerKey, _consumerSecret);
            service.AuthenticateWith(_accessToken, _accessTokenSecret);
            
            service.SendTweetWithMedia(new SendTweetWithMediaOptions
            {
//                Status = post,
                Images = new Dictionary<string, Stream> { { "john", stream } }
            });

//            service.SendTweet(new SendTweetOptions { Status = post });

        }

        public static void FollowPoetryHashtaggers(int cnt)
        {

            string _consumerKey = ConfigurationManager.AppSettings["consumerKey"];
            string _consumerSecret = ConfigurationManager.AppSettings["consumerSecret"];
            string _accessToken = ConfigurationManager.AppSettings["accessToken"];
            string _accessTokenSecret = ConfigurationManager.AppSettings["accessTokenSecret"];

            var service = new TwitterService(_consumerKey, _consumerSecret);
            service.AuthenticateWith(_accessToken, _accessTokenSecret);

            var tweets = service.Search(new SearchOptions { Q = "#poetry", Count = cnt, Resulttype = TwitterSearchResultType.Popular, IncludeEntities = false });

            foreach (var tweet in tweets.Statuses)
            {
//                Console.WriteLine(tweet.User.ScreenName);
                service.FollowUser(new FollowUserOptions { UserId = tweet.User.Id } );
            }

        }

    }
}
