using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;
using TwitterBot_Csharp.Classes;
using HtmlAgilityPack;
using CsQuery;
using System.Net;

namespace TwitterBot_Csharp
{
    class Program
    {
        static void Main(string[] args)
        {

            string url = "https://www.poetryfoundation.org/poems-and-poets/poems/detail/90621";

            WebClient client = new WebClient();
            string htmlString = client.DownloadString(url);

            var doc = new HtmlDocument();
            doc.LoadHtml(htmlString);

            HtmlNode rateNode = doc.DocumentNode.SelectSingleNode("//div[@class='poem']");
            Console.WriteLine(rateNode.InnerText);

//            HtmlNode specificNode = doc.GetElementbyId("poem");
            //            HtmlNodeCollection nodesMatchingXPath = doc.DocumentNode.SelectNodes("x/path/nodes");
//            Console.WriteLine(specificNode.InnerText);
            //            TwitterConnect.PostToBot("this post");         
            Console.ReadKey();
        }
    }
}
