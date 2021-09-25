using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
        public  IHttpActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var invoice_Detail = phdb.Invoice_Details.Where(d => d.Invoice_ID == id).Select(d => new { d.Invoice_ID, d.Medicine_ID, d.Quantity, d.Price }).GroupBy(s => s.Invoice_ID).ToList();

            if (invoice_Detail == null)
            {
                return NotFound();
            }
           
            return Ok(invoice_Detail);
        }

        // POST: api/InvoiceDetail
        public IHttpActionResult Post([FromBody]Invoice_Detail detail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("enter data");
            }
            if (IsExist(detail))
            {
                return Conflict();
            }
            else
            {
                phdb.Entry(detail).State = EntityState.Added;
                // phdb.Invoices.Add(invoice);

                try
                {
                    phdb.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));

                }
            }


            return Created("invoiceeeee detailsssss created", phdb.Invoice_Details);
        }

        private bool IsExist(object invoicedetails)
        {
            throw new NotImplementedException();
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
