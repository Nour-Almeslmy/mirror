using BusinessLogicLayer.checkDataProfileStatus;
using BusinessLogicLayer.getEligibleServiceClassesMigration;

namespace BusinessLogicLayer.Utilities
{
    public interface IXMLSerializer
    {
        T DeserializeFromXmlString<T>(string responseLog);
    }
}