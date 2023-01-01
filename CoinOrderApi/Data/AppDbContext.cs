using CoinOrderApi.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CoinOrderApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CoinOrder>(entity =>
            {
                entity.HasOne(x => x.CommunicationPermission).WithOne(x => x.CoinOrder);
                entity.HasMany(x => x.SmsMessages).WithOne(x => x.CoinOrder);
                entity.HasMany(x => x.EmailMessages).WithOne(x => x.CoinOrder);
                entity.HasMany(x => x.PushNotificationMessages).WithOne(x => x.CoinOrder);
                entity.HasIndex(x => x.CreatedDate);
                entity.HasIndex(x => new { x.UserId, x.DeletedDate });
            });
        }

        public DbSet<MessageTemplate> MessageTemplate { get; set; }
        public DbSet<CommunicationPermission> CommunicationPermission { get; set; }
        public DbSet<SmsMessage> SmsMessage { get; set; }
        public DbSet<PushNotificationMessage> PushNotificationMessages { get; set; }
        public DbSet<EmailMessage> EmailMessage { get; set; }
    }
}
