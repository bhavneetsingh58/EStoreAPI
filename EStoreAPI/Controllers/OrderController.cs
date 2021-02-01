using EStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EStoreAPI.Controllers
{
    public class OrderOut
    {
        public int userID { get; set; }
        public string userName { get; set; }
        public int productID { get; set; }
        public string prodName { get; set; }
        public string addedBy { get; set; }
        public int orderID { get; set; }
    }



    [RoutePrefix("api/Order")]
    public class OrderController : ApiController
    {
        EStoreAPIEntities db = new EStoreAPIEntities();
        // GET api/<controller>
        //public IEnumerable<Order> Get()
        //{
        //    return db.Orders.ToList();
        //}
        public IEnumerable<OrderOut> Get()
        {
            return (from obj in db.Orders
                    join prod in db.Products on obj.productID equals prod.prodID
                    join user in db.Users on obj.userID equals user.userID
                    select new OrderOut
                    {
                      orderID = obj.orderID,
                      productID = (int)obj.productID,
                      prodName = prod.name,
                      userID = (int)obj.userID,
                      userName = user.name
                    }
                ).ToList();
        }

        // GET api/<controller>/5
        public Order Get(int id)
        {
            return db.Orders.Find(id);
        }

        // POST api/<controller>
        public HttpResponseMessage Post(Order order)
        {
            try
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                Order order = db.Orders.Find(id);
                db.Orders.Remove(order);
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