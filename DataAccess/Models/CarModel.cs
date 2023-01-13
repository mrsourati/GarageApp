using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using ZstdSharp.Unsafe;

namespace GarageDataAccess.Models
{
    public class CarModel   //En klass som ger objektet värde (Brand och Colour)
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]   //Skapar ett unik ID som ses i MongoDB
        public string Id { get; set; }
        public string? Brand { get; set; }
        public string? Colour { get; set; }


    }
}
