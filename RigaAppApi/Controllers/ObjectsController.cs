using RigaAppDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RigaAppApi.Controllers
{
    public class ObjectsController : ApiController
    {
        public IEnumerable<Places> Get()
        {
            using (RigaAppObjectsEntities entities = new RigaAppObjectsEntities())
            {
                return entities.Places.ToList();
            }
        }


        public Places Get(int id)
        {
            using (RigaAppObjectsEntities entities = new RigaAppObjectsEntities())
            {
                return entities.Places.FirstOrDefault(e => e.Id == id);
            }
        }
    }
}
