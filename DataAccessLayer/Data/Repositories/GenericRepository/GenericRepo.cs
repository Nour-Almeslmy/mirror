using DataAccessLayer.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data.Repositories
{
    public class GenericRepo<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext context_;
        public GenericRepo(ApplicationDbContext context)
        {
            this.context_ = context;
        }

        public T GetWSDLResponseByDial(string dial)
        {
            return context_.Set<T>().Find(dial);
        }

        public async Task<T> GetWSDLResponseByDialAsync(string dial)
        {
            return await context_.Set<T>().FindAsync(dial);
        }
    }
}
