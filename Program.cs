using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiClient
{
    class Program
    {

        class Film
        {
            [JsonPropertyName("id")]
            public string id { get; set; }

            [JsonPropertyName("title")]
            public string FilmTitle { get; set; }

            [JsonPropertyName("original_title")]
            public string JapaneseTitle { get; set; }

            [JsonPropertyName("original_title_romanised")]
            public string PhoneticTitle { get; set; }

            [JsonPropertyName("description")]
            public string description { get; set; }

            [JsonPropertyName("director")]
            public string director { get; set; }

            [JsonPropertyName("producer")]
            public string producer { get; set; }

            [JsonPropertyName("release_date")]
            public string YearReleased { get; set; }

            [JsonPropertyName("running_time")]
            public string RunningTimeInMinutes { get; set; }

            [JsonPropertyName("rt_score")]
            public string RottenTomatoScore { get; set; }

        }

        //            Task instead of void 
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var client = new HttpClient();
            //                                    change string into stream 
            var responseAsStream = await client.GetStreamAsync("https://ghibliapi.herokuapp.com/films");

            //async means it can do multiple things so the program will keep running while its waiting to get a
            // response from the URL this si important because we want things to keep updating the whole time this is happening(ex: video games)
            //javascript is asynchronous in nature

            // Supply that *stream of data* to a Deserialize that will interpret it as a List of Item objects.
            var films = await JsonSerializer.DeserializeAsync<List<Film>>(responseAsStream);

            //gets the index of the movies so you dont have to make a separate variable 
            Console.WriteLine("Here is a list of all of the Studio Ghilbi films");
            //why do you need the film part and i get hwo you make up a variable to use (item) but why don't you change the films to find its index 
            foreach (var (film, index) in films.Select((item, index) => (item, index)))
            {
                Console.WriteLine($"{index + 1}. {film.FilmTitle} - {film.JapaneseTitle}");
            }
            Console.WriteLine("Which film would you like to learn more about (1-films.Count())");
            var filmIndex = Int32.Parse(Console.ReadLine()) - 1;

            var selectedFile = films[filmIndex];
            Console.WriteLine(films[filmIndex].FilmTitle);


        }
    }
}
