using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudnica
{
    public class TipPostupka
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Naslov { get; set; }
    }
}
