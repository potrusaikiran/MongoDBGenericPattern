using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern
{
   public interface IEntity<TKey>
    {  
        [BsonId]
        TKey Id { get; set; }

    }

    public interface IEntity : IEntity<string>
    {
    }
}
