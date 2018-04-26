using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Collections.Generic;
using NewApp.Entities;
using NewApp.Models.DTOs;
using NewApp.Services.Generic;
using NewApp.Models;
using NewApp.Helpers;

namespace NewApp.Controllers
{
    [Route("api/[controller]")]
    public class GenericCRUDController<TEntity, TEntityDTO, TService> : Controller
        where TEntity : BaseEntity
        where TEntityDTO : BaseModel
        where TService : IService<TEntity, TEntityDTO>
    {
        protected readonly TService service;

        public GenericCRUDController(TService service)
        {
            this.service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(DataResponseResult<List<BaseModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(type: typeof(CommonWithoutDataResModel), statusCode: (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(CommonWithoutDataResModel), (int)HttpStatusCode.NotFound)]
        public virtual async Task<IActionResult> GetAll()
        {
            var data = await service.GetAll();
            return ResponseHelper.Success(data: data);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DataResponseResult<BaseModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CommonWithoutDataResModel), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(CommonWithoutDataResModel), (int)HttpStatusCode.NotFound)]
        public virtual async Task<IActionResult> Get(string id)
        {
            var data = await service.Get(id);
            if (data == null)
                return ResponseHelper.NotFound(string.Empty);

            return ResponseHelper.Success(data: data);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CommonWithoutDataResModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(CommonWithoutDataResModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CommonWithoutDataResModel), (int)HttpStatusCode.Unauthorized)]
        public virtual async Task<IActionResult> Create([FromBody]TEntityDTO model)
        {
            if (!ModelState.IsValid)
                return ResponseHelper.BadRequest(ModelState);

            var entity = await service.Insert(model);
            return ResponseHelper.Created(data: entity);
        }

        [HttpPost("upsert")]
        [ProducesResponseType(typeof(CommonWithoutDataResModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CommonWithoutDataResModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CommonWithoutDataResModel), (int)HttpStatusCode.Unauthorized)]
        public virtual async Task<IActionResult> UpSert([FromBody]TEntityDTO model)
        {
            if (!ModelState.IsValid)
                return ResponseHelper.BadRequest(ModelState);

            var entity = await service.UpSert(model);
            return ResponseHelper.Success(data: entity);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CommonWithoutDataResModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CommonWithoutDataResModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CommonWithoutDataResModel), (int)HttpStatusCode.Unauthorized)]
        public virtual async Task<IActionResult> Update(string id, [FromBody]TEntityDTO model)
        {
            if (!ModelState.IsValid)
                return ResponseHelper.BadRequest(ModelState);

            var checkExists = await service.CheckExists(id);
            if (!checkExists)
                return ResponseHelper.NotFound(string.Empty);

            model.Id = id;
            var entity = await service.Update(model);

            return ResponseHelper.Success(data: entity);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(CommonWithoutDataResModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CommonWithoutDataResModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CommonWithoutDataResModel), (int)HttpStatusCode.Unauthorized)]
        public virtual async Task<IActionResult> Patch(string id, [FromBody]TEntityDTO model)
        {
            if (!ModelState.IsValid)
                return ResponseHelper.BadRequest(ModelState);

            var checkExists = await service.CheckExists(id);
            if (!checkExists)
                return ResponseHelper.NotFound(string.Empty);

            model.Id = id;
            await service.Update(model);
            return ResponseHelper.Success();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(CommonWithoutDataResModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CommonWithoutDataResModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CommonWithoutDataResModel), (int)HttpStatusCode.Unauthorized)]
        public virtual async Task<IActionResult> Delete(string id)
        {
            var checkExists = await service.CheckExists(id);
            if (!checkExists)
                return ResponseHelper.NotFound(string.Empty);

            await service.Delete(id);
            return ResponseHelper.Success();
        }
    }
}