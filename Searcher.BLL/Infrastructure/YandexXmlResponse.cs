using System;
using System.Xml;
using System.Xml.Serialization;

namespace Searcher.BLL.Infrastructure
{
    [Serializable()]
    public class Doc
    {
        [XmlElement(ElementName = "url")]
        public string Url { get; set; }
        [XmlElement(ElementName = "title")]
        public XmlNode Title { get; set; }

        [XmlArray("passages")]
        [XmlArrayItem(ElementName = "passage", Type = typeof(XmlNode))]
        public XmlNode[] Passage { get; set; }
    }

    [Serializable()]
    public class Group
    {
        [XmlElement(ElementName = "doc")]
        public Doc Doc { get; set; }
    }

    [Serializable()]
    public class Results
    {
        [XmlArray("grouping")]
        [XmlArrayItem(ElementName = "group", Type = typeof(Group))]
        public Group[] Group { get; set; }
    }

    [Serializable()]
    public class Response
    {
        [XmlElement(ElementName = "results")]
        public Results Results { get; set; }
    }

    [Serializable()]
    [XmlRoot(ElementName = "yandexsearch")]
    public class YandexSearch
    {
        [XmlElement(ElementName = "response")]
        public Response Response { get; set; }
    }
}
