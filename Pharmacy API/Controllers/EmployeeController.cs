using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using Pharmacy_API.Models;
using System.Web.Http.Description;
using System.Data.Entity.Infrastructure;
using System.Net.Http;
using System.Net;
using System.Data.Entity;

namespace Pharmacy_API.Controllers
{
    public class EmployeeController : ApiController
    {
        PharmacyModel phdb = new PharmacyModel();
      
        // GET: api/Employee
        public IQueryable<Employee> GetEmployee()
        {
            return phdb.Employees;
        }

        // GET: api/Employee/id
        public IHttpActionResult GetEmployee(int id)
        {
            Employee emp = phdb.Employees.Find(id);
            if (emp==null)
            {
                return NotFound();
            }
            else
            {
                return Ok(emp);
            }
            
        }

        // POST  api/employee
       // [ResponseType(typeof(Employee))]
         public IHttpActionResult PostEmployee(Employee emp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            phdb.Employees.Add(emp);
            if (IsExist(emp))
            {
                return Conflict();
            }
            else if (UniquePhone(emp))
            {
                phdb.SaveChanges();
            }
            else
            {
                return ResponseMessage(new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Ambiguous));
            }

            return Created("employeeeeeessss", phdb.Employees);
           // return RedirectToRoute("Defaultapi" , new { id = emp.ID });

        }

        private bool UniquePhone(Employee emp)
        {
            Employee x = phdb.Employees.FirstOrDefault(e => e.Phone == emp.Phone);
            if (x!=null)
            {
                return false;
            }
            
            return true;
        }

        private bool IsExist(Employee emp)
        {
            return phdb.Employees.Count(e => e.ID == emp.ID) > 0;
        }



        //PUT api/Employee/id
        public IHttpActionResult PutEmployee(int id, Employee emp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (id != emp.ID)
            {
                return BadRequest();
            }

            phdb.Entry(emp).State =EntityState.Modified;

            try
            {
                phdb.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsExist(emp))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }//put Employee

        //DELETE api/Employee/id
        public IHttpActionResult DeleteEmployee(int id)
        {
            Employee e = phdb.Employees.Find(id);
            if (e == null)
            {
                return NotFound();
            }

            phdb.Employees.Remove(e);

            try
            {
                phdb.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            return Ok(e);

        }











    }
}