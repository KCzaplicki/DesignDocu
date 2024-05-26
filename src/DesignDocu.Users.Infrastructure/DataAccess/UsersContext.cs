using DesignDocu.Infrastructure.Authorization.Abstractions.DataAccess;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DesignDocu.Users.Infrastructure.DataAccess;

public class UsersContext(DbContextOptions<UsersContext> options) : IdentityDbContext<ApplicationUser>(options);