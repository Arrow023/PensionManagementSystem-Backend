using log4net;
using Pension_Management_System;
using PensionManagementSystem.Data;
using PensionManagementSystem.Models;
using Process_Pension_Module.Repository.IRepository;
using System.Net.Http.Headers;

namespace Process_Pension_Module.Repository
{
    public class ProcessPensionRepository : IProcessPensionRepository
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(ProcessPensionRepository));
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProcessPensionRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Computes the Bank Service charge using bank types
        /// </summary>
        /// <param name="pensionerDetail">PensionerDetail object</param>
        /// <returns>bank service charge</returns>
        public double CalculateBankServiceCharge(PensionerDetail pensionerDetail)
        {
            if (pensionerDetail.Bank.Type == Bank.BankType.Public)
            {
                var charge = 500;
                return charge;
            }
            else
            {
                var charge = 550;
                return charge;
            }
        }

        /// <summary>
        /// Computes the Pension amount using pension types
        /// </summary>
        /// <param name="pensionerDetail">PensionerDetail object</param>
        /// <returns>pension amount</returns>
        public double CalculatePensionAmount(PensionerDetail pensionerDetail)
        {
            if (pensionerDetail.Type == PensionerDetail.PensionType.Self)
            {
                var amount = 0.8 * (pensionerDetail.SalaryEarned + pensionerDetail.Allowances);
                return amount;
            }
            else
            {
                var amount = 0.5 * (pensionerDetail.SalaryEarned + pensionerDetail.Allowances);
                return amount;
            }
        }

        /// <summary>
        /// Retrieves Pension Detail of an indivdual using Aadhar Number
        /// </summary>
        /// <param name="aadharNumber">a 12-digit number</param>
        /// <returns></returns>
        public async Task<PensionDetail> GetPensionDetail(long aadharNumber)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", _httpContextAccessor.HttpContext.Request.Headers.Authorization.ToString());
                var pensionerDetail = await httpClient.GetFromJsonAsync<PensionerDetail>(SD.PensionerDetailUrl + "?aadharNumber=" + aadharNumber);
                if (pensionerDetail == null)
                    return null;
                else
                {
                    PensionDetail pensionDetail = new PensionDetail
                    {
                        PensionAmount = CalculatePensionAmount(pensionerDetail),
                        BankServiceCharge = CalculateBankServiceCharge(pensionerDetail)
                    };
                    return pensionDetail;
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return null;
            }
        }

    }
}
