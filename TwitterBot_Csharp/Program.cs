﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;

namespace TwitterBot_Csharp
{
    class Program
    {
        static void Main(string[] args)
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


            service.SendTweet(new SendTweetOptions {Status = "Hello world. #helloworld"});

            var tweets = service.Search(new SearchOptions { Q = "trump", Count = 25, Resulttype = TwitterSearchResultType.Recent, IncludeEntities = false});

//            var tweets = service.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions());
            foreach (var tweet in tweets.Statuses)
            {
                Console.WriteLine("{0} says '{1}'", tweet.User.ScreenName, tweet.Text);
            }
            Console.ReadKey();
            

        }
    }
}