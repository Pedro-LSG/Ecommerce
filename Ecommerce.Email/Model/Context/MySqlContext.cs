using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Email.Model.Context
{
    public class MySqlContext : DbContext
    {
        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) { }

        public DbSet<EmailLog> Emails { get; set; }
    }
}
