using Mercury.Shared.API;
using Mercury.Shared.Models.Mercury;
using Mercury.Shared.Repository;
using Newtonsoft.Json;
using System.Net;

namespace Mercury.Client.Services
{



    public class PersonManager : APIRepository<Person>
    {

        public PersonManager(HttpClient _http)
            : base(_http, "Person", nameof(Person.PersonID))
        {

        }


        public async Task<APIEntityResponse<Person>> GetPersonByEmail(string email)
        {
            try
            {

                var expression = new QueryFilter<Person>();

                expression.FilterProperties.Add(new FilterProperty { Name = "Email", Value = email, Operator = FilterOperator.Contains, CaseSensitive = true });


                var result = (await Get(expression)).FirstOrDefault();

                return new APIEntityResponse<Person>()
                {
                    Success = result is not null,
                    Data = result,
                };


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
