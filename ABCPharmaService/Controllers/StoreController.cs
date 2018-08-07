using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ABCPharmaService.Models;
using ABCPharmaService.Repositories;

namespace ABCPharmaService.Controllers
{
    [RoutePrefix("api/Store")]
    public class StoreController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetAllStores()
        {
            try
            {
                var stores = StoreRepository.GetAllStores();
                return Ok(stores);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [HttpGet]
        public IHttpActionResult GetStoreById(int storeId)
        {
            try
            {
                var store = StoreRepository.GetStoreById(storeId);
                return Ok(store);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //[Route("SaveStoreDetails")]
        [HttpPostAttribute]
        [HttpOptions]
        public HttpResponseMessage SaveStoreDetails(Stores storeToAdd)
        {
            StoreRepository storeRepository = new StoreRepository();

            bool sucess = storeRepository.updateDeleteStore(storeToAdd);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "OK");
            return response;
        }
    }
}
