using System.Threading.Tasks;

namespace DataAccessLayer.Data.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        T GetWSDLResponseByDial(string dial);

        Task<T> GetWSDLResponseByDialAsync(string dial);
    }
}