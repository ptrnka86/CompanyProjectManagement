using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CompanyProjectManagement.Data.Model
{
    [Serializable]
    [XmlRoot(ElementName = "projects")]
    public class Projects
    {
        [XmlElement(ElementName = "project")]
        public Project[] Project { get; set; }
    }

    public class Project
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }

        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "abbreviation")]
        public string Abbrevation { get; set; }

        [XmlElement(ElementName = "customer")]
        public string Customer { get; set; }
    }
}
