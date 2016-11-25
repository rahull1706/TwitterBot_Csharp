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
//using HtmlRender.WinForms;
using System.Drawing;
using System.Windows.Forms;
using TheArtOfDev.HtmlRenderer;
using System.Drawing.Imaging;

namespace TwitterBot_Csharp
{
    class Program
    {
        static void Main(string[] args)
        {

            string url = "https://www.poetryfoundation.org/poems-and-poets/poems/detail/57789"; //90621

            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string htmlString = client.DownloadString(url);

            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(htmlString);

            HtmlNode poemDiv = doc.DocumentNode.SelectSingleNode("//div[@class='poem']");
            HtmlNode titleSpan = doc.DocumentNode.SelectSingleNode("//span[@class='hdg hdg_1']");
            HtmlNode authSpan = doc.DocumentNode.SelectSingleNode("//span[@class='hdg hdg_utility']");

            string htmlConcat =
                "<em>" + titleSpan.InnerText.ToString() + @"</em>"
                + authSpan.InnerText.ToString()
                + poemDiv.OuterHtml.ToString();


            Image image = TheArtOfDev.HtmlRenderer.WinForms.HtmlRender.RenderToImage(htmlConcat);

            image.Save(@"C:\Users\kirkbozeman\Desktop\test.png");
            System.IO.File.WriteAllText(@"C:\Users\kirkbozeman\Desktop\Poem.txt", htmlConcat);

            //            TwitterConnect.PostToBot("this post");         
            //  Console.ReadKey();
        }
    }
}
