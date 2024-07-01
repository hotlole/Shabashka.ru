namespace Шабашка.Domain.Entity
{
    public class Profile
    {
        public long id { get; set; }

        public string Email { get; set; }

        public short Age { get; set; }

        public long UserId { get; set; }

        public User User { get; set; }
    }
}
