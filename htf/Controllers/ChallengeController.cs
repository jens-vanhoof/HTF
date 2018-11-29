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
        public class InputValue
        {
            public string name { get; set; }
            public string data { get; set; }
        }

        public class Question
        {
            public List<InputValue> inputValues { get; set; }
        }

        public class InputValue2
        {
            public string name { get; set; }
            public string data { get; set; }
        }

        public class Question2
        {
            public List<InputValue2> inputValues { get; set; }
        }

        public class Value
        {
            public string name { get; set; }
            public string data { get; set; }
        }

        public class Answer
        {
            public string challengeId { get; set; }
            public List<Value> values { get; set; }
        }

        public class Example
        {
            public Question2 question { get; set; }
            public Answer answer { get; set; }
        }

        public class RootObject
        {
            public string id { get; set; }
            public string identifier { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public Question question { get; set; }
            public Example example { get; set; }
        }

        private readonly IHttpClientFactory _clientFactory;

        public ChallengeController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<RootObject> GetOne()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "http://htf2018.azurewebsites.net/challenges/593bc0a2e0dfdc53b239bc2a96ab0fd5");
            request.Headers.Add("htf-identification", "ODc2ZjM2NjYtZGUyNy00ZDczLThkN2QtOTY4ZTA2NzY3MGMy");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {

                var test = await response.Content.ReadAsAsync<RootObject>();
                return test;
            }
            else
            {
                return null;
            }
        }

        public IActionResult One()
        {
            var test = GetOne();
            ViewData["test"] = test;
            return View(test);
        }
    }
}