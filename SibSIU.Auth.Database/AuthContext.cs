using Microsoft.EntityFrameworkCore;

using SibSIU.Auth.Database.Entities;
using SibSIU.Core.Database.EF.Contexts;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Auth.Database;
public sealed class AuthContext(DbContextOptions<AuthContext> options)
    : BaseTransactionContext(options)
{
    public DbSet<AcademicForm> AcademicForms { get; set; } = default!;
    public DbSet<AcademicGroup> AcademicGroups { get; set; } = default!;
    public DbSet<AcademicLevel> AcademicLevels { get; set; } = default!;
    public DbSet<AuthClaim> Claims { get; set; } = default!;
    public DbSet<AuthClaimType> ClaimTypes { get; set; } = default!;
    public DbSet<DepartmentUnit> DepartmentUnits { get; set; } = default!;
    public DbSet<DirectionOfTraining> DirectionOfTraining { get; set; } = default!;
    public DbSet<Gender> Genders { get; set; } = default!;
    public DbSet<InstituteUnit> InstituteUnits { get; set; } = default!;
    public DbSet<Organization> Organizations { get; set; } = default!;
    public DbSet<Partner> Partners { get; set; } = default!;
    public DbSet<Post> Posts { get; set; } = default!;
    public DbSet<Pupil> Pupils { get; set; } = default!;
    public DbSet<Role> Roles { get; set; } = default!;
    public DbSet<School> Schools { get; set; } = default!;
    public DbSet<Scope> Scopes {  get; set; } = default!; 
    public DbSet<Student> Students { get; set; } = default!;
    public DbSet<TempStudentDeanCodeFromDeanDB> TempStudents { get; set; } = default!;
    public DbSet<Unit> Units { get; set; } = default!;
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<UserRole> UserRoles { get; set; } = default!;
    public DbSet<WorkPlaces> WorkPlaces { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AuthContext).Assembly);
        builder.Seed();
    }
}
