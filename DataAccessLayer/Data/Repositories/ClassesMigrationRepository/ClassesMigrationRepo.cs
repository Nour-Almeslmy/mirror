using DataAccessLayer.Data.Context;
using DataAccessLayer.Models;

namespace DataAccessLayer.Data.Repositories
{
    public class ClassesMigrationRepo : GenericRepo<WSDLResponseLog_ServiceClassesMigration>, IClassesMigrationRepo
    {
        //private readonly BucketSubscribtionModel db_;
        public ClassesMigrationRepo(ApplicationDbContext db) : base(db)
        {
            //this.db_ = db;
        }

        //public WSDLResponseLog_ServiceClassesMigration GetClassesMigrationResponses(string dial)
        //{
        //    WSDLResponseLog_ServiceClassesMigration EligibleServiceClassesMigrationResponses = db_.ClassesMigrationResponses.Find(dial);
        //    return EligibleServiceClassesMigrationResponses;
        //}
    }
}
