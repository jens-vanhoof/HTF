using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace htf.Controllers
{
    public class ChallengeController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public ChallengeController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<String> GetOne()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "htf2018.azurewebsites.net/challenges/593bc0a2e0dfdc53b239bc2a96ab0fd5");
            request.Headers.Add("htf-identification", "ODc2ZjM2NjYtZGUyNy00ZDczLThkN2QtOTY4ZTA2NzY3MGMy");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {

                var test = await response.Content.ReadAsAsync<String>();
                test = "succes";
                return test;
            }
            else
            {
                return "error";
            }
        }

        public IActionResult One()
        {
            var test = GetOne();
            ViewData["test"] = test;
            return View(ViewData);
        }
    }
}