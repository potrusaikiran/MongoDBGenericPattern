using Entites;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public class CrudModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<CrudDetail> CrudDetail { get; set; }
    }
}
