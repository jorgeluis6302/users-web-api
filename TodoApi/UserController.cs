using Microsoft.EntityFrameworkCore;
using UsersApi.UserContext;

namespace UsersApi.Controllers
{
    class UserController
    {
        public static async Task<List<UserItemDTO>> GetAll(UserDb db)
        {
            return await db.Users.Select(user => new UserItemDTO(user)).ToListAsync();
        }

        public static async Task<IResult> GetDetail(UserDb db, int id)
        {
            return await db.Users.FindAsync(id)
                    is User user
                        ? Results.Ok(new UserItemDTO(user))
                        : Results.NotFound();
        }

        public static async Task<List<User>> GetAdmins(UserDb db)
        {
            return await db.Users.Where(user =>user.IsAdmin).ToListAsync();
        }

        public static async Task<IResult> Create(UserDb db, UserItemDTO useritem)
        {
            var user = new User
            {
                Id = useritem.Id,
                UserName = useritem.UserName,
                IsAdmin = useritem.IsAdmin,
            };
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return Results.Created($"/users/{user.Id}", new UserItemDTO(user));
        }

        public static async Task<IResult> ModifyWhole(UserDb db, int id, UserItemDTO newUser)
        {
            var user = await db.Users.FindAsync(id);
            if (user is null) return Results.NotFound();
            user.UserName = newUser.UserName;
            user.IsAdmin = newUser.IsAdmin;
            await db.SaveChangesAsync();
            return Results.NoContent();
        }

        public static async Task<IResult> Remove(UserDb db, int id)
        {
            if (await db.Users.FindAsync(id) is User user)
            {
                db.Users.Remove(user);
                await db.SaveChangesAsync();
                return Results.Ok(new UserItemDTO(user));
            }
            return Results.NotFound();
        }
    }
}
