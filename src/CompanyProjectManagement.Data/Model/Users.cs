using System.Xml.Serialization;

namespace CompanyProjectManagement.Data.Model
{
    [Serializable]
    [XmlRoot(ElementName = "users")]
    public class Users
    {
        [XmlElement(ElementName = "user")]
        public List<User> UserList { get; set; }
    }

    public class User
    {
        [XmlAttribute(AttributeName = "id")]
        public Guid UserId { get; set; }

        [XmlElement(ElementName = "username")]
        public string UserName { get; set; }

        [XmlElement(ElementName = "password")]
        public string Password { get; set; }
    }
}
