using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pensioner_Detail_Module.Repository.IRepository;
using PensionManagementSystem.Dtos;
using PensionManagementSystem.Models;

namespace Pensioner_Detail_Module.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PensionerDetailController : ControllerBase
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(PensionerDetailController));
        private readonly IPensionerDetailRepository _repo;
        private readonly IMapper _mapper;

        public PensionerDetailController(IPensionerDetailRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200,Type=typeof(PensionerDetailDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Get(long aadharNumber)
        {
            try
            {
                var pensionerDetail = _repo.GetPensionerDetail(aadharNumber);
                if (pensionerDetail == null)
                    return NotFound();
                else
                {
                    var objDto = _mapper.Map<PensionerDetailDto>(pensionerDetail);
                    return Ok(objDto);
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return StatusCode(500,ex);
            }
        }
    }
}
