using System;
using TwitterBot_Csharp.Classes;
using System.Drawing;
using System.Drawing.Imaging;

namespace TwitterBot_Csharp
{
    class Program
    {
        static void Main(string[] args)
        {

            // add epigraphs for poetry foundation

            try
            {
                Random rnd = new Random();
                int rndMethod = rnd.Next(1, 4); // this is how you get 1-3 random

                Image image;

                if (rndMethod == 1) // poetry foundation
                {
                    image = PoetryBot.PoetryFoundationRndmToImage();
                    PoetryBot.PostToBot("", image.ToStream(ImageFormat.Png));
                }
                if (rndMethod == 2) // public domain poetry
                {
                    image = PoetryBot.PublicDomainPoetryRndmToImage();
                    PoetryBot.PostToBot("", image.ToStream(ImageFormat.Png));
                }
                if (rndMethod == 3) //poets.org
                {
                    image = PoetryBot.PoetsorgToImage();
                    PoetryBot.PostToBot("", image.ToStream(ImageFormat.Png));
                }

        //      PoetryBot.LogTweetInfo();


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }

            PoetryBot.FollowPoetryHashtaggers(1);
            PoetryBot.FollowBackNotFollowed();
            
        }
    }
}
