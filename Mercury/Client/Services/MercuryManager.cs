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
    }
}
