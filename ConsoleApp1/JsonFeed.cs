using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class JsonFeed
    {
        private readonly HttpClient client = new HttpClient();

        public JsonFeed() { }

        public JsonFeed(string endpoint)
        {
            client.BaseAddress = new Uri(endpoint);
        }

        /// <summary>
        /// Retrieves random chuck norris jokes based on category, replacing the name Chuck Norris with the provided firstName and lastName
        /// </summary>
        /// <returns>a string array of random chuck norris jokes</returns>
        public string[] GetRandomJokes(string firstname, string lastname, string category, int numberOfJokes)
		{			
			string requestUrl = "jokes/random";
            if (category != null)
            {
                if (requestUrl.Contains('?'))
                {
                    requestUrl += "&";
                }
                else
                {
                    requestUrl += "?category=" + category;
                }
			}

            List<String> jokes = new List<string>();
            for (int i = 0; i < numberOfJokes; i++)
            {
                string joke = Task.FromResult(client.GetStringAsync(requestUrl).Result).Result;

                if (firstname != null && lastname != null)
                {
                    joke = joke.Replace("Chuck Norris", firstname + " " + lastname);
                    joke = joke.Replace("Chuck", firstname);
                    joke = joke.Replace("Norris", lastname);                
                }

                jokes.Add(joke);
            }

            return jokes.ToArray();
        }

        /// <summary>
        /// Retrieves a random name
        /// </summary>
        /// <returns>an object that contains name and surname</returns>
		public dynamic Getname()
		{
            var result = client.GetStringAsync("").Result;
            return JsonConvert.DeserializeObject<dynamic>(result);
        }

        /// <summary>
        /// Retrieves available joke categories
        /// </summary>
        /// <returns>a string array of categories</returns>
		public string[] GetCategories()
		{
			return new string[] { Task.FromResult(client.GetStringAsync("jokes/categories").Result).Result };
		}
    }
}
