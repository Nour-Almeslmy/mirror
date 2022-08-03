using DataAccessLayer.Models;

namespace DataAccessLayer.Data.Repositories
{
    public interface IClassesMigrationRepo : IGenericRepository<WSDLResponseLog_ServiceClassesMigration>
    {
        //WSDLResponseLog_ServiceClassesMigration GetClassesMigrationResponses(string dial);
    }
}