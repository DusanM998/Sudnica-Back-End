using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudnica
{
    public class Kontakt
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonRequired]
        public string Ime { get; set; }
        public string Telefon1 { get; set; }
        public string Telefon2 { get; set; }
        public string Adresa { get; set; }
        public string Email { get; set; }
        public string Flag { get; set; }
        public string Zanimanje { get; set; }
        public Kompanija PripadnostKompaniji { get; set; }
    }
}
