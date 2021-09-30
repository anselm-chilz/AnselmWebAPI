using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace AnselmWebAPI.Models
{
    public class Country
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Capital { get; set; }
    }
}
