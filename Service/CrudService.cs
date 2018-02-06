using System;
using Entites;
using RepositoryPattern;
using MongoDB.Bson;

namespace Service
{
    public class CrudService: Repository<Crud>, ICrudService
    {
        private readonly Repository<Crud> _crudService;
        public CrudService(Repository<Crud> crudService)
        {
            this._crudService = crudService;
        }

        public void DeleteById(ObjectId id)
        {
            _crudService.Delete(id);
        }

        public Crud GetByCId(ObjectId id)
        {
            return _crudService.GetById(id);
        }
    }
}
