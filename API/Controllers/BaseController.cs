using AutoMapper;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BaseController: ControllerBase
    {
        protected readonly IMapper _mapper;
        
        public BaseController(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}