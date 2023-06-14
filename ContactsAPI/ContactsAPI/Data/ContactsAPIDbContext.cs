using ContactsAPI.Models;
using Microsoft.EntityFrameworkCore;


    public class ContactsAPIDbContext : DbContext
    {
        public ContactsAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Contact> Contact { get; set; }
    public object Contacts { get; internal set; }
}
