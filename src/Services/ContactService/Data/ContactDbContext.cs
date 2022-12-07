using ContactService.Entities.Contact;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Data
{
    public class ContactDbContext : DbContext
    {
        #region Ctor

        public ContactDbContext(DbContextOptions options) : base(options)
        {
        }

        #endregion Ctor

        #region Properties

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<ContactInfoType> ContactInfoTypes { get; set; }

        #endregion Properties

        #region Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        #endregion Methods
    }
}