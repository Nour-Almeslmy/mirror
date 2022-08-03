using BusinessLogicLayer.checkDataProfileStatus;
using BusinessLogicLayer.getEligibleServiceClassesMigration;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace BusinessLogicLayer.Helpers
{
    public class Mapper : IMApper
    {
        public ProfileStatusServiceResponseDTO mapToProfileStatusServiceSecureResponse(checkDataProfileStatus_out ProfileStatusResponseLog)
        {
            var SecureServiceResponse = new ProfileStatusServiceResponseDTO()
            {
                BucketsInfo = new List<DataAccessLayer.DTOs.BucketInfo>(),
                ErrorDoc = new ErrorDoc()
                {
                    Status = ProfileStatusResponseLog.errorDoc.status,
                    ErrorMessage = ProfileStatusResponseLog.errorDoc.errorMessage
                }
            };

            foreach (var bucket in ProfileStatusResponseLog.BucketInfo)
            {
                DataAccessLayer.DTOs.BucketInfo bucketInfo = new DataAccessLayer.DTOs.BucketInfo()
                {
                    CurrentBucketId = bucket.currentBucketId,
                    CurrentBucketName = bucket.currentBucketName,
                    Options = new List<Option>()
                };

                foreach (var optionList in bucket.optionsList)
                {
                    if (optionList.optionId != null)
                    {
                        Option option = new Option()
                        {
                            optionId = optionList.optionId,
                            optionName = optionList.optionName,
                        };

                        bucketInfo.Options.Add(option);
                    }
                }

                SecureServiceResponse.BucketsInfo.Add(bucketInfo);
            }

            #region Error Message Mapping
            //in case we want to alter the error message.
            /*
            if (Enum.TryParse(ProfileStatusResponseLog.errorDoc.errorCode, out ErrorCode errorCode))
            {
                switch (errorCode)
                {
                    case ErrorCode.er0000:
                        SecureServiceResponse.ErrorDoc.ErrorMessage = ErrorMessageDictionary[ErrorCode.er0000];
                        break;
                    case ErrorCode.er2056:
                        SecureServiceResponse.ErrorDoc.ErrorMessage = ErrorMessageDictionary[ErrorCode.er2056];
                        break;
                    case ErrorCode.er3000:
                        SecureServiceResponse.ErrorDoc.ErrorMessage = ErrorMessageDictionary[ErrorCode.er3000];
                        break;
                    default:
                        SecureServiceResponse.ErrorDoc.ErrorMessage = ProfileStatusResponseLog.errorDoc.errorMessage;
                        break;
                }
            }
            else
                SecureServiceResponse.ErrorDoc.ErrorMessage = String.Empty;
            */
            #endregion

            return SecureServiceResponse;
        }

        //Dictionary<ErrorCode, string> ErrorMessageDictionary = new Dictionary<ErrorCode, string>()
        //{
        //    {ErrorCode.er0000, "Success"},
        //    {ErrorCode.er2056, "Technical Error"},
        //    {ErrorCode.er3000, "Not Eligibile"}
        //};

        public ClassMigrationDTO mapClassMigrationToDTO(getEligibleServiceClassesMigration_Output classMigration_out)
        {
            var classMigrationDTO = new ClassMigrationDTO()
            {
                errorDoc = new ErrorDoc()
                {
                    Status = classMigration_out.errorDoc.status,
                    ErrorMessage = classMigration_out.errorDoc.errorDesc
                },
                familyIds = new List<FamilyId>()
            };

            foreach(var famId in classMigration_out.familyId)
            {
                var FamId = new FamilyId()
                {
                    familyId = famId.familyId1,
                    familyName = famId.familyName,
                    serviceClassParameters = new List<ServiceClassParameter>()
                };

                foreach(var service in famId.serviceClassParameters)
                {
                    var serviceClassParameter = new ServiceClassParameter()
                    {
                        serviceClassId = service.serviceClassId,
                        serviceClassName = service.serviceClassName
                    };

                    FamId.serviceClassParameters.Add(serviceClassParameter);
                }

                classMigrationDTO.familyIds.Add(FamId);
            }

            return classMigrationDTO;
        }
    }
}
