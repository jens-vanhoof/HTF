using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using htf.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

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

        public class ResponseGet
        {
            public string id { get; set; }
            public string identifier { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public Question question { get; set; }
            public Example example { get; set; }
        }
        public class Anwser
        {
            public string name { get; set; }
            public string data { get; set; }
        }

        public class Body
        {
            public string challengeId { get; set; }
            public List<Anwser> values { get; set; }
        }

        private readonly IHttpClientFactory _clientFactory;
        public ResponseGet ChallengeTwo { get; private set; }

        public ChallengeController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task PostTwo(Two anwser,string veld)
        {
            var value = new Anwser
            {
                data = anwser.answer,
                name = veld
            };
            var data = new Body
            {
                challengeId = ChallengeTwo.id
            };

            data.values = new List<Anwser>();
            data.values.Add(value);

            var client = new RestClient("http://htf2018.azurewebsites.net");
            var request = new RestRequest("challenges/{id}", Method.POST);
            request.AddUrlSegment("id", ChallengeTwo.id);
            request.AddJsonBody(data);
            request.AddHeader("htf-identification", "ODc2ZjM2NjYtZGUyNy00ZDczLThkN2QtOTY4ZTA2NzY3MGMy");

            IRestResponse response = client.Execute(request);
            var content = response.Content;

        }

        public async Task GetTwo()
        {
            var client = new RestClient("http://htf2018.azurewebsites.net");
            var request = new RestRequest("challenges/{id}", Method.GET);
            request.AddUrlSegment("id", "593bc0a2e0dfdc53b239bc2a96ab0fd5");
            request.AddHeader("htf-identification", "ODc2ZjM2NjYtZGUyNy00ZDczLThkN2QtOTY4ZTA2NzY3MGMy");

            IRestResponse<ResponseGet> response = client.Execute<ResponseGet>(request);
            ChallengeTwo = response.Data;
        }

        public void SolveChallengeTwo(Two obj)
        {
            obj.values = new List<String>();
            var som = 0;

            foreach (var i in ChallengeTwo.question.inputValues)
            {
                obj.values.Append(i.data);
                som += int.Parse(i.data);
            }

            obj.question = ChallengeTwo.description;
            obj.answer = som.ToString();
        }

        public async Task<IActionResult> Two()
        {
            await GetTwo();
            var obj = new Two();
            SolveChallengeTwo(obj);
            System.Threading.Thread.Sleep(11000);
            await PostTwo(obj,"sum");
            return View(obj);
        }
    }
}