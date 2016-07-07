using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeSearch;

namespace Example_Application
{
    class Program
    {
        static void Main(string[] args)
        {
            // Keyword
            string querystring = "test";

            // Number of result pages
            int querypages = 1;

            ////////////////////////////////
            // Starting searchquery
            ////////////////////////////////

            var items = new VideoSearch();

            int i = 1;

            foreach (var item in items.SearchQuery(querystring, querypages))
            {
                Console.WriteLine(i + "###########################");
                Console.WriteLine("Title: " + item.Title);
                Console.WriteLine("Author: " + item.Author);
                Console.WriteLine("Description: " + item.Description);
                Console.WriteLine("Duration: " + item.Duration);
                Console.WriteLine("Url: " + item.Url);
                Console.WriteLine("Thumbnail: " + item.Thumbnail);
                Console.WriteLine("");

                i++;
            }

            Console.ReadLine();
        }
    }
}
