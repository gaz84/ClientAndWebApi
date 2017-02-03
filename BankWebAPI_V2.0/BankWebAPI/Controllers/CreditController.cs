using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using DAL;
using System.Net.Http;
using System.Net;

namespace BankWebAPI.Controllers
{
    public class CreditController : ApiController
    {
        private UnitOfWork uow = new UnitOfWork();

        // http://localhost:49425/api/credit

        // ** На метод Get **//

        public Credit Get(int id)
        {
            try
            {
                return uow.CreditRepository.GetById(id);
            }
            catch (Exception e)
            { return null; } // Якщо немає категорії
        }

        public IEnumerable<Credit> GetAll()
        {
            return uow.CreditRepository.GetAll();
        }

        // ** На метод POST (Створення) ** //
        


        public HttpResponseMessage Post([FromBody]Credit obj)
        {
            try
            {
                if (obj == null)
                    return Request.CreateResponse(HttpStatusCode.NotAcceptable); //406 погані дані

                uow.CreditRepository.Create(obj);
                uow.Commit();
                return Request.CreateResponse(HttpStatusCode.Created); //  Code: 201
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError); // Code: 500
            }
        }

        // ** На метод Put (Редагування) ** //
        
        public HttpResponseMessage Put([FromBody] Credit obj)
        {
            try
            {
                uow.CreditRepository.Update(obj);
                uow.Commit();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable); // Code: 406
            }
        }

        


        public HttpResponseMessage Delete(int id)
        {
            try
            {
                uow.CreditRepository.Delete(id);
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