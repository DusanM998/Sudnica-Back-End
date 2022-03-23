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
    public class KorisnikController : Controller
    {
        private IMongoDatabase db = Session.MongoDatabase;
        private readonly JwtSettings jwtSettings;
        public KorisnikController(JwtSettings jwtSettings)
        {
            this.jwtSettings = jwtSettings;
        }

        [HttpPost]
        [Route("addKorisnik")]
        public async Task<Korisnik?> AddKorisnik([FromBody] Korisnik korisnik)
        {
            try
            {
                var collection = db.GetCollection<Korisnik>("Korisnik");
                if (collection.Find(x => x.Ime == korisnik.Ime).FirstOrDefault() == null)
                {
                    collection.InsertOne(korisnik);
                    return korisnik;
                }
                else
                {
                    throw new Exception("Korisnik vec postoji");
                }
            }
            catch
            {
                throw new Exception("Error!");
            }

        }

        [HttpGet]
        [Route("GetAllUsers")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "user")]
        public async Task<List<Korisnik>> GetAllUsers()
        {
            try
            {
                var collection = db.GetCollection<Korisnik>("Korisnik");
                var user = collection.Find(x => true).ToList();
                return user;
            }
            catch
            {
                throw new Exception("Error!");
            }
        }

        [HttpPost]
        [Route("LoginUser")]
        public async Task<UserTokens?> LoginUser(LoginDto loginUser)
        {
            try
            {
                var Token = new UserTokens();
                var collections = db.GetCollection<Korisnik>("Korisnik");
                var users = collections.Find(x => true).ToList();
                foreach (var user in users)
                {
                    if (user.Username == loginUser.Username && user.Password == loginUser.Password)
                    {
                        Token = JwtHelpers.GenTokenkey(new UserTokens()
                        {
                            EmailId = "",
                            GuidId = Guid.NewGuid(),
                            UserName = user.Username,
                            Id = new Guid(),
                            Role = user.Role
                        }, jwtSettings);
                        return Token;
                    }
                }
                throw new Exception("Error!");
            }
            catch
            {
                throw new Exception("Error!");
            }
        }

        [HttpPut]
        [Route("UpdateUserPassword/{username}/{password}")]
        public async Task<IActionResult> UpdateUserPassword([FromRoute] string username, [FromRoute] string password)
        {
            var collection = db.GetCollection<Korisnik>("Korisnik");

            var filter = Builders<Korisnik>.Filter.Eq(x => x.Username, username);
            var update = Builders<Korisnik>.Update.Set(x => x.Password, password);
            collection.UpdateOne(filter, update);
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromBody] Korisnik korisnik)
        {
            var collectionUsers = db.GetCollection<Korisnik>("Korisnik");
            collectionUsers.DeleteOne(x => x.Id == korisnik.Id);
            return Ok();
        }
    }
}
