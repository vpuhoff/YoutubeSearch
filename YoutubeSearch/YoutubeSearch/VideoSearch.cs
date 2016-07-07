// YoutubeSearch
// YoutubeSearch is a library for .NET, written in C#, to show search query results from YouTube.
//
// (c) 2016 Torsten Klinger - torsten.klinger(at)googlemail.com
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program. If not, see<http://www.gnu.org/licenses/>.

using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace YoutubeSearch
{
    public class VideoSearch
    {
        List<VideoInformation> items;

        WebClient webclient;

        string title;
        string author;
        string description;
        string duration;
        string url;
        string thumbnail;

        /// <summary>
        /// Doing search query with given parameters. Returns a List<> object.
        /// </summary>
        /// <param name="querystring"></param>
        /// <param name="querypages"></param>
        /// <returns></returns>
        public List<VideoInformation> SearchQuery(string querystring, int querypages)
        {
            items = new List<VideoInformation>();

            webclient = new WebClient();

            // Do search
            for (int i = 1; i <= querypages; i++)
            {
                // Search address
                string html = webclient.DownloadString("https://www.youtube.com/results?search_query=" + querystring + "&page=" + i);

                // Search string
                string pattern = "<div class=\"yt-lockup-content\">.*?title=\"(?<NAME>.*?)\".*?</div></div></div></li>";
                MatchCollection result = Regex.Matches(html, pattern, RegexOptions.Singleline);

                for (int ctr = 0; ctr <= result.Count - 1; ctr++)
                {
                    // Title
                    title = result[ctr].Groups[1].Value;

                    // Author
                    author = VideoItemHelper.cull(result[ctr].Value, "/user/", "class").Replace('"', ' ').TrimStart().TrimEnd();

                    // Description
                    description = VideoItemHelper.cull(result[ctr].Value, "dir=\"ltr\" class=\"yt-uix-redirect-link\">", "</div>");

                    // Duration
                    duration = VideoItemHelper.cull(VideoItemHelper.cull(result[ctr].Value, "id=\"description-id-", "span"), ": ", "<");

                    // Url
                    url = string.Concat("http://www.youtube.com/watch?v=", VideoItemHelper.cull(result[ctr].Value, "watch?v=", "\""));

                    // Thumbnail
                    thumbnail = "https://i.ytimg.com/vi/" + VideoItemHelper.cull(result[ctr].Value, "watch?v=", "\"") + "/mqdefault.jpg";

                    // Remove playlists
                    if (title != "__title__")
                    {
                        if (duration != "")
                        {
                            // Add item to list
                            items.Add(new VideoInformation() { Title = title, Author = author, Description = description, Duration = duration, Url = url, Thumbnail = thumbnail,  });
                        }
                    }
                }
            }

            return items;
        }
    }
}
