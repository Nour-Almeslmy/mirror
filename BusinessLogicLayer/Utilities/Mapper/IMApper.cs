using BusinessLogicLayer.checkDataProfileStatus;
using BusinessLogicLayer.getEligibleServiceClassesMigration;
using DataAccessLayer.DTOs;

namespace BusinessLogicLayer.Helpers
{
    public interface IMApper
    {
        ProfileStatusServiceResponseDTO mapToProfileStatusServiceSecureResponse(checkDataProfileStatus_out ProfileStatusResponseLog);

        ClassMigrationDTO mapClassMigrationToDTO(getEligibleServiceClassesMigration_Output classMigration_out);
    }
}