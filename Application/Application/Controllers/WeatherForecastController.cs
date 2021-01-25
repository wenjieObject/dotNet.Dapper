using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity;
using IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private IDeptRepository _ideptRepository;

        private ITestRepository _itestRepository;


        public WeatherForecastController( IDeptRepository ideptRepository, ITestRepository itestRepository)
        {
            _ideptRepository = ideptRepository;
            _itestRepository = itestRepository;
        }

        [HttpGet]
        public  string Get()
        {
            _itestRepository.InvokeApi();
            return _ideptRepository.GetSingle("12");
        }
    }
}
