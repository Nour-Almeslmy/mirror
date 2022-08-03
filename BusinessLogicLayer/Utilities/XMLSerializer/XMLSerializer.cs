using BusinessLogicLayer.checkDataProfileStatus;
using BusinessLogicLayer.getEligibleServiceClassesMigration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BusinessLogicLayer.Utilities
{
    public class XMLSerializer : IXMLSerializer
    {
        #region Non-Generic Deserialization
        //public checkDataProfileStatus_out DeserializeFromXmlString(string responseLog)
        //{
        //    checkDataProfileStatus_out ProfileStatusResponse = null;

        //    var xmlSerializer = new XmlSerializer(typeof(checkDataProfileStatus_out));

        //    using (TextReader reader = new StringReader(responseLog))
        //    {
        //        ProfileStatusResponse = xmlSerializer.Deserialize(reader) as checkDataProfileStatus_out;
        //    }

        //    return ProfileStatusResponse;
        //}

        //public getEligibleServiceClassesMigration_Output DeserializeFromXmlString_ClassMigration(string responseLog)
        //{
        //    getEligibleServiceClassesMigration_Output response = null;

        //    var xmlSerializer = new XmlSerializer(typeof(getEligibleServiceClassesMigration_Output));

        //    using (TextReader reader = new StringReader(responseLog))
        //    {
        //        response = xmlSerializer.Deserialize(reader) as getEligibleServiceClassesMigration_Output;
        //    }

        //    return response;
        //}
        #endregion

        public T DeserializeFromXmlString<T>(string responseLog)
        {
            T ProfileStatusResponse = default;

            var xmlSerializer = new XmlSerializer(typeof(T));

            using (TextReader reader = new StringReader(responseLog))
            {
                //ProfileStatusResponse = (T)xmlSerializer.Deserialize(reader);
                var value = xmlSerializer.Deserialize(reader);
                ProfileStatusResponse = (T)Convert.ChangeType(value, typeof(T));
            }

            return ProfileStatusResponse;
        }
    }
}
