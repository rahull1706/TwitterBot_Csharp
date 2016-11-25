using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

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

        public static Image PoetryFoundationToImage()
        {


        }


        public static void PostToBot(string post, Stream stream)
        {
            /* TweetSharp is no longer being updated but is necessary. TweetMoaSharp is the current
            updated package. Use Update-Package TweetMoaSharp to update it. Use Update-Package -reinstall TweetMoaSharp
            to reinstall the entire package. It must be installed ON TOP of existing final TweetSharp package.
             */

            string _consumerKey = "licUTuwmj4J16pMy30UFQZofI";
            string _consumerSecret = "LuLn34qrT7iczsga7RvtJnHf1jncDH4oJU70Btzj9LTz8rjA5E";
            string _accessToken = "801871349434187776-mcvC35KftjhlR7doV6l6UQzrAWIFbdz";
            string _accessTokenSecret = "AWClOy005tBXiyxnFUlLVAuDhu269YI5vyh5HSW1mUA2D";

            var service = new TwitterService(_consumerKey, _consumerSecret);
            service.AuthenticateWith(_accessToken, _accessTokenSecret);
            
            service.SendTweetWithMedia(new SendTweetWithMediaOptions
            {
//                Status = post,
                Images = new Dictionary<string, Stream> { { "john", stream } }
            });

//            service.SendTweet(new SendTweetOptions { Status = post });

        }


    }
}
