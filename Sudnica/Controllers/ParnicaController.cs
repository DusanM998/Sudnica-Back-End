using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Sudnica.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.JwtHelpers;
using WebApplication.JWTModels;

namespace Sudnica.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParnicaController : Controller
    {
        private IMongoDatabase db = Session.MongoDatabase;
        private readonly JwtSettings jwtSettings;
        public ParnicaController(JwtSettings jwtSettings)
        {
            this.jwtSettings = jwtSettings;
        }

        [HttpPost]
        [Route("addParnica")]
        public async Task<Parnica?> AddParnica([FromBody] Parnica parnica)
        {
            try
            {
                var collection = db.GetCollection<Parnica>("Parnica");
                if (collection.Find(x => x.Id == parnica.Id).FirstOrDefault() == null)
                {
                    collection.InsertOne(parnica);
                    return parnica;
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
        [Route("GetAllParnice")]
        public async Task<List<Parnica>> GetAllParnice()
        {
            try
            {
                var collection = db.GetCollection<Parnica>("Parnica");
                var parnica = collection.Find(x => true).ToList();
                return parnica;
            }
            catch
            {
                throw new Exception("Error!");
            }
        }

        [HttpGet]
        [Route("GetParnice")]
        public async Task<List<Parnica>> GetParnice()
        {
            try
            {
                var parniceCollection = db.GetCollection<Parnica>("Parnica");
                var parnice = parniceCollection.Find(x => true).ToList();
                List<Parnica> Lista = new List<Parnica>();
                foreach(var parnica in parnice)
                {
                    var lokacijeCollection = db.GetCollection<Lokacija>("Lokacija");
                    var lokacija = lokacijeCollection.Find(x => x.Id == parnica.LokacijaId).FirstOrDefault();
                    var Parnica = new Parnica();
                    Parnica.Id = parnica.Id;
                    Parnica.Lokacija = new Lokacija();
                    Parnica.Lokacija.Id = lokacija.Id;
                    Parnica.Lokacija.Naslov = lokacija.Naslov;

                    var sudijeCollection = db.GetCollection<Kontakt>("Kontakt");
                    var sudija = sudijeCollection.Find(x => x.Id == parnica.KontaktId).FirstOrDefault();
                    Parnica.Id = parnica.Id;
                    Parnica.Sudija = new Kontakt();
                    Parnica.Sudija.Id = sudija.Id;
                    Parnica.Sudija.Ime = sudija.Ime;

                    var tuzilacCollection = db.GetCollection<Kontakt>("Kontakt");
                    var tuzilac = tuzilacCollection.Find(x => x.Id == parnica.TuzilacId).FirstOrDefault();
                    Parnica.Id = parnica.Id;
                    Parnica.Tuzilac = new Kontakt();
                    Parnica.Tuzilac.Id = tuzilac.Id;
                    Parnica.Tuzilac.Ime = tuzilac.Ime;

                    var tuzenikCollection = db.GetCollection<Kontakt>("Kontakt");
                    var tuzenik = tuzenikCollection.Find(x => x.Id == parnica.TuzenikId).FirstOrDefault();
                    Parnica.Id = parnica.Id;
                    Parnica.Tuzenik = new Kontakt();
                    Parnica.Tuzenik.Id = tuzenik.Id;
                    Parnica.Tuzenik.Ime = tuzenik.Ime;

                    var advokatCollection = db.GetCollection<Kontakt>("Kontakt");
                    var advokat = advokatCollection.Find(x => x.Id == parnica.AdvokatId).FirstOrDefault();
                    Parnica.Id = parnica.Id;
                    Parnica.ZaduzeniAdvokat = new Kontakt();
                    Parnica.ZaduzeniAdvokat.Id = advokat.Id;
                    Parnica.ZaduzeniAdvokat.Ime = advokat.Ime;

                    var tipPostupkaCollection = db.GetCollection<TipPostupka>("TipPostupka");
                    var tipPostupka = tipPostupkaCollection.Find(x => x.Id == parnica.TipPostupkaId).FirstOrDefault();
                    Parnica.Id = parnica.Id;
                    Parnica.TipPostupka = new TipPostupka();
                    Parnica.TipPostupka.Id = tipPostupka.Id;
                    Parnica.TipPostupka.Naslov = tipPostupka.Naslov;
                }
                return parnice;
            }
            catch
            {
                throw new Exception("Error!");
            }
        }
    };
}
