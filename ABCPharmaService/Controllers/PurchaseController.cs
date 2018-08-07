using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ABCPharmaService.Models;
using ABCPharmaService.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace ABCPharmaService.Controllers
{
    [RoutePrefix("api/Purchase")]
    public class PurchaseController : ApiController
    {
        [HttpPostAttribute]
        [HttpOptions]
        [AcceptVerbs("GET", "POST")]
        public HttpResponseMessage GenerateInvoice(object data_)
        {
            try
            {
                PurchaseRepository purchaseRepository = new PurchaseRepository();
                Purchases purchase = new Purchases();
                string invoice_ = string.Empty;
              
                if (data_ != null)
                {
                    string content = data_.ToString();
                    JObject json = JObject.Parse(content);
                    purchase.storeId = Convert.ToInt32(JsonConvert.DeserializeObject<int>(json.GetValue("storeId").ToString()));
                    purchase.purchaseDetails = content;
                    purchase.purchaseDate = DateTime.Today;
                    //invoice = purchaseRepository.SavePurchaseDetails(purchase, json);
                    invoice_ = JsonConvert.SerializeObject(purchaseRepository.SavePurchaseDetails(purchase, json));
                }
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, invoice_);
                return response;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
