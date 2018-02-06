using Entites;
using MongoDB.Bson;
using RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ICrudService : IRepository<Crud>
    {
        Crud GetByCId(ObjectId id);
        void DeleteById(ObjectId id);
    }
}
