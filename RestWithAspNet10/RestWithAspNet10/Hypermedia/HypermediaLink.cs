using System.Xml.Serialization;

namespace RestWithAspNet10.Hypermedia
{
    public class HypermediaLink
    {
        [XmlAttribute]
        public string Rel { get; set; } = string.Empty;

        [XmlAttribute]
        public string Href { get; set; } = string.Empty;

        [XmlAttribute]
        public string Type { get; set; } = "application/json";

        [XmlAttribute]
        public string Action { get; set; } = string.Empty;
    }
}
