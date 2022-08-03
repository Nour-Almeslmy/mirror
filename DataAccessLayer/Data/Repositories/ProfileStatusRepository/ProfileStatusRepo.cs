using DataAccessLayer.Data.Context;
using DataAccessLayer.Models;


namespace DataAccessLayer.Data.Repositories
{
    public class ProfileStatusRepo : GenericRepo<WSDLResponseLog_ProfileStatus>, IProfileStatusRepo
    {
        //private readonly BucketSubscribtionModel db_;
        public ProfileStatusRepo(ApplicationDbContext db) : base(db)
        {
            //this.db_ = db;
        }

        //public WSDLResponseLog_ProfileStatus GetProfileStatusResponse(string dial)
        //{
        //    WSDLResponseLog_ProfileStatus profileStatusResponse = db_.ProfileStatusResponses.Find(dial);
        //    return profileStatusResponse;
        //}
    }
}
