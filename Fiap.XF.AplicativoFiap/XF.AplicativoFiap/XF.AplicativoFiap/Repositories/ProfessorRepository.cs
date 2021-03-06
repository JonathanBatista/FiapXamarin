﻿using Newtonsoft.Json;
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

        private static string uriString = "http://apiaplicativofiap.azurewebsites.net/";

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

        public static async Task<bool> PutProfessorSqlAzureAsync(Professor professorEdit)
        {
            if (professorEdit == null)
                return false;

            using (var httpRequest = new HttpClient())
            {

                httpRequest.BaseAddress = new Uri(uriString);
                httpRequest.DefaultRequestHeaders.Accept.Clear();

                string profJson = JsonConvert.SerializeObject(professorEdit);

                var response = await httpRequest.PutAsync($"api/professors/{professorEdit.Id}",
                        new StringContent(profJson, Encoding.UTF8, "application/json"));
                
                if (response.IsSuccessStatusCode)
                    return true;

                return false;
            }
        }

        public static async Task<bool> PostProfessorSqlAzureAsync(Professor profAdd)
        {            
            if (profAdd == null)
                return false;

            using (var httpRequest = new HttpClient())
            {
                httpRequest.BaseAddress = new Uri(uriString);
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
