using Mercury.Shared.API;
using Mercury.Shared.Models.Mercury;
using Newtonsoft.Json;
using System.Net;

namespace Mercury.Client.Services
{


    public class PersonManager : APIRepository<Person>
    {
        public PersonManager(HttpClient _http)
            : base(_http, "Person", nameof(Person.PersonID))
        { }

        public async Task<APIEntityResponse<Person>> GetPersonByEmail(string email)
        {
            try
            {
                var url = $"{base.controllerName}/GetPersonByEmail/{WebUtility.HtmlEncode(email)}";
                var result = await base.http.GetAsync(url);
                result.EnsureSuccessStatusCode();
                var responseBody = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<APIEntityResponse<Person>>(responseBody);


                if (response is not null && response.Success)
                    return new APIEntityResponse<Person>()
                    {
                        Success = true,
                        Data = response.Data
                    };
                else
                {
                    return new APIEntityResponse<Person>()
                    {
                        Success = false,
                        ErrorMessages = new List<string>() {"Not found"}
                    };
                }
            }
            catch (Exception ex)
            {
                return new APIEntityResponse<Person>()
                {
                    Success = false,
                    ErrorMessages = new List<string>() { ex.Message }
                };
            }
        }
    }
}
