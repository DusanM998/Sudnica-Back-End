using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.JWTModels;

namespace Sudnica.Controllers
{
    public class KompanijaController : Controller
    {
        private IMongoDatabase db = Session.MongoDatabase;
        private readonly JwtSettings jwtSettings;
        public KompanijaController(JwtSettings jwtSettings)
        {
            this.jwtSettings = jwtSettings;
        }

        [HttpPost]
        [Route("addKompanija")]
        public async Task<Kompanija?> AddKompanija([FromBody] Kompanija kompanija)
        {
            try
            {
                var collection = db.GetCollection<Kompanija>("Kompanija");
                if (collection.Find(x => x.Id == kompanija.Id).FirstOrDefault() == null)
                {
                    collection.InsertOne(kompanija);
                    return kompanija;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw new Exception("Error!");
            }
        }

        [HttpGet]
        [Route("GetAllCompanies")]
        public async Task<List<Kompanija>> GetAllCompanies()
        {
            try
            {
                var collection = db.GetCollection<Kompanija>("Kompanija");
                var company = collection.Find(x => true).ToList();
                return company;
            }
            catch
            {
                throw new Exception("Error!");
            }
        }

        [HttpDelete]
        [Route("DeleteKompanija")]
        public async Task<IActionResult> DeleteKompanija([FromBody] Kompanija kompanija)
        {
            var collectionCompanies = db.GetCollection<Kompanija>("Kompanija");
            collectionCompanies.DeleteOne(x => x.Id == kompanija.Id);
            return Ok();
        }

    }
}
