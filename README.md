# YoutubeSearch
YoutubeSearch is a library for .NET, written in C#, to show search query results from YouTube.

# Target platforms
- .NET Framework 4.5 and higher
- Windows Phone 8
- WinRT
- Xamarin.Android
- Xamarin.iOS

# NuGet
**Install-Package YoutubeSearch.dll**

# License
YoutubeSearch is licensed under the **GPL** license.

# Example code
```
// Keyword
string querystring = "test";

// Number of result pages
int querypages = 1;

var items = new VideoSearch();

foreach (var item in items.SearchQuery(querystring, querypages))
{
    Console.WriteLine(item.Title);
}
```

# Supported Items

- Title
- Author
- Description
- Duration
- Thubmnail
- Video Url


