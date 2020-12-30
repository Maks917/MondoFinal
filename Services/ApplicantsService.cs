using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoFinal.Services
{
    public class ApplicantsService
    {
        private readonly IMongoCollection<Applicants> _applicants;

        public ApplicantsService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _applicants = database.GetCollection<Applicants>("Applicants");
        }

        public Applicants Create(Applicants applicants)
        {
            _applicants.InsertOne(applicants);
            return applicants;
        }


        public IList<BsonDocument> Statistics()
        {
            var group = _applicants.Aggregate()
                .Group(new BsonDocument { { "_id", "$City" }, { "avgEnt", new BsonDocument("$avg", "$ENT") } }).ToList();
            return group;
        }

        public IList<Applicants> Ent_Filter(int? ent)
        {

            var filter = new BsonDocument("ENT", new BsonDocument("$gte", ent));
            var result = _applicants.Find(filter).ToList();
            return result;
        }

        public IList<Applicants> Read()
        {
            return _applicants.Find(sub => true).ToList();
        }

        public Applicants Find(string id)
        {
            return _applicants.Find(sub => sub.Id == id).SingleOrDefault();
        }

        public void Update(Applicants applicants) =>
            _applicants.ReplaceOne(sub => sub.Id == applicants.Id, applicants);

        public void Delete(string id) =>
            _applicants.DeleteOne(sub => sub.Id == id);
    }
}
