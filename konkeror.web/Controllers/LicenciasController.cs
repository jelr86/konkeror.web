using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using konkeror.data;
using konkeror.data.Domain;

namespace konkeror.web.Controllers
{
    public class LicenciasController : ApiController
    {
        public konkerorEntities KonkerorDb { get; set; }
        public LicenciasController()
        {
            KonkerorDb = new konkerorEntities();
        }

        // GET api/values
        public IEnumerable<Licencia> Get()
        {

            return KonkerorDb.Licencias.ToList();
        }

        // GET api/values/5
        public Licencia Get(string id)
        {
            return KonkerorDb.Licencias.Where(l => l.Id.Equals(id)).FirstOrDefault();
        }

        // POST api/values
        public void Post([FromBody] Licencia licencia)
        {
            try
            {
                KonkerorDb.Licencias.Add(licencia);
                KonkerorDb.SaveChanges();
            }
            catch (Exception)
            {

            }
        }

        // PUT api/values/5
        public void Put(string id, [FromBody] Licencia value)
        {
            try
            {
                var lic = KonkerorDb.Licencias.Where(l => l.Id.Equals(id)).FirstOrDefault();
                lic.Name = value.Name;
                KonkerorDb.SaveChanges();
            }
            catch (Exception)
            {

            }
        }

        // DELETE api/values/5
        public void Delete(string id)
        {
            try
            {
                var lic = KonkerorDb.Licencias.Where(l => l.Id.Equals(id)).FirstOrDefault();
                KonkerorDb.Licencias.Remove(lic);
                KonkerorDb.SaveChanges();
            }
            catch (Exception)
            {

            }
        }
    }
}
