using Models;
using MongoDB.Bson;
using System;
using System.Web.Http;

namespace MongoDBGenericRepo.Controllers
{
    [RoutePrefix("api/crud")]
    public class CrudController : ApiController
    {

        private readonly ApplicationService.ApplicationService _applicationService;
        public CrudController(ApplicationService.ApplicationService applicationService)
        {
            this._applicationService = applicationService;
        }
        [HttpGet]
        [Route("getbyid")]
        public IHttpActionResult GetById(string id)
        {
            try
            {
                return Ok(_applicationService.GetById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        [Route("save")]
        public IHttpActionResult Save(CrudModel crudModel)
        {
            try
            {
                return Ok(_applicationService.Save(crudModel));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        [Route("put")]
        public IHttpActionResult Put()
        {
            try
            {
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("delete")]
        public IHttpActionResult Delete(string id)
        {
            try
            {
                return Ok(_applicationService.Delete(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
