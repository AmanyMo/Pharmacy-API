using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


using Pharmacy_API.Models;

namespace Pharmacy_API.Controllers
{
    public class InvoiceController : ApiController
    {
        PharmacyModel phdb = new PharmacyModel();

        //GET api/invoice

        public IQueryable<Invoice> GetInvoice()
        {
            return phdb.Invoices;
        }

        //GET api/invoice/id
        public IHttpActionResult GetInvoice(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Invoice I = phdb.Invoices.Find(id);
            if (I == null)
            {
                return NotFound();
            }
            return Ok(I);
        }


        //POST api/invoice
        public IHttpActionResult PostInvoice(Invoice invoice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("enter data");
            }
            if (IsExist(invoice))
            {
                return Conflict();
            }
            else
            {
                phdb.Entry(invoice).State = EntityState.Added;
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


            return Created("invoiceeeee", phdb.Invoices);

        }

        private bool IsExist(Invoice invoice)
        {
            Invoice i = phdb.Invoices.Find(invoice.ID);
            if (i == null)
                return false;
            else
                return true;
        }


        //DELETE api/deleteinvoice/id
        public IHttpActionResult DeleteInvoice(int id)
        {
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
            return Ok(i);
        }

        //PUT api/putinvoice/id
        public IHttpActionResult PutInvoice(int id, Invoice invoice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("obj  * smth goes wrong !!!");
            }
            if (id != invoice.ID)
            {
                return BadRequest("id url != id in obj model");
            }

            Invoice i = phdb.Invoices.Find(id);
            if (i == null)
            {
                return BadRequest("id ** doesnot exist");
            }

            
            // phdb.Entry(invoice).State = EntityState.Modified;
            i.Date = invoice.Date;
            i.Emp_ID = invoice.Emp_ID;
            i.Total = invoice.Total;
                try
                {
                    phdb.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
                }
                return Ok(invoice);

        }



    }
}