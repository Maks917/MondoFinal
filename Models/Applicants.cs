using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace MongoFinal.Models
{
    public class Applicants
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FIO { get; set; }
        public double ENT { get; set; }
        public string Tel_Num { get; set; }
        public string gmail { get; set; }
        public string City { get; set; }
        public Subj Subject { get; set; }
    }

    public class Subj
    {
        public double Math { get; set; }
        public double Geometry { get; set; }
        public double Phisics { get; set; }
        public double Geography { get; set; }
        public double Biology { get; set; }
        public double Chemistry { get; set; }
        public double History { get; set; }
        public double Kazakh_language { get; set; }
        public double Forein_language { get; set; }
    }

   /* public class Avarage
    {
        [BsonId]
        public string Id { get; set; }
        public double AvarageGrade { get; set; }
    }*/
}
