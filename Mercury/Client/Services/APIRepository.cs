using Mercury.Shared.API;
using Mercury.Shared.Repository;
using System.Net.Http.Json;
using System.Net;
using Newtonsoft.Json;

namespace Mercury.Client.Services
{
    public class APIRepository<TEntity> : IRepository<TEntity>
      where TEntity : class
    {
        protected string controllerName;
        protected string primaryKeyName;
        protected HttpClient http;

        public APIRepository(HttpClient _http, string _controllerName, string _primaryKeyName)
        {
            http = _http;
            controllerName = _controllerName;
            primaryKeyName = _primaryKeyName;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            try
            {
                var result = await http.GetAsync(controllerName);
                result.EnsureSuccessStatusCode();

                string responseBody = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<APIListOfEntityResponse<TEntity>>(responseBody);

                if (response is not null && response.Success)
                    return response.Data;
                else
                    return new List<TEntity>();
            }
            catch (Exception ex)
            {
                return new List<TEntity>();
            }
        }

        public async Task<TEntity?> GetByID(object id)
        {
            try
            {
                var encodedID = WebUtility.HtmlEncode(id.ToString());
                var url = @$"{controllerName}/{encodedID}";

                var result = await http.GetAsync(url);
                result.EnsureSuccessStatusCode();

                string responseBody = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<APIEntityResponse<TEntity>>(responseBody);

                if (response is not null && response.Success)
                    return response.Data;
                else
                    return null;
            }
            catch (Exception ex)
            {
                // log error here
                return null;
            }
        }

        public async Task<TEntity?> Insert(TEntity entity)
        {
            try
            {
                var result = await http.PostAsJsonAsync(controllerName, entity);
                result.EnsureSuccessStatusCode();

                string responseBody = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<APIEntityResponse<TEntity>>(responseBody);

                if (response is not null && response.Success)
                    return response.Data!;
                else
                    return null;
            }
            catch (Exception ex)
            {
                // log exception here
                return null;
            }
        }

        public async Task<TEntity?> Update(TEntity entityToUpdate)
        {
            try
            {

                var settings = new System.Text.Json.JsonSerializerOptions
                {
                    ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
                };

                var result = await http.PutAsJsonAsync(controllerName, entityToUpdate, settings);
                result.EnsureSuccessStatusCode();

                string responseBody = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<APIEntityResponse<TEntity>>(responseBody);

                if (response is not null && response.Success)
                    return response.Data!;
                else
                    return null;
            }
            catch (Exception ex)
            {
                // log exception here
                return null;
            }
        }

        public async Task<bool> Delete(TEntity entityToDelete)
        {
            try
            {
                string? idFromEntity = default(string);

                var property = entityToDelete.GetType().GetProperty(primaryKeyName);
                if (property is not null)
                {
                    var propertyValue = property.GetValue(entityToDelete);
                    idFromEntity = propertyValue?.ToString();
                }
                if (idFromEntity is null) throw new Exception("Id missing from entity");

                var encodedID = WebUtility.HtmlEncode(idFromEntity);
                var url = $@"{controllerName}/{encodedID}";

                var result = await http.DeleteAsync(url);
                result.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                // log exception here
                return false;
            }
        }

        public async Task<bool> Delete(object id)
        {
            try
            {
                var encodedID = WebUtility.HtmlEncode(id.ToString());
                var url = @$" {controllerName}/{encodedID}";

                var result = await http.DeleteAsync(url);
                result.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                // log exception here
                return false;
            }
        }
    }
}
