using EStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace EStoreAPI.Controllers
{   
    public class UserLogin
    {
        public string email { get; set; }
        public string pass { get; set; }
    }
    public class UserResult
    {
        public string name { get; set; }
        public string email { get; set; }
        public string userType { get; set; }
        public string addedBy { get; set; }
        public int userID { get; set; }
        public string Message { get; set; }


    }
    public class LoginController : ApiController
    {
        EStoreAPIEntities db1 = new EStoreAPIEntities();
        // GET api/<controller>

        [HttpGet]
        public string Get()
        {
            //var user = db1.Users.Where(i => i.email == email).FirstOrDefault();
            //if (user != null)
            //{
            //    return "exists";
            //}
            //else
            //{
            //    return "does not exist";
            //}
            return "test";

        }


        // POST api/<controller>
        
        //[HttpPost]
        //public String Post(UserLogin ul)
        //{
        //    try
        //    {

        //        var user = db1.Users.Where(i => i.email == ul.email && i.pass == ul.pass).FirstOrDefault();

        //        if (user != null)
        //        {
        //            return user.userType;
        //        }
        //        else
        //        {
        //            return "Wrong Info";
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return "Incorrect Info";
        //    }
        //}


        [HttpPost]
        public UserResult Post(UserLogin ul)
        {   
            try
            {

                var user = db1.Users.Where(i => i.email == ul.email && i.pass == ul.pass).FirstOrDefault();
                UserResult ur = new UserResult();

                if (user != null)
                {
                   
                    ur.name = user.name;
                    ur.email = user.email;
                    ur.userType = user.userType;
                    ur.userID = user.userID;
                    ur.addedBy = user.addedBy;
                    ur.Message = "Authenticated";
                    return ur;
                }
                else
                {
                    ur.name = "";
                    ur.email = "";
                    ur.userType = "";
                    ur.userID  = 0;
                    ur.addedBy = "";
                    ur.Message = "WrongEmailorPassword";
                    return ur;
                }

            }
            catch (Exception ex)
            {
                UserResult ur = new UserResult();
                ur.name = "";
                ur.email = "";
                ur.userType = "";
                ur.userID = 0;
                ur.addedBy = "";
                ur.Message = "ConnectionError";
                return ur;
            }
        }

    }
}