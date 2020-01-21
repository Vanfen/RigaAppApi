using RigaAppDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace RigaAppApi.Controllers
{
    //[RoutePrefix("api/user")]
    public class RegistrationController : ApiController
    {

        public string Sha256Password(string str)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(str));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private string HashPass(string pass)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] hashValue;
            UTF8Encoding objUtf8 = new UTF8Encoding();
            hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(pass));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashValue.Length; i++)
            {
                builder.Append(hashValue[i].ToString("x2"));
            }
            return builder.ToString(); //ASCIIEncoding.ASCII.GetString(hashValue, 0, hashValue.Length);
        }
        //Encoding.UTF8.GetString

        [HttpPost]
        [Route("Register")]
        public HttpResponseMessage PostNewUser(User newUser)
        {
            try
            {
                using (RigaAppUserEntities entities = new RigaAppUserEntities())
                {
                    newUser.password = HashPass(newUser.password);
                    entities.Users.Add(newUser);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, newUser);
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}
