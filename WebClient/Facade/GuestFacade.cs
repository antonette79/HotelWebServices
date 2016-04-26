using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Newtonsoft.Json;
using WebClient.Model;

namespace WebClient.Facade
{
    class GuestFacade
    {
        public static Guest Guest { get; set; }
        public static IEnumerable<Guest> Guests { get; set; }
        public const string ServerUrl = "http://localhost:7908/";

        public static async Task<IEnumerable<Guest>> GetGuestsAsync()
        {
            Guests = null;
            HttpClientHandler handler = new HttpClientHandler
            {
                UseDefaultCredentials = true
            };
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var response = await client.GetAsync("api/guests");
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        Guests = JsonConvert.DeserializeObject<IEnumerable<Guest>>(result);
                    }
                }
                catch (Exception ex)
                {
                    MessageDialog msgDialog = new MessageDialog(ex.Message, "Error");
                    await msgDialog.ShowAsync();
                    throw;
                }
                return Guests;
            }
        }
        public static async Task<Guest> GetGuest(int id)
        {
            Guest = null;
            HttpClientHandler handler = new HttpClientHandler
            { UseDefaultCredentials = true };
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var response = await client.GetAsync("api/guests");
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        var listofGuests = JsonConvert.DeserializeObject<List<Guest>>(result);
                        foreach (var item in listofGuests)
                        {
                            Guest = item.Guest_No == id ? item : null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageDialog msgDialog = new MessageDialog(ex.Message, "Error");
                    await msgDialog.ShowAsync();
                    throw;
                }
                return Guest;
            }
        }
        public static async Task<string> PostGuest(Guest guest)
        {
            Guest = guest;
            HttpClientHandler handler = new HttpClientHandler
            { UseDefaultCredentials = true };
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var response = await client.PostAsJsonAsync("api/guests", Guest);
                    if (response.IsSuccessStatusCode)
                    {
                        return "Guest Created";
                    }
                    return "Error Creating Guest" + response.StatusCode;
                }
                catch (Exception ex)
                {
                    MessageDialog msgDialog = new MessageDialog(ex.Message, "Error");
                    await msgDialog.ShowAsync();
                    throw;
                }
            }
        }
        public static async Task<string> DeleteGuest(Guest guest)
        {
            Guest = guest;
            HttpClientHandler handler = new HttpClientHandler {UseDefaultCredentials = true};
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var response = await client.DeleteAsync("api/guests/" + Guest.Guest_No);
                    if (response.IsSuccessStatusCode)
                    {
                        return "Guest Deleted";
                    }
                    return "Error Deleting Guest: " + response.StatusCode;
                }
                catch (Exception ex)
                {
                    MessageDialog msgDialog = new MessageDialog(ex.Message, "Error");
                    await msgDialog.ShowAsync();
                    throw;
                }
            }
        }
      public static async Task<string> PutGuestAsync(Guest guest, int id)
        {
            Guest = guest;
            HttpClientHandler handler = new HttpClientHandler { UseDefaultCredentials = true };
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var response = await client.PutAsJsonAsync("api/guests/" + id, Guest);
                    if (response.IsSuccessStatusCode)
                    {
                        return "Guest Updated";
                    }
                    return "Error Updating Guest: " + response.StatusCode;
                }
                catch (Exception ex)
                {
                    MessageDialog msgDialog = new MessageDialog(ex.Message, "Runtime Error");
                    await msgDialog.ShowAsync();
                    throw;
                }
            }
        }
    }
}