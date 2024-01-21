using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SignalRDemo.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalRDemo.DBContext
{
    public class NotificationDBContext : DbContext
    {
        public NotificationDBContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

        //    modelBuilder.Entity<UserGroup>()
        //.HasKey(ug => new { ug.RecipientId, ug.NotiSetting_Id });

        //    modelBuilder.Entity<UserGroup>()
        //        .HasOne(ug => ug.Recipient)
        //        .WithMany(r => r.UserGroups)
        //        .HasForeignKey(ug => ug.RecipientId);

        //    modelBuilder.Entity<UserGroup>()
        //        .HasOne(ug => ug.NotificationSetting)
        //        .WithMany(ns => ns.UserGroups)
        //        .HasForeignKey(ug => ug.NotiSetting_Id);

        //    modelBuilder.Entity<Recipient>()
        //        .HasKey(r => r.RecipientId);

        //    modelBuilder.Entity<NotificationSettings>()
        //        .HasKey(ns => ns.NotiSetting_Id);



        }

        public DbSet<Senders> Senders { get; set; }

        public DbSet<Recipients> Recipients { get; set; }

        public DbSet<NotificationSettings> NotificationSettings { get; set; }

        public DbSet<Notifications> Notifications { get; set; }

        public DbSet<UserGroups> UserGroups { get; set; }

        public DbSet<HubConnections> HubConnections { get; set; }

    }
}
