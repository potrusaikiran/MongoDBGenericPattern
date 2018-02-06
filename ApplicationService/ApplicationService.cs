using Entites;
using Models;
using MongoDB.Bson;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService
{
    public class ApplicationService
    {
        private readonly ICrudService _curdService;
        public ApplicationService(ICrudService curdService)
        {
            this._curdService = curdService;
        }

        public Crud GetById(string id)
        {
            var ida = ObjectId.Parse(id);
            var crud = _curdService.GetByCId(ObjectId.Parse(id));
            if (crud == null)
                return new Crud();
            return crud;
        }

        public CrudModel Save(CrudModel crudModel)
        {
            try
            {
                var sample = crudModel.Address.ToBsonDocument();
                Crud crud = new Crud()
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    FirstName = crudModel.FirstName,
                    MiddleName = crudModel.MiddleName,
                    LastName = crudModel.LastName,
                    Address = crudModel.Address
                };
                //var root = new BsonDocument();
                //root.Add("name", "NamingConventions");
                //var rootCategories = new BsonArray();
                //rootCategories.Add(new BsonDocument
                //                                                {
                //                                                    { "standards", new BsonArray() }
                //                                                 });

                //root.Add("categories", rootCategories);
                _curdService.Insert(crud);
            }
            catch (Exception e)
            {
                throw e;
            }
            return crudModel;
        }

        public string Delete(string id)
        {
            _curdService.DeleteById(ObjectId.Parse(id));
            return "deleted...!!!";
        }
    }
}
