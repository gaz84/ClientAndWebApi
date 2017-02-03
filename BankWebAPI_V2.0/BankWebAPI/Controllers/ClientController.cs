using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using DAL;
using System.Net.Http;
using System.Net;
using Repository;
namespace BankWebAPI.Controllers
{
    public class ClientController : ApiController
    {
        private UnitOfWork uow = new UnitOfWork();

        // http://localhost:49425/api/client

        // ** На метод Get **//

        public Client Get(int id)
        {
            try
            {
                return uow.ClientRepository.GetById(id);
            }
            catch (Exception e)
            { return null; } 
        }

        public IEnumerable<Client> GetAll()
        {
            return uow.ClientRepository.GetAll();
        }

        // ** На метод POST (Створення) ** //



        public HttpResponseMessage Post([FromBody]Client obj)
        {
            try
            {
                if (obj == null)
                    return Request.CreateResponse(HttpStatusCode.NotAcceptable); //406 погані дані

                uow.ClientRepository.Create(obj);
                uow.Commit();
                return Request.CreateResponse(HttpStatusCode.Created); //  Code: 201
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError); // Code: 500
            }
        }

        // ** На метод Put (Редагування) ** //

        public HttpResponseMessage Put([FromBody]Client obj)
        {
            try
            {
                uow.ClientRepository.Update(obj);
                uow.Commit();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable); // Code: 406
            }
        }

        // ** На метод Delete (Видалення) ** //



        public HttpResponseMessage Delete(int id)
        {
            try
            {
                uow.ClientRepository.Delete(id);
                uow.Commit();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.NotFound); // Code: 404
            }
        }
    }
}