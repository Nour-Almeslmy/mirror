using BusinessLogicLayer.Helpers;
using DataAccessLayer.DTOs;

namespace BusinessLogicLayer.ControllerHelper.ProfileController
{
    public interface IProfileControllerHelper
    {
        ProfileStatusServiceResponseDTO GetProfileStatusSecureServiceResponse(string dial, bool mock);
        ClassMigrationDTO GetClassesMigrationResponse(string dial_, bool mock);
    }
}