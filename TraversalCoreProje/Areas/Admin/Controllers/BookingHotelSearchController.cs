using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TraversalCoreProje.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace TraversalCoreProje.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class BookingHotelSearchController : Controller
    {
        private static readonly Dictionary<string, string> ReviewScoreWordTranslations = new Dictionary<string, string>
        {
            { "Exceptional", "Olağanüstü" },
            { "Superb", "Muhteşem" },
            { "Fabulous", "Efsanevi" },
            { "Very good", "Çok iyi" },
            { "Good", "İyi" },
            { "Okay", "Orta" }
        };

        [HttpGet]
        public IActionResult GetCityDestID()
        {
            return View();
        }

        [HttpPost]
        public async Task< IActionResult> GetCityDestID(string p)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://booking-com.p.rapidapi.com/v1/hotels/locations?locale=en-gb&name={p}"),
                Headers =
    {
        { "x-rapidapi-key", "f59357f02dmshf8875a859b08ed3p176bc9jsn04fb190df32d" },
        { "x-rapidapi-host", "booking-com.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return View();
            }
        }
        public async Task<IActionResult> Index()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://booking-com.p.rapidapi.com/v2/hotels/search?dest_id=-1456928&order_by=popularity&checkout_date=2025-01-19&children_number=2&filter_by_currency=EUR&locale=en-gb&dest_type=city&checkin_date=2025-01-18&categories_filter_ids=class%3A%3A2%2Cclass%3A%3A4%2Cfree_cancellation%3A%3A1&children_ages=5%2C0&include_adjacency=true&page_number=0&adults_number=2&room_number=1&units=metric"),
                Headers =
    {
        { "x-rapidapi-key", "f59357f02dmshf8875a859b08ed3p176bc9jsn04fb190df32d" },
        { "x-rapidapi-host", "booking-com.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<BookingHotelSearchViewModel>(body);

                foreach (var result in values.results)
                {
                    if (ReviewScoreWordTranslations.TryGetValue(result.reviewScoreWord, out var translatedValue))
                    {
                        result.reviewScoreWord = translatedValue;
                    }
                }

                return View(values.results);
            }
        }
    }
}
