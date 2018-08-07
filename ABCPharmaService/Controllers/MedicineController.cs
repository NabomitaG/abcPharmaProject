using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ABCPharmaService.Models;
using ABCPharmaService.Repositories;

namespace ABCPharmaService.Controllers
{
    [RoutePrefix("api/Medicine")]
    public class MedicineController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetAllMedicine()
        {
            try
            {
                var medicines = MedicineRepository.GetAllMedicines();
                return Ok(medicines);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [HttpGet]
        public IHttpActionResult GetMedicineByName(string medicineName_)
        {
            try
            {
                var medicines = MedicineRepository.GetMedicineByName(medicineName_);
                return Ok(medicines);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [HttpPostAttribute]
        [HttpOptions]
        public HttpResponseMessage SaveMedicines(Medicines medicineToAdd)
        {
            MedicineRepository medicineRepository = new MedicineRepository();

            bool sucess = medicineRepository.updateDeleteMedicine(medicineToAdd);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "OK");
            return response;
        }
    }
}