using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Searcher.BLL.Infrastructure.Helpers
{
    public static class XmlSerializationUtil
    {
        public static T Deserialize<T>(string xmlText)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (TextReader sr = new StringReader(xmlText))
            {
                using (var reader = new XmlTextReader(sr))
                {
                    return (T)xmlSerializer.Deserialize(reader);
                }
            }
        }
    }
}
