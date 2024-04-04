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

    public class User : UserBaseModel
    {
        [XmlElement(ElementName = "password")]
        public virtual string Password { get; set; }
    }

    public class UserBaseModel
    {
        [XmlAttribute(AttributeName = "id")]
        public virtual Guid UserId { get; set; }

        [XmlElement(ElementName = "username")]
        public virtual string UserName { get; set; }
    }
}
