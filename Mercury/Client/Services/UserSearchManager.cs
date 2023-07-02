using Mercury.Shared.API;
using Mercury.Shared.Models;
using Mercury.Shared.Models.AspNetUser;
using Newtonsoft.Json;
using System.Net;

namespace Mercury.Client.Services
{
    public class UserSearchManager : APIRepository<UserSearch>
    {
        HttpClient http;

        public UserSearchManager(HttpClient _http)
            : base(_http, "AppUsers", "AspNetUserID")
        {
            http = _http;
        }

        public async Task<IEnumerable<AspNetUser>> GetAspNetUsers( )
        {
            try
            {
                //var arg = WebUtility.HtmlEncode(email);
                var url = $"appusers/GetAllAspNetUsers";
                var result = await http.GetAsync(url);
                result.EnsureSuccessStatusCode();
    
                string responseBody = await result.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
                var response = JsonConvert.DeserializeObject<APIListOfEntityResponse<AspNetUser>>(responseBody);
                if (response.Success)
                    return response.Data;
                else
                    return new List<AspNetUser>();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }


        public async Task<IEnumerable<UserSearch>> SearchByEmail(string email)
        {
            try
            {
                var arg = WebUtility.HtmlEncode(email);
                var url = $"appusers/{arg}/searchbyemail";
                var result = await http.GetAsync(url);
                result.EnsureSuccessStatusCode();
                string responseBody = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<APIListOfEntityResponse<UserSearch>>(responseBody);
                if (response.Success)
                    return response.Data;
                else
                    return new List<UserSearch>();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }


    }
}
