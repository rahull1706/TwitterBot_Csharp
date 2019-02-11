using System;
using System.ServiceProcess;
using System.Timers;
using TwitterBot_Csharp.Classes;
using System.Drawing;
using System.Drawing.Imaging;

namespace WindowsService1
{
    public partial class Scheduler : ServiceBase
    {

        private Timer timer1 = null; 

        public Scheduler()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer1 = new Timer(60*1000); //7200000; // I think this is every two hours
            timer1.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            timer1.Start();

            try
            {
                Random rnd = new Random();
                int rndMethod = rnd.Next(1, 2);

                //                rndMethod = 3;

                Image image;

                if (rndMethod == 1)
                {
                    image = PoetryBot.PoetryFoundationRndmToImage();
                    PoetryBot.PostToBot("", image.ToStream(ImageFormat.Png));
                }
                if (rndMethod == 2)
                {
                    image = PoetryBot.PublicDomainPoetryRndmToImage();
                    PoetryBot.PostToBot("", image.ToStream(ImageFormat.Png));
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }

            PoetryBot.FollowPoetryHashtaggers(1);
            PoetryBot.FollowBackNotFollowed();

        }

        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            // ignore the time, just compare the date
            if (_lastRun.Date < DateTime.Now.Date)
            {
                // stop the timer while we are running the cleanup task
                _timer.Stop();
                //
                // do cleanup stuff
                //
                _lastRun = DateTime.Now;
                _timer.Start();
            }
        }


        protected override void OnStop()
        {
        }
    }
}
