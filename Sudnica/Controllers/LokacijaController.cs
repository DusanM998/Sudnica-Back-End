using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.JWTModels;

namespace Sudnica.Controllers
{
    public class LokacijaController : Controller
    {
        private IMongoDatabase db = Session.MongoDatabase;
        private readonly JwtSettings jwtSettings;
        public LokacijaController(JwtSettings jwtSettings)
        {
            this.jwtSettings = jwtSettings;
        }

        [HttpPost]
        [Route("addLokacija")]
        public async Task<Lokacija?> AddLokacija([FromBody] Lokacija lokacija)
        {
            try
            {
                var collection = db.GetCollection<Lokacija>("Lokacija");
                if (collection.Find(x => x.Id == lokacija.Id).FirstOrDefault() == null)
                {
                    collection.InsertOne(lokacija);
                    return lokacija;
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
        [Route("GetAllLocations")]
        public async Task<List<Lokacija>> GetAllLocations()
        {
            try
            {
                var collection = db.GetCollection<Lokacija>("Lokacija");
                var location = collection.Find(x => true).ToList();
                return location;
            }
            catch
            {
                throw new Exception("Error!");
            }
        }

        [HttpDelete]
        [Route("DeleteLokacija")]
        public async Task<IActionResult> DeleteLokacija([FromBody] Lokacija lokacija)
        {
            var collectionLocations = db.GetCollection<Lokacija>("Lokacija");
            collectionLocations.DeleteOne(x => x.Id == lokacija.Id);
            return Ok();
        }
    }
}
