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
    public class ProductOut 
    {
        public string name { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
        public int rating { get; set; }
        public string addedBy { get; set; }
        public int prodID { get; set; }
        public string photo { get; set; }

    }


    [RoutePrefix("api/Product")]
    public class ProductController : ApiController
    {
        // GET api/<controller>
        EStoreAPIEntities db = new EStoreAPIEntities();

        [HttpGet]

      

        public IEnumerable<ProductOut> Get()
        {
            return (from obj in db.Products
                    select new ProductOut
                    {
                        prodID = obj.prodID,
                        name = obj.name,
                        price = obj.price,
                        photo = obj.photo,
                        quantity = obj.quantity
                    }
                ).ToList();
        }

        // POST api/<controller>
        [HttpPost]
        public HttpResponseMessage Post(Product product)
        {
            try
            {
                db.Products.Add(product);
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
        public HttpResponseMessage Put(int id, Product product)
        {
            try
            {
                if (id == product.prodID)
                {
                    db.Entry(product).State = EntityState.Modified;
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
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                Product product = db.Products.Find(id);
                db.Products.Remove(product);
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