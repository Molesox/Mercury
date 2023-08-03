using Mercury.Shared.API;
using Mercury.Shared.Models.Mercury;
using Newtonsoft.Json;

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
                var result = await base.http.GetAsync($"{base.controllerName}/GetPersonByEmail/{email}");
                result.EnsureSuccessStatusCode();
                var responseBody = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<APIListOfEntityResponse<Person>>(responseBody);


                if (response is not null && response.Success)
                    return new APIEntityResponse<Person>()
                    {
                        Success = true,
                        Data = response.Data.FirstOrDefault()
                    };
                else
                {
                    return new APIEntityResponse<Person>()
                    {
                        Success = false,
                        ErrorMessages = new List<string>() { result.ReasonPhrase }
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
