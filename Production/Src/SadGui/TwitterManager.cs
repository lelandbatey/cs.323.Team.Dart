using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using TweetSharp;
//Code used from http://www.d80.co.uk/post/2011/02/13/A-Simple-Twitter-Client-in-C-with-OAUTH-using-TweetSharp.aspx

namespace TwitterMachine
{
    class TwitterManager
    {
        //Our actual "Twitter Object"
        //It's the object from which everything happens.
        private TwitterService service = new TwitterService(ConsumerKey, ConsumerSecret, AccessToken, AccessTokenSecret);

        //Twitter has its own checks for improper tweets.
        public void SendTweet(string twitter_msg) {
            service.SendTweet(new SendTweetOptions() { Status = twitter_msg });

            //After message is sent, the state of service will indicate whether it was successful or not.
            //service.Response.StatusCode seems more appropriate, but can't figure out the type for boolean comparison.
            if (service.Response.StatusDescription == "Unauthorized") {
                Console.WriteLine("Need to include a message with your Tweet Command.");
                Console.WriteLine();
            }
            else if (service.Response.StatusDescription != "OK") {
                //Deserialize makes the generated error message more readable.
                Console.WriteLine(service.Deserialize<TwitterError>(service.Response.Response));
                Console.WriteLine();
            } else {
                Console.WriteLine("Message Successfully Posted.");
                Console.WriteLine();
            }      
        }

        private static string ConsumerSecret {
            get { return ConfigurationManager.AppSettings["ConsumerSecret"]; }
        }
        private static string ConsumerKey {
            get { return ConfigurationManager.AppSettings["ConsumerKey"]; }
        }
        private static string AccessToken {
            get { return ConfigurationManager.AppSettings["AccessToken"]; }
        }
        private static string AccessTokenSecret {
            get { return ConfigurationManager.AppSettings["AccessTokenSecret"]; }
        }
    }
}
