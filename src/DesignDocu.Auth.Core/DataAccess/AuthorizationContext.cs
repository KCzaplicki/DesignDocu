using DesignDocu.Authorization.DataAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DesignDocu.Authorization.DataAccess;

public class AuthorizationContext(DbContextOptions<AuthorizationContext> options) 
    : IdentityDbContext<ApplicationUser>(options);