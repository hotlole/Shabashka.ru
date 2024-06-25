namespace Шабашка.Domain.Entity
{
    public class User
    {
        public long id { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Name { get; set; }

        public Role role { get; set; }
    }
}