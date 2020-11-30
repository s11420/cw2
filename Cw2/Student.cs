using System.Xml.Serialization;

namespace Cw2
{
    public class Student
    {
        [XmlElement(elementName: "fname")]
        public string Imie { get; set; }
        [XmlElement(elementName: "lname")]
        public string Nazwisko { get; set; }
        public string Email { get; set; }
        [XmlElement(elementName: "indexNumber")]
        public string Index { get; set; }
        public string BirthDate { get; set; }
        public string MothersName { get; set; }
        public string FathersName { get; set; }
        public Studies Studies { get; set; }
    }
}