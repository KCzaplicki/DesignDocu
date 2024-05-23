using DesignDocu.Authorization.DataAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DesignDocu.Users.Infrastructure.DataAccess;

public class UsersContext(DbContextOptions<UsersContext> options) : IdentityDbContext<ApplicationUser>(options);