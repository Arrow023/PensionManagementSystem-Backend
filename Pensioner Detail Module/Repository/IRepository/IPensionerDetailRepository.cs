using PensionManagementSystem.Models;

namespace Pensioner_Detail_Module.Repository.IRepository
{
    public interface IPensionerDetailRepository
    {
        public PensionerDetail GetPensionerDetail(long aadharNumber);
    }
}
