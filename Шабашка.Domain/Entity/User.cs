using Шабашка.Domain.Enum;

namespace Шабашка.Domain.Entity
{
    public class User
    {
        public long id { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public Role Role { get; set; }

        public Profile Profile { get; set; }

    }
}