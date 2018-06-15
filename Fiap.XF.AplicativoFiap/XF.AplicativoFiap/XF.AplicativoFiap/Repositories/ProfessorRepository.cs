using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using XF.AplicativoFiap.Models;

namespace XF.AplicativoFiap.Repositories
{
    public class ProfessorRepository
    {
        private static List<Professor> professoresSqlAzure;

        public static async Task<List<Professor>> GetProfessoresSqlAzureAsync(bool update)
        {
            if (professoresSqlAzure != null && !update)
                return professoresSqlAzure;
            
            using (var httpRequest = new HttpClient())
            {
                var stream = await httpRequest.GetStreamAsync("http://apiaplicativofiap.azurewebsites.net/api/professors");

                var professorSerializer = new DataContractJsonSerializer(typeof(List<Professor>));

                professoresSqlAzure = (List<Professor>)professorSerializer.ReadObject(stream);

                return professoresSqlAzure;
            }
            
        }

        public static async Task<bool> PostProfessorSqlAzureAsync(Professor profAdd)
        {

            try
            {
                if (profAdd == null)
                    return false;

                using (var httpRequest = new HttpClient())
                {
                    httpRequest.BaseAddress = new Uri("http://apiaplicativofiap.azurewebsites.net/");
                    httpRequest.DefaultRequestHeaders.Accept.Clear();

                    httpRequest.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string profJson = JsonConvert.SerializeObject(profAdd);
                    var response = await httpRequest.PostAsync("api/professors",
                                            new StringContent(profJson, Encoding.UTF8, "application/json"));


                    if (response.IsSuccessStatusCode)
                        return true;

                    return false;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
                      
        }

        public static async Task<bool> DeleteProfessorSqlAzureAsync(string profId)
        {
            if (string.IsNullOrWhiteSpace(profId))
                return false;
            
            using (var httpRequest = new HttpClient())
            {
                httpRequest.BaseAddress = new Uri("http://apiaplicativofiap.azurewebsites.net/");

                httpRequest.DefaultRequestHeaders.Accept.Clear();

                httpRequest.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpRequest.DeleteAsync(string.Format("api/professors/{0}", profId));

                if (response.IsSuccessStatusCode)
                    return true;

                return false;
            }            
        }
    }
}
