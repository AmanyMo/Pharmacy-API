using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Pharmacy_API.Models;

namespace Pharmacy_API.Controllers
{
    public class InvoiceDetailsController : ApiController
    {
        PharmacyModel phdb = new PharmacyModel();
        // GET: api/InvoiceDetails
        public IEnumerable<Invoice_Detail> Get()
        {
            return phdb.Invoice_Details;
        }

        // GET: api/InvoiceDetails/5
        public IHttpActionResult Get(int id)
        {
            return Ok();
        }

        // POST: api/InvoiceDetails
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/InvoiceDetails/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/InvoiceDetails/5
        public void Delete(int id)
        {
        }
    }
}
