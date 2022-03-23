using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudnica
{
    public class Parnica
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public DateTime DateTime { get; set; }
        public ObjectId LokacijaId { get; set; }
        public virtual Lokacija Lokacija { get; set; }
        public Kontakt Sudija { get; set; }
        public ObjectId KontaktId { get; set; }
        public string TipUstanove { get; set; }
        public int BrojSudnice { get; set; }
        public virtual Kontakt Tuzilac { get; set; }
        public ObjectId TuzilacId { get; set; }
        public virtual Kontakt Tuzenik { get; set; }
        public ObjectId TuzenikId { get; set; }
        public virtual Kontakt ZaduzeniAdvokat { get; set; }
        public ObjectId AdvokatId { get; set; }
        public string Napomena { get; set; }
        public virtual TipPostupka TipPostupka { get; set; }
        public ObjectId TipPostupkaId { get; set; }
    }
}
