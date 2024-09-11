using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace wpfdatabase
{
    internal class NetController
    {
        public static async Task<string> GetAllUsers()
        {
            HttpClient client = new HttpClient();

           
            return await client.GetAsync("https://localhost:7136/api/Users").Result.Content.ReadAsStringAsync();
        }

        public static async Task WriteUser(User user)
        {
            HttpClient client = new HttpClient();
            await client.GetAsync($"https://localhost:7136/api/AddUser?name={user.Name}&email={user.Email}&age={user.Age}").Result.Content.ReadAsByteArrayAsync();
           
        }
    }
}
