using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using BankClient.Models;
using Newtonsoft.Json;
using System.Text;

namespace BankClient.Controllers
{
    public class CreditController : Controller
    {
        public async Task<ActionResult> ShowAllCredits()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49425");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync(string.Format("api/credit"));
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    List<Credit> credits = JsonConvert.DeserializeObject<List<Credit>>(result);

                    return View("ShowAllCredits", credits);

                }

            }
            return View();
        }

        public async Task<ActionResult> GetCurrentCreditAndShow(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49425");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync(string.Format("api/credit?id=" + id));
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    Credit creditForShow = JsonConvert.DeserializeObject<Credit>(result);

                    return PartialView("ShowPartial", creditForShow);
                }

            }

            return View();
        }

        public ActionResult Add()
        {
            return PartialView("AddPartial");
        }

        public async Task<ActionResult> ConfirmAdd(decimal ammount, double percent, string description, DateTime dayOfCredit, int clientId)
        {
            Client currentClient = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49425");
                client.DefaultRequestHeaders.Clear();

                var response = await client.GetAsync(string.Format("api/client?id=" + clientId));

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    currentClient = JsonConvert.DeserializeObject<Client>(result);
                }
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49425");
                client.DefaultRequestHeaders.Clear();
                Credit credit = new Credit { Ammount= ammount, Percent=percent, Description=description, DayOfCredit=dayOfCredit,Client=currentClient };

                StringContent cont = new StringContent(JsonConvert.SerializeObject(credit), Encoding.UTF8, "application/json");

                var response2 = await client.PostAsync("api/credit", cont);

                if (response2.IsSuccessStatusCode)
                {
                    var result = response2.Content.ReadAsStringAsync().Result;
                    return RedirectToAction("GetNewCreditAndShow");
                }

            }

            return View();
        }

        public async Task<ActionResult> GetNewCreditAndShow()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49425");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync(string.Format("api/credit"));
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    List<Credit> credit = JsonConvert.DeserializeObject<List<Credit>>(result);
                    Credit creditForShow = credit.LastOrDefault();

                    return PartialView("ConfirmAddPartial", creditForShow);
                }

            }
            return View();
        }


        public async Task<ActionResult> Edit(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49425");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync(string.Format("api/credit?id=" + id));
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    Credit creditForEdit = JsonConvert.DeserializeObject<Credit>(result);

                    return PartialView("EditPartial", creditForEdit);
                }

            }

            return View();
        }

        public async Task<ActionResult> OkEdit(int id, decimal ammount, double percent, string description, DateTime dayOfCredit, int clientId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49425");
                client.DefaultRequestHeaders.Clear();
                //var response = await client.GetAsync(string.Format("api/client?id=" + clientId));
                //Client currentClient = null;
                //if (response.IsSuccessStatusCode)
                //{
                //    var result = response.Content.ReadAsStringAsync().Result;
                //    currentClient = JsonConvert.DeserializeObject<Client>(result);
                //}

                Credit credit = new Credit { Ammount = ammount, Percent = percent, Description = description, DayOfCredit = dayOfCredit, ClientId = clientId };

                StringContent cont = new StringContent(JsonConvert.SerializeObject(credit), Encoding.UTF8, "application/json");

                var response2 = await client.PutAsync("api/credit", cont);

                if (response2.IsSuccessStatusCode)
                {
                    var result = response2.Content.ReadAsStringAsync().Result;
                    return RedirectToAction("GetCurrentCreditAndShow", new { id = id });
                }

            }

            return View();
        }




        public async Task<ActionResult> CancelEdit(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49425");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync(string.Format("api/credit?id=" + id));
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    Credit creditForEdit = JsonConvert.DeserializeObject<Credit>(result);

                    return PartialView("ShowPartial", creditForEdit);
                }

            }

            return View();
        }


        public async Task<ActionResult> Delete(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49425");
                client.DefaultRequestHeaders.Clear();


                var response = await client.DeleteAsync("api/credit?id=" + id);

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;

                    return Content(id.ToString());
                }

            }

            return View();
        }
    }
}