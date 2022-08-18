using PensionManagementSystem.Models;

namespace Process_Pension_Module.Repository.IRepository
{
    public interface IProcessPensionRepository 
    {
        public Task<PensionDetail> GetPensionDetail(long aadharNumber);
        public double CalculatePensionAmount(PensionerDetail pensionerDetail);
        public double CalculateBankServiceCharge(PensionerDetail pensionerDetail);
    }
}
