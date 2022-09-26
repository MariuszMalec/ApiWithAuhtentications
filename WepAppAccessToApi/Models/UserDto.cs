using AutoMapper.Configuration.Annotations;

namespace WepAppAccessToApi.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Nationality { get; set; }
        [Ignore]
        public string PasswordHash { get; set; }
        public int RoleId { get; set; }
        [Ignore]
        public Role Role { get; set; }
    }
}
