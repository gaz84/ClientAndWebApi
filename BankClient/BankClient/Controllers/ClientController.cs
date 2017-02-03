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
    public class ClientController : Controller
    {
        public async Task<ActionResult> ShowAllClients()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49425");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync(string.Format("api/client"));
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    List<Client> clients = JsonConvert.DeserializeObject<List<Client>>(result);

                    return View("ShowAllClients", clients);
                   
                }

            }
            return View();
        }

        public async Task<ActionResult> GetCurrentClientAndShow(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49425");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync(string.Format("api/client?id=" + id));
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    Client clientForShow = JsonConvert.DeserializeObject<Client>(result);

                    return PartialView("ShowPartial", clientForShow);
                }

            }

            return View();
        }

        public ActionResult Add()
        {
            return PartialView("AddPartial");
        }

        public async Task<ActionResult> ConfirmAdd(string name,string surname)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49425");
                client.DefaultRequestHeaders.Clear();
                
               Client cat = new Client { Name = name,Surname=surname };

                StringContent cont = new StringContent(JsonConvert.SerializeObject(cat), Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/client", cont);

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return RedirectToAction("GetNewClientAndShow");
                }

            }

            return View();
        }

        public async Task<ActionResult> GetNewClientAndShow()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49425");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync(string.Format("api/client"));
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    List<Client> cat = JsonConvert.DeserializeObject<List<Client>>(result);
                    Client clientForShow = cat.LastOrDefault();

                    return PartialView("ConfirmAddPartial", clientForShow);
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
                var response = await client.GetAsync(string.Format("api/client?id=" + id));
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    Client clientForEdit = JsonConvert.DeserializeObject<Client>(result);

                    return PartialView("EditPartial", clientForEdit);
                }

            }

            return View();
        }

        public async Task<ActionResult> OkEdit(Client cat)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49425");
                client.DefaultRequestHeaders.Clear();
                

                StringContent cont = new StringContent(JsonConvert.SerializeObject(cat), Encoding.UTF8, "application/json");

                var response = await client.PutAsync("api/client", cont);

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return RedirectToAction("GetCurrentClientAndShow", new { id = cat.id });
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
                var response = await client.GetAsync(string.Format("api/client?id=" + id));
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    Client clientForEdit = JsonConvert.DeserializeObject<Client>(result);

                    return PartialView("ShowPartial", clientForEdit);
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
               

                var response = await client.DeleteAsync("api/client?id=" + id);

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