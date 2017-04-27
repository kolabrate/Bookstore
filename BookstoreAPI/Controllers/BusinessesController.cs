﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BookstoreAPI.Models;
using GoogleApi;
using GoogleApi.Entities.Maps.Geocode.Request;

namespace BookstoreAPI.Controllers
{
    public class BusinessesController : ApiController
    {
        #region actions
        
        private Bookstorecontext db = new Bookstorecontext();

        // GET: api/Businesses
        public IQueryable<Business> GetBusinesses()
        {
            return db.Businesses;
        }

        // GET: api/Businesses/5
        [ResponseType(typeof(Business))]
        public IHttpActionResult GetBusiness(int id)
        {
            Business business = db.Businesses.Find(id);
            if (business == null)
            {
                return NotFound();
            }

            return Ok(business);
        }

        // PUT: api/Businesses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBusiness(int id, Business business)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != business.Id)
            {
                return BadRequest();
            }

            db.Entry(business).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusinessExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Businesses
        [ResponseType(typeof(Business))]
        public IHttpActionResult PostBusiness(Business business)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Businesses.Add(business);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = business.Id }, business);
        }

        // DELETE: api/Businesses/5
        [ResponseType(typeof(Business))]
        public IHttpActionResult DeleteBusiness(int id)
        {
            Business business = db.Businesses.Find(id);
            if (business == null)
            {
                return NotFound();
            }

            db.Businesses.Remove(business);
            db.SaveChanges();

            return Ok(business);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BusinessExists(int id)
        {
            return db.Businesses.Count(e => e.Id == id) > 0;
        }

        #endregion

        #region LocationService


        private Business GetBusinessDetails()
        {
            var request = new GeocodingRequest
            {
                Address = "Westerb Bace"
            };
            var result = GoogleMaps.Geocode.Query(request);

            throw new NotImplementedException();
            


        }



        #endregion
    }
}