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
                Crud crud = new Crud()
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Name = crudModel.Name,
                    CrudDetail = new List<CrudDetail>()
                    {
                      new CrudDetail{  Id = ObjectId.GenerateNewId().ToString(),
                                       CrudId = ObjectId.GenerateNewId().ToString(),
                                       DetailName = "Detail1",
                                            crudDetailDetail=new List<CrudDetailDetail>()
                                                {
                                                    new CrudDetailDetail{
                                                                          Id = ObjectId.GenerateNewId().ToString(),
                                                                          CrudDetailId = ObjectId.GenerateNewId().ToString(),
                                                                          Name = "DetailDetail1",
                                                                              subDetail=new List<SubDetail>()
                                                                                                            {
                                                                                  new SubDetail{
                                                                                      Name="subdetail1"
                                                                                               }
                                                                                                          }
                                                                     },
                                                    new CrudDetailDetail{
                                                                          Id = ObjectId.GenerateNewId().ToString(),
                                                                          Name = "DetailDetail2",
                                                                       }
                                               }
                                  },
                      new CrudDetail{
                        Id = ObjectId.GenerateNewId().ToString(),
                        CrudId = ObjectId.GenerateNewId().ToString(),
                        DetailName = "Detail2"
                      }
                    },

                };
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
