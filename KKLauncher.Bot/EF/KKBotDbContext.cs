using KKLauncher.DB.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace KKLauncher.Bot.EF
{
    public class KKBotDbContext : DbContext
    {
        public KKBotDbContext()
        {
        }

        public virtual DbSet<PCEntity> PCs { get; set; }

        public virtual DbSet<AppEntity> Applications { get; set; }

        public virtual DbSet<CollectionEntity> Collections { get; set; }

        public virtual DbSet<AppCollectionEntity> AppCollection { get; set; }

        public virtual DbSet<WhitelistEntity> Whitelists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = JObject.Parse(File.ReadAllText(Path.Combine("config", "kk-bot.json")));

            optionsBuilder.UseNpgsql(config["ConnectionString"].ToString());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<AppEntity>()
                .HasMany(ac => ac.Collections)
                .WithMany(ac => ac.Apps)
                .UsingEntity<AppCollectionEntity>(
                    c => c.HasOne<CollectionEntity>().WithMany().HasForeignKey(ac => ac.CollectionId),
                    a => a.HasOne<AppEntity>().WithMany().HasForeignKey(ac => ac.AppId));

            modelBuilder
                .Entity<CollectionEntity>()
                .HasMany(ac => ac.Apps)
                .WithMany(ac => ac.Collections)
                .UsingEntity<AppCollectionEntity>(
                    a => a.HasOne<AppEntity>().WithMany().HasForeignKey(ac => ac.AppId),
                    c => c.HasOne<CollectionEntity>().WithMany().HasForeignKey(ac => ac.CollectionId));
        }
    }
}
