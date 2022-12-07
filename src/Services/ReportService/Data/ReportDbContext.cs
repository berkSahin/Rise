using Microsoft.EntityFrameworkCore;
using ReportService.Entities.Report;

namespace ReportService.Data
{
    public class ReportDbContext : DbContext
    {
        #region Ctor

        public ReportDbContext(DbContextOptions options) : base(options)
        {
        }

        #endregion Ctor

        #region Properties

        public DbSet<Report> Reports { get; set; }
        //public DbSet<ContactInfo> ContactInfos { get; set; }
        //public DbSet<ContactInfoType> ContactInfoTypes { get; set; }

        #endregion Properties

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // If a tcpserver is deleted, set foreign key field in related Strings DB Table to null
        //    modelBuilder.Entity<ContactInfo>()
        //        .HasOne(x => x.Contact)
        //        .WithMany(x => x.ContactInfos)
        //        .HasForeignKey(x => x.ContactId);

        //}

        //modelBuilder.Entity<Book>()
        //.HasRequired<Author>(b => b.Author)
        //.WithMany(a => a.Books)
        //.HasForeignKey<int>(b => b.AuthorId);
    }
}