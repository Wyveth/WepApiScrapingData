﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Class = WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Infrastructure.Repository;
using WepApiScrapingData.Controllers.Abstract;
using WepApiScrapingData.DTOs.Concrete.Quizz;
using WepApiScrapingData.ExtensionMethods;
using WebApiScrapingData.Infrastructure.Mapper;
using WebApiScrapingData.Infrastructure.Data;
using ClassQuizz = WebApiScrapingData.Domain.Class.Quizz;

namespace WepApiScrapingData.Controllers.Quizz
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    public class QuizzController : GenericController<Class.Quizz, QuizzDto, QuizzRepository>
    {
        #region Constructors
        public QuizzController(ILogger<Class.Quizz> logger, GenericMapper<Class.Quizz, QuizzDto> mapper, QuizzRepository repository, ScrapingContext context) : base(logger, mapper, repository, context)
        {
        }
        #endregion

        [HttpGet]
        [Route("GenerateQuizz")]
        public async Task<ClassQuizz.Quizz> GenerateQuizz()
        {
            return await _repository.GenerateQuizz(null, true, false, false, false, false, false, false, false, false, false, true, false, false);
        }
    }
}
