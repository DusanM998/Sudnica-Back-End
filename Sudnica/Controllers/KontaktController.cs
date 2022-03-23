using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.JWTModels;

namespace Sudnica.Controllers
{
    public class KontaktController : Controller
    {
        private IMongoDatabase db = Session.MongoDatabase;
        private readonly JwtSettings jwtSettings;
        public KontaktController(JwtSettings jwtSettings)
        {
            this.jwtSettings = jwtSettings;
        }

        [HttpPost]
        [Route("addKontakt")]
        public async Task<Kontakt?> AddKontakt([FromBody] Kontakt kontakt)
        {
            try
            {
                var collection = db.GetCollection<Kontakt>("Kontakt");
                if (collection.Find(x => x.Id == kontakt.Id).FirstOrDefault() == null)
                {
                    collection.InsertOne(kontakt);
                    return kontakt;
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
        [Route("GetAllContacts")]
        public async Task<List<Kontakt>> GetAllContacts()
        {
            try
            {
                var collection = db.GetCollection<Kontakt>("Kontakt");
                var contact = collection.Find(x => true).ToList();
                return contact;
            }
            catch
            {
                throw new Exception("Error!");
            }
        }
    }
}
