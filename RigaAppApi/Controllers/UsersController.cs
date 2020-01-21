using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using RigaAppDataAccess;

namespace RigaAppApi.Controllers
{

    [RoutePrefix("api/user")]
    public class UsersController : ApiController
    {
        
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

        private string message;

        [HttpPost]
        [Route("Register")]
        public string PostNewUser(User newUser)
        {
            try
            {
                using (RigaAppUserEntities entities = new RigaAppUserEntities())
                {
                    newUser.password = HashPass(newUser.password);
                    if (entities.Users.FirstOrDefault(e => e.username == newUser.username) == null)
                    {
                        if (entities.Users.FirstOrDefault(e => e.email == newUser.email) == null)
                        {
                            entities.Users.Add(newUser);
                            entities.SaveChanges();
                            message = "User " + newUser.username + " created successfully!";
                        } else
                        {
                            message = "Email "+newUser.email+" already used!";
                        }
                    } else
                    {
                        message = "Account with username "+ newUser.username + " already exists.";
                    }

                    
                    return message;
                }
            }
            catch (Exception)
            {
                return "Something went wrong!";
            }
        }

        [HttpPost]
        [Route("Login")]
        public string PostLogin(User UserCred)
        {
            try
            {
                using (RigaAppUserEntities entities = new RigaAppUserEntities())
                {
                    UserCred.password = HashPass(UserCred.password);
                    if (((entities.Users.FirstOrDefault(e => e.username == UserCred.username) != null) || (entities.Users.FirstOrDefault(e => e.email == UserCred.username) != null)) && (entities.Users.FirstOrDefault(e => e.password == UserCred.password) != null))
                    {
                        message = "Logged in successfully!";
                    }
                    else
                    {
                        message = "Credentials are incorrect!";
                    }


                    return message;
                }
            }
            catch (Exception)
            {
                return "Something went wrong!";
            }
        }

        /*public IEnumerable<User> Get()
        {
            using (RigaAppUserEntities entities = new RigaAppUserEntities())
            {
                return entities.Users.ToList();
            }
        }*/



        public User Get(string username)
        {
            using (RigaAppUserEntities entities = new RigaAppUserEntities())
            {
                return entities.Users.FirstOrDefault(e => e.username == username);
            }
        }

        /*public void Post(user user)
        {
            using (RigaAppDBEntities entities = new RigaAppDBEntities())
            {
                entities.users.Add(user);
                entities.SaveChanges();
            }
        }*/
    }
}
