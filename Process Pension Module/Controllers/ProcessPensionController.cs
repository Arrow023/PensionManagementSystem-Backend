using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PensionManagementSystem.Dtos;
using PensionManagementSystem.Models;
using Process_Pension_Module.Repository.IRepository;

namespace Pension_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProcessPensionController : ControllerBase
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(ProcessPensionController));
        private readonly IProcessPensionRepository _repo;
        private readonly IMapper _mapper;

        public ProcessPensionController(IProcessPensionRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves the pension details by using Process Pension Microservice
        /// </summary>
        /// <param name="model">contains aadhaar number</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200,Type=typeof(PensionDetailDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Post([FromBody]ProcessPensionInput model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid pensioner detail provided, please provide valid detail");
                }
                var pensionDetail = await _repo.GetPensionDetail(Convert.ToInt64(model.AadhaarNumber));
                if (pensionDetail == null)
                    return NotFound();
                else
                {
                    var objDto = _mapper.Map<PensionDetailDto>(pensionDetail);
                    return Ok(objDto);
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                return StatusCode(500, ex);
            }
        }
    }
}
