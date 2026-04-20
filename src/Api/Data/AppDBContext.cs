using CESIZen.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace CESIZen.Shared.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MoodPrimary> MoodPrimaries { get; set; }
        public DbSet<MoodDetail> MoodDetails { get; set; }
        public DbSet<TrackerLog> TrackerLogs { get; set; }
        public DbSet<PageStock> PageStocks { get; set; }
        public DbSet<Menu> Menus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User configuration
            modelBuilder.Entity<User>()
                .HasKey(u => u.id_user);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.email)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.id_role)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>()
                .HasOne(u => u.Profile)
                .WithOne(p => p.User)
                .HasForeignKey<Profile>(p => p.id_user)
                .OnDelete(DeleteBehavior.Cascade);

            // Profile configuration
            modelBuilder.Entity<Profile>()
                .HasKey(p => p.id_profile);
            modelBuilder.Entity<Profile>()
                .HasIndex(p => p.id_user)
                .IsUnique();

            // Role configuration
            modelBuilder.Entity<Role>()
                .HasKey(r => r.id_role);

            // Session configuration
            modelBuilder.Entity<Session>()
                .HasKey(s => s.id_session);
            modelBuilder.Entity<Session>()
                .HasOne(s => s.User)
                .WithMany(u => u.Sessions)
                .HasForeignKey(s => s.id_user)
                .OnDelete(DeleteBehavior.Cascade);

            // Article configuration
            modelBuilder.Entity<Article>()
                .HasKey(a => a.id_article);
            modelBuilder.Entity<Article>()
                .HasOne(a => a.User)
                .WithMany(u => u.Articles)
                .HasForeignKey(a => a.id_user)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Article>()
                .HasOne(a => a.Category)
                .WithMany(c => c.Articles)
                .HasForeignKey(a => a.id_category)
                .OnDelete(DeleteBehavior.Restrict);

            // Category configuration
            modelBuilder.Entity<Category>()
                .HasKey(c => c.id_category);

            // MoodPrimary configuration
            modelBuilder.Entity<MoodPrimary>()
                .HasKey(m => m.id_mood_primary);
            modelBuilder.Entity<MoodPrimary>()
                .HasMany(m => m.MoodDetails)
                .WithOne(md => md.MoodPrimary)
                .HasForeignKey(md => md.id_mood_primary)
                .OnDelete(DeleteBehavior.Cascade);

            // MoodDetail configuration
            modelBuilder.Entity<MoodDetail>()
                .HasKey(md => md.id_mood_detail);
            modelBuilder.Entity<MoodDetail>()
                .HasMany(md => md.TrackerLogs)
                .WithOne(tl => tl.MoodDetail)
                .HasForeignKey(tl => tl.id_mood_detail)
                .OnDelete(DeleteBehavior.Restrict);

            // TrackerLog configuration
            modelBuilder.Entity<TrackerLog>()
                .HasKey(tl => tl.id_tracker_log);
            modelBuilder.Entity<TrackerLog>()
                .HasOne(tl => tl.Profile)
                .WithMany(p => p.TrackerLogs)
                .HasForeignKey(tl => tl.id_profile)
                .OnDelete(DeleteBehavior.Cascade);



            #region MODEL AND MENU

            // PageStock configuration
            modelBuilder.Entity<PageStock>()
                .HasKey(ps => ps.id_page_stock);
            modelBuilder.Entity<PageStock>()
                .HasMany(ps => ps.Menus)
                .WithOne(m => m.PageStock)
                .HasForeignKey(m => m.id_page_stock)
                .OnDelete(DeleteBehavior.Cascade);

            // Menu configuration
            modelBuilder.Entity<Menu>()
                .HasKey(m => m.id_menu);
            modelBuilder.Entity<Menu>()
                .HasIndex(m => m.label)
                .IsUnique();
            #endregion
        }
    }
}
