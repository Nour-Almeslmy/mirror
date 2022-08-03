using DataAccessLayer.Data.Repositories;

namespace DataAccessLayer.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IClassesMigrationRepo ClassMigrationRepository { get; }
        IProfileStatusRepo ProfileStatusRepository { get; }
    }
}