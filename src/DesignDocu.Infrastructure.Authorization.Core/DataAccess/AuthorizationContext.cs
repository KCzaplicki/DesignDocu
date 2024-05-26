using DesignDocu.Infrastructure.Authorization.Abstractions.DataAccess;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DesignDocu.Infrastructure.Auth.Authorization.Core.DataAccess;

internal class AuthorizationContext(DbContextOptions<AuthorizationContext> options) : IdentityDbContext<ApplicationUser>(options);