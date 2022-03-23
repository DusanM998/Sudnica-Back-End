using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.JWTModels;

namespace Sudnica.Controllers
{
    public class TipPostupkaController : Controller
    {
        private IMongoDatabase db = Session.MongoDatabase;
        private readonly JwtSettings jwtSettings;
        public TipPostupkaController(JwtSettings jwtSettings)
        {
            this.jwtSettings = jwtSettings;
        }

        [HttpPost]
        [Route("AddTipPostupka")]
        public async Task<TipPostupka?> AddTipPostupka([FromBody] TipPostupka tipPostupka)
        {
            try
            {
                var collection = db.GetCollection<TipPostupka>("TipPostupka");
                if (collection.Find(x => x.Id == tipPostupka.Id).FirstOrDefault() == null)
                {
                    collection.InsertOne(tipPostupka);
                    return tipPostupka;
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
        [Route("GetAllTipoviPostupaka")]
        public async Task<List<TipPostupka>> GetAllTipoviPostupaka()
        {
            try
            {
                var collection = db.GetCollection<TipPostupka>("TipPostupka");
                var tipPostupka = collection.Find(x => true).ToList();
                return tipPostupka;
            }
            catch
            {
                throw new Exception("Error!");
            }
        }

        [HttpDelete]
        [Route("DeleteTipPostupka")]
        public async Task<IActionResult> DeleteTipPostupka([FromBody] TipPostupka tipPostupka)
        {
            var collection = db.GetCollection<TipPostupka>("TipPostupka");
            collection.DeleteOne(x => x.Id == tipPostupka.Id);
            return Ok();
        }
    }
}
