using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PhotoAlbum
{
    public class Photo
    {
        public int AlbumId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ThumbnailUrl { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();
        }
        static HttpClient client = new HttpClient();        
        static async Task RunAsync()
        {
            client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/photos");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            Photo[] photo = new Photo[] { };
            photo = await GetPhotoAsync("?albumId=2");
            ShowDetails(photo);
        }
        static async Task<Photo[]> GetPhotoAsync(string path)
        {
            Photo[] photo = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                photo = await response.Content.ReadAsAsync<Photo[]>();
            }
            return photo;
        }
        static void ShowDetails(Photo[] photos)
        {
            foreach (Photo photo in photos)
            {
                Console.WriteLine($"[{photo.Id}] {photo.Title}");
            }
        }
    }
}
