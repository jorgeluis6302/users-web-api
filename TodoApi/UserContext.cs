using Microsoft.EntityFrameworkCore;

namespace UsersApi.UserContext
{
    class UserDb : DbContext
    {
        public UserDb(DbContextOptions<UserDb> options)
            : base(options) { }

        public DbSet<User> Users => Set<User>();
    }

    class User
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public bool IsAdmin { get; set; }
    }

    class UserItemDTO
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public bool IsAdmin { get; set; }
        public UserItemDTO() { }
        public UserItemDTO(User useritem)
            => (Id, UserName, IsAdmin) = (useritem.Id, useritem.UserName, useritem.IsAdmin);
    }
}
