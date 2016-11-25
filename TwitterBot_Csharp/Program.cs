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

            //create a config file for your creds so you can publish to GitHub

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
                    strPoems.Add(href); // put just links in list
                }          
            }


            int rndPoem = rnd.Next(1, strPoems.Count);

            Console.WriteLine("http://www.public-domain-poetry.com/" + strPoems[rndPoem]);

            /*            foreach (var str in strPoems)
            {
                Console.WriteLine(str);
            }*/

            Console.ReadKey();


            

            /*

            try
            {
                Image image = TwitterConnect.PoetryFoundationRndmToImage();
                TwitterConnect.PostToBot("", image.ToStream(ImageFormat.Png));
                TwitterConnect.FollowPoetryHashtaggers(5);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
            */
        }
    }
}
