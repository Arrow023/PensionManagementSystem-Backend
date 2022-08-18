using log4net;
using Microsoft.EntityFrameworkCore;
using Pensioner_Detail_Module.Repository.IRepository;
using PensionManagementSystem.Data;
using PensionManagementSystem.Models;
using System.Security.Claims;

namespace Pensioner_Detail_Module.Repository
{
    public class PensionerDetailRepository : IPensionerDetailRepository
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(PensionerDetailRepository));
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PensionerDetailRepository(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Retrieves pensioner's detail using aadhaar Number
        /// </summary>
        /// <param name="aadharNumber">a 12-digit aadhaar number</param>
        /// <returns></returns>
        public PensionerDetail GetPensionerDetail(long aadharNumber=0)
        {
            try
            {
                PensionerDetail pensionerDetail;
                if (aadharNumber == 0)
                {
                    var user = _httpContextAccessor.HttpContext.User.Identity.Name;
                    pensionerDetail = _db.PensionerDetails.Include(x => x.Bank).FirstOrDefault(x => x.UserId.ToString() == user);
                    return pensionerDetail;
                }
                pensionerDetail = _db.PensionerDetails.Include(x=>x.Bank).FirstOrDefault(x => x.AadhaarNumber == aadharNumber);
                return pensionerDetail;

            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return null;
            }
        }
    }

}
