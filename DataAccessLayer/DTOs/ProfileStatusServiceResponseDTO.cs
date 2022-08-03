using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.DTOs
{
    public class ProfileStatusServiceResponseDTO
    {
        public List<BucketInfo> BucketsInfo { get; set; }
        public ErrorDoc ErrorDoc { get; set; }

    }

    public class Option
    {
        public string optionId { get; set; }
        public string optionName { get; set; }
    }

    public class BucketInfo
    {
        public List<Option> Options { get; set; }

        public string CurrentBucketName { get; set; }

        public string CurrentBucketId { get; set; }

    }

    public class ErrorDoc
    {
        public string Status { get; set; }

        //public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}