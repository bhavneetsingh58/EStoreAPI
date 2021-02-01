using EStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace EStoreAPI.Controllers
{
    public class ProdImageController : ApiController
    {
        EStoreAPIEntities db = new EStoreAPIEntities();
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public HttpResponseMessage Post()
        {
            if (HttpContext.Current.Request.Files.Count == 0)
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];
            byte[] bytes;
            //int ID = 2;
            using (BinaryReader br = new BinaryReader(postedFile.InputStream))
            {
                bytes = br.ReadBytes(postedFile.ContentLength);
            }
            ProdImage p = new ProdImage()
            {
                //productID = id,
                //product_name = postedFile.FileName,
                photo = bytes
            };
            db.ProdImages.Add(p);
            db.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
            // PUT api/<controller>/5


        // DELETE api/<controller>/5
 
    }
}