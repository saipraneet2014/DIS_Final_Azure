using DIS_Final_Azure.DataAccess;
using DIS_Final_Azure.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DIS_Praneet.DataAccess
{
    public class DbInitializer
    {

        static HttpClient httpClient;
        static string BASE_URL = "https://developer.nrel.gov/docs/transportation/alt-fuel-stations-v1/";
        static string API_KEY = "QDZUN6JFSXUlauvAa9SapfxWhQ7t1KthqZnNgWqq";
        public static void Initialize(ApplicationDbContext context)
        {
            Console.WriteLine("Database Initialization");
            context.Database.EnsureCreated();
            //getStations(context);
            /*getTopics(context);
            getActivities(context);
            getParks(context);*/
        }

        /*public static void getStations(ApplicationDbContext context)
        {

            if (context.Fuel_Stations.Any())
            {
                return;
            }

            string uri = BASE_URL + "/parks?limit=100";
            string responsebody = "";
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.BaseAddress = new Uri(uri);

            try
            {
                HttpResponseMessage response = httpClient.GetAsync(uri).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    responsebody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }

                if (!responsebody.Equals(""))
                {
                    JObject parsedResponse = JObject.Parse(responsebody);
                    JArray stations = (JArray)parsedResponse["data"];

                    foreach (JObject jsonstation in stations)
                    {
                        Fuel_Stations p = new Fuel_Stations
                        {
                            ID = (string)jsonstation["id"],
                            Fuel_type = (string)jsonstation["Fuel_type"],
                            Station_name = (string)jsonstation["Station_name"],
                            Street_address = (string)jsonstation["Street_address"],
                            City = (string)jsonstation["City"],
                            State = (string)jsonstation["State"],
                            Zip = (string)jsonstation["Zip"],

                        };
                        context.Fuel_Stations.Add(p);
                        string[] Fuel_Stations = ((string)jsonstation["Fuel_Stations"]).Split(",");
                        foreach (string s in Fuel_Stations)
                        {
                            Fuel_Stations st = context.Fuel_Stations.Where(c => c.ID == s).FirstOrDefault();
                            if (st != null)
                            {
                                StatePark sp = new StatePark()
                                {
                                    state = st,
                                    park = p
                                };
                                context.StateParks.Add(sp);
                                context.SaveChanges();
                            }*/

    }
}

