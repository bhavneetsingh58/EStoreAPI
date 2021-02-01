using EStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
namespace EStoreAPI.Controllers
{   
    public class userID
    {
        public int UserID { get; set; }
    }


    [RoutePrefix("api/Users")]
    public class UsersController : ApiController
    {
        EStoreAPIEntities db = new EStoreAPIEntities();
        // GET api/<controller>

        [HttpGet]
      
        public IEnumerable<User> Get()
        {
            return db.Users.ToList();
        }

        // GET api/<controller>/5
        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        // POST api/<controller>
        [HttpPost]
        public HttpResponseMessage Post(User user)
        {
            try
            {
                db.Users.Add(user);
                db.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        
        // PUT api/<controller>/5
        [HttpPut]
        public HttpResponseMessage Put(int id,User user)
        {
            try
            {
                if (id == user.userID)
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                User user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }
    }
}