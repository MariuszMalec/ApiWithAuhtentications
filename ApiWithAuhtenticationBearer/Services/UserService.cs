
using ApiWithAuhtenticationBearer.Entities;

namespace ApiWithAuhtenticationBearer.Services
{
    public class UserService
    {

        private static List<User> Users = new List<User>()
        {
            new User(1, "mm@example.com" , "Mariusz", "Malec", DateTime.Now, "polish","",1),
            new User(1, "mm@example.com" , "Mariusz", "Malec", DateTime.Now, "polish","",1)
        };
        public List<User> GetAll()
        {
            return Users;
        }
        public User GetById(int id)
        {
            //Users = GetAll();
            return Users.SingleOrDefault(u => u.Id == id);
        }

        public void Delete(int id)
        {
            var user = GetById(id);
            Users.Remove(user);
        }

        public void Create(User model)
        {
            model.Id = GetNextId();
            Users.Add(model);
        }
        public int GetNextId()
        {
            if (!Users.Any())
                return 0;
            return (Users?.Max(m => m.Id) ?? 0) + 1;
        }
    }
}
