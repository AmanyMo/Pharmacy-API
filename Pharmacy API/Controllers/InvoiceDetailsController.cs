using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
        public async  Task<IHttpActionResult> Post([FromBody]Invoice_Detail detail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("enter data correctly !");
            }
            if (await IsExist(detail.Invoice_ID))
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

        private async Task<bool> IsExist(int Inv_id)
        {
            // return T= exist before can't add another invoice details :( , F doesn't exist & u can add invoice details :)
          Invoice_Detail invoice_ =  await phdb.Invoice_Details.FindAsync(Inv_id);
 
                return ( (invoice_ !=null) ?true : false ) ;       
        }

        // PUT: api/InvoiceDetails/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]Invoice_Detail invoice_Detail)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
          Invoice_Detail invoice_ =  await phdb.Invoice_Details.FindAsync(id);
            if(invoice_ == null)
            {
                return NotFound();
            }
            phdb.Entry(invoice_).State = EntityState.Modified;
            try
            {
                phdb.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            return Ok(invoice_Detail);

        }

        // DELETE: api/InvoiceDetails/5
        public IHttpActionResult Delete(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            Invoice i = phdb.Invoices.Find(id);
            if (i == null)
            {
                return NotFound();
            }
            else
            {
                //  phdb.Invoices.Remove(i);
                phdb.Entry(i).State = EntityState.Deleted;
                try
                {
                    phdb.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
                }

            }
            return Ok();
        }
    }
}
