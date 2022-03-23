using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudnica
{
    public class Kompanija
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }
    }
}
