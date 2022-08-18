using AutoMapper;
using JWTAuthentication.Repository.IRepository;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PensionManagementSystem.Dtos;
using PensionManagementSystem.Models;

namespace JWTAuthentication.Controllers
{
    public class JwtAuthenticationController : Controller
    {
        private readonly IAuthRepository _authRepo;
        private readonly ILog _log = LogManager.GetLogger(typeof(JwtAuthenticationController));
        private readonly IMapper _mapper;

        public JwtAuthenticationController(IAuthRepository authRepo, IMapper mapper)
        {
            _authRepo = authRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// API to authenticate an user
        /// </summary>
        /// <param name="model">authentication model</param>
        /// <returns>User object</returns>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200,Type=typeof(UserDto))]
        [ProducesResponseType(500)]
        public IActionResult Authenticate([FromBody] Authentication model)
        {
            _log.Error("Test error");
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var user = _authRepo.Authenticate(model.Username, model.Password);
                if (user == null)
                {
                    return BadRequest(new { message = "Username or password is invalid" });
                }
                var objDto = _mapper.Map<UserDto>(user);
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                return StatusCode(500,ex);
            }
            
        }

        /// <summary>
        /// API to register an user in the database
        /// </summary>
        /// <param name="model">Authentication model</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Register([FromBody] Authentication model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                bool ifUniqueUser = _authRepo.IsUniqueUser(model.Username);
                if (!ifUniqueUser)
                {
                    return BadRequest(new { message = "Username already exists" });
                }
                var user = _authRepo.Register(model.Username, model.Password);
                if (user == null)
                {
                    return BadRequest(new { message = " Error while registering" });
                }
                return Ok();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                return StatusCode(500, ex);
            }
            
        }

    }
}
