using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class ClassMigrationDTO
    {
        public List<FamilyId> familyIds { get; set; }
        public ErrorDoc errorDoc { get; set; }
    }

    public class FamilyId
    {
        public string familyId { get; set; }
        public string familyName { get; set; }
        public List<ServiceClassParameter> serviceClassParameters { get; set; }
    }

    public class ServiceClassParameter
    {
        public string serviceClassId { get; set; }
        public string serviceClassName { get; set; }
    }
}
