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
        public List<ProjectDataModel> ProjectList { get; set; }
    }

    public class ProjectDataModel : ProjectBaseModel
    {
        [XmlAttribute(AttributeName = "id")]
        public virtual string Id { get; set; }
    }

    public class ProjectBaseModel
    {
        [XmlElement(ElementName = "name")]
        public virtual string Name { get; set; }

        [XmlElement(ElementName = "abbreviation")]
        public virtual string Abbrevation { get; set; }

        [XmlElement(ElementName = "customer")]
        public virtual string Customer { get; set; }
    }


}
