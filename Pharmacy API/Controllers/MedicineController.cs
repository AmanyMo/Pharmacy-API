using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using Pharmacy_API.Models;
using System.Web.Http.Description;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Data.Entity;
namespace Pharmacy_API.Controllers
{
    public class MedicineController:ApiController
    {
        PharmacyModel phdb = new PharmacyModel();

        //GET  api/medicines
        public IQueryable<Medicine> GetMedicines()
        {
            return phdb.Medicines;
        }

        //GET  api/medicines/3
        public IHttpActionResult GetMedicines(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Medicine med = phdb.Medicines.Find(id);

            if (med ==null)
            {
                return NotFound();
            }
            return Ok(med);
        }

        //POST   api/medicines
        [ResponseType(typeof(Medicine))]

        public IHttpActionResult PostMedicine(Medicine med)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
    
            if (IsExist(med))
            {
                return Conflict();
            }
            else
            {
                //Medicine m = new Medicine();
                //m.Name = med.Name;
                //m.Description = med.Description;
                //m.Date_of_Production = med.Date_of_Production;
                //m.Expire_Date = med.Expire_Date;
                //m.price = med.price;
                phdb.Medicines.Add(med);
                try
                {
                phdb.SaveChanges();
                }
                catch (DbUpdateException)
                {
                  return  ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden)); 
                }
 
            }
            return RedirectToRoute("Defaultapi", new { id = med.ID });

        }

        private bool IsExist(Medicine med)
        {
            return phdb.Medicines.Count(m => m.ID == med.ID) > 0;
        }


        //PUT api/medicine/id
        public IHttpActionResult PutMedicine(int id, Medicine med)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (id != med.ID)
            {
                return BadRequest();
            }
            
                phdb.Entry(med).State = EntityState.Modified;

            try
            {
                phdb.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsExist(med))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }//put medicine

        //DELETE api/medicine/id
        public IHttpActionResult DeleteMedicine(int id)
        {
           
            Medicine m = phdb.Medicines.Find(id);
            if (m==null)
            {
                return NotFound();
            }
            
                phdb.Medicines.Remove(m);

                try
                {
                    phdb.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
                }
            return Ok(m);

        }









    }
}