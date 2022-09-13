namespace ApiWithAuhtenticationBearer.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string PasswordHash { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public User(int id, string email, string firstName, string lastName, DateTime? dateOfBirth, string nationality, string passwordHash, int roleId)
        {
            Id = id;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Nationality = nationality;
            PasswordHash = passwordHash;
            RoleId = roleId;
        }

        public User()
        {
        }
    }
}
