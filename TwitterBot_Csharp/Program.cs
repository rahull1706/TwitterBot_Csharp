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

            // create a config file for your creds so you can publish to GitHub
            // add epigraphs for poetry foundation
                        
            try
            {
                Random rnd = new Random();
                int rndMethod = rnd.Next(1, 2);

//                rndMethod = 2;

                Image image;

                if (rndMethod == 1)
                {
                    image = TwitterConnect.PoetryFoundationRndmToImage();
                    TwitterConnect.PostToBot("", image.ToStream(ImageFormat.Png));
                }
                if (rndMethod == 2)
                {
                    image = TwitterConnect.PublicDomainPoetryRndmToImage();
                    TwitterConnect.PostToBot("", image.ToStream(ImageFormat.Png));
                }

                TwitterConnect.FollowPoetryHashtaggers(1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
            
        }
    }
}
