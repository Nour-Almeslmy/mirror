using DataAccessLayer.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        public UnitOfWork(IProfileStatusRepo profileStatusRepo, IClassesMigrationRepo classMigrationRepo)
        {
            ProfileStatusRepository = profileStatusRepo;
            ClassMigrationRepository = classMigrationRepo;
        }

        public IProfileStatusRepo ProfileStatusRepository { get; }

        public IClassesMigrationRepo ClassMigrationRepository { get; }
    }
}
