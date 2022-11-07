using BusinessLogicLayer.checkDataProfileStatus;
using BusinessLogicLayer.getEligibleServiceClassesMigration;
using BusinessLogicLayer.Helpers;
using BusinessLogicLayer.Utilities;
using DataAccessLayer.Data.Repositories;
using DataAccessLayer.Data.UnitOfWork;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.ControllerHelper.ProfileController
{


    //Change
    public class ProfileControllerHelper : IProfileControllerHelper
    {
        private readonly IUnitOfWork unitOfWork_;
        private readonly IMApper Mapper_;
        private readonly IXMLSerializer XMLSerializer_;
        public ProfileControllerHelper (IUnitOfWork unitOfWork, IXMLSerializer XMLSerializer, IMApper Mapper)
        {
            this.unitOfWork_ = unitOfWork; this.XMLSerializer_ = XMLSerializer; this.Mapper_ = Mapper;
        }

        public ProfileStatusServiceResponseDTO GetProfileStatusSecureServiceResponse(string dial, bool mock)
        {
            if (mock)
            {
                WSDLResponseLog_ProfileStatus profileStatusResponse = unitOfWork_.ProfileStatusRepository.GetWSDLResponseByDial(dial);

                if (profileStatusResponse == null)
                {
                    return null;
                }

                checkDataProfileStatus_out ProfileStatusResponseLog =
                    XMLSerializer_.DeserializeFromXmlString<checkDataProfileStatus_out>(profileStatusResponse.ResponseLog);

                ProfileStatusServiceResponseDTO secureServiceResponse = Mapper_.mapToProfileStatusServiceSecureResponse(ProfileStatusResponseLog);

                return secureServiceResponse;
            }
            else
            {
                var CheckProfileStatusService = new BusinessLogicLayer.checkDataProfileStatus.MEAI_OnlineServices_2017webServicesGOcheckDataProfileStatus_V2_WSDL();

                var CheckProfileStatusService_Input = new BusinessLogicLayer.checkDataProfileStatus.checkDataProfileStatus_in()
                {
                    dial = dial
                };

                BusinessLogicLayer.checkDataProfileStatus.checkDataProfileStatus_out CheckProfileStatusService_Out =
                    CheckProfileStatusService.checkDataProfileStatus_V2(CheckProfileStatusService_Input);

                ProfileStatusServiceResponseDTO secureServiceResponse = Mapper_.mapToProfileStatusServiceSecureResponse(CheckProfileStatusService_Out);

                return secureServiceResponse;
            }
        }

        public ClassMigrationDTO GetClassesMigrationResponse(string dial_, bool mock)
        {
            if (mock)
            {
                var response = unitOfWork_.ClassMigrationRepository.GetWSDLResponseByDial(dial_);

                if (response == null)
                {
                    return null;
                }

                getEligibleServiceClassesMigration_Output ClassesMigrationResponse =
                    XMLSerializer_.DeserializeFromXmlString<getEligibleServiceClassesMigration_Output>(response.ResponseLog);

                var classMigrationResponseDTO = Mapper_.mapClassMigrationToDTO(ClassesMigrationResponse);

                return classMigrationResponseDTO;
            }
            else
            {
                var EligibleServiceClassesMigration_WSDL =
                        new BusinessLogicLayer.getEligibleServiceClassesMigration.getEligibleServiceClassesMigration_WSDL();

                var EligibleServiceClassesMigration_Input =
                    new BusinessLogicLayer.getEligibleServiceClassesMigration.getEligibleServiceClassesMigration_Input()
                    {
                        dial = dial_
                    };

                var EligibleServiceClassesMigration_Out = 
                        EligibleServiceClassesMigration_WSDL.getEligibleServiceClassesMigration(EligibleServiceClassesMigration_Input);

                var classMigrationResponseDTO = Mapper_.mapClassMigrationToDTO(EligibleServiceClassesMigration_Out);

                return classMigrationResponseDTO;
            }
        }
    }
}
