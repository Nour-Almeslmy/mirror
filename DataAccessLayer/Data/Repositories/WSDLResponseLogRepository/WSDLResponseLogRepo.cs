using DataAccessLayer.Data.Context;
using DataAccessLayer.Models;


namespace DataAccessLayer.Data.Repositories
{
    public class WSDLResponseLogRepo : GenericRepo<WSDLResponseLog>, IWSDLResponseLogRepo
    {
        public WSDLResponseLogRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}
