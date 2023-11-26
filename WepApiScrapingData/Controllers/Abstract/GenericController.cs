

using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebApiScrapingData.Core;
using WebApiScrapingData.Domain.Interface;
using WebApiScrapingData.Infrastructure.Repository.Generic;
using WepApiScrapingData.DTOs.Abstract;

namespace WepApiScrapingData.Controllers.Abstract
{
    public abstract class GenericController<T, D, R> : ControllerBase
        where T : class, ITIdentity, new()
        where D : IdentityDto,  new() 
        where R : Repository<T>

    {
        #region Fields
        protected readonly ILogger<T> _logger;
        protected readonly GenericMapper<T,D> _mapper;
        protected readonly R _repository;
        #endregion

        #region Constructor
        public GenericController(ILogger<T> logger, GenericMapper<T,D> mapper, R repository)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }
        #endregion

        #region Public methods
        #region Get
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public virtual async Task<IActionResult> GetAll()
        {
            IActionResult result = this.BadRequest();

            try
            {
                var entities = await _repository.GetAll();
                result = this.Ok(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                result = this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return result;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Tags = new[] { "GET Operations" })]
        [Route("{id}")]
        public virtual async Task<IActionResult> GetSingle(int id)
        {
            IActionResult result = this.BadRequest();

            try
            {
                result = this.Ok(await _repository.Get(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                result = this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return result;
        }
        #endregion

        #region Post
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("Add")]
        public virtual async Task<IActionResult> Add([FromBody] D dto)
        {
            IActionResult result = this.BadRequest();

            try
            {
                T entity = _mapper.MapReverse(dto);
                var success = await _repository.Add(entity);

                if (success)
                {
                    result = this.CreatedAtAction(nameof(GetSingle), new { id = entity.Id }, dto);
                }

                return result;
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erreur Accès BDD");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("AddRange")]
        public virtual async Task<IActionResult> AddRange([FromBody] IEnumerable<D> dtos)
        {
            IActionResult result = this.BadRequest();

            try
            {
                IList<T> entities = new List<T>();
                foreach(D dto in dtos)
                {
                    entities.Add(_mapper.MapReverse(dto));
                }

                var success = await _repository.AddRange(entities);

                if (success)
                {
                    result = this.CreatedAtAction(nameof(GetAll), dtos);
                }

                return result;
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erreur Accès BDD");
            }
        }
        #endregion

        #region Put
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("Update")]
        public virtual IActionResult Update([FromBody] D dto)
        {
            IActionResult result = this.BadRequest();

            try
            {
                T entity = _mapper.MapReverse(dto);
                var success = _repository.Update(entity);

                if (success)
                {
                    result = this.CreatedAtAction(nameof(GetSingle), new { id = entity.Id }, dto);
                }

                return result;
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erreur Accès BDD");
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("UpdateRange")]
        public virtual IActionResult UpdateRange([FromBody] IEnumerable<D> dtos)
        {
            IActionResult result = this.BadRequest();

            try
            {
                IList<T> entities = new List<T>();
                foreach (D dto in dtos)
                {
                    entities.Add(_mapper.MapReverse(dto));
                }

                var success = _repository.UpdateRange(entities);

                if (success)
                {
                    result = this.CreatedAtAction(nameof(GetAll), dtos);
                }

                return result;
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erreur Accès BDD");
            }
        }
        #endregion

        #region Delete
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("Remove/{id}")]
        public virtual async Task<IActionResult> Remove(int id)
        {
            IActionResult result = this.BadRequest();

            try
            {

                T entity = await _repository.Get(id);
                if (entity != null)
                {
                    var success = _repository.Remove(entity);

                    if (success)
                    {
                        result = this.CreatedAtAction(nameof(GetSingle), new { id = entity.Id }, entity);
                    }
                }
                else
                {
                    result = this.StatusCode(StatusCodes.Status400BadRequest, "Aucune donnée à supprimer");
                }

                return result;
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erreur Accès BDD");
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("RemoveRange")]
        public virtual async Task<IActionResult> RemoveRange(IEnumerable<int> ids)
        {
            IActionResult result = this.BadRequest();

            try
            {
                IList<T> entities = new List<T>();
                foreach (int id in ids)
                {
                    T entity = await _repository.Get(id);

                    if (entity != null)
                        entities.Add(entity);
                }

                if (entities.Count > 0)
                {
                    var success = _repository.RemoveRange(entities);

                    if (success)
                    {
                        result = this.CreatedAtAction(nameof(GetAll), entities);
                    }
                }
                else
                {
                    result = this.StatusCode(StatusCodes.Status400BadRequest, "Aucune donnée à supprimer");
                }

                return result;
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erreur Accès BDD");
            }
        }
        #endregion
        #endregion
    }
}
