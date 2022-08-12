using Microsoft.EntityFrameworkCore;

namespace Ecommerce.CouponAPI.Model.Context
{
    public class MySqlContext : DbContext
    {
        public MySqlContext(){}
        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) {}

        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                Id = 1,
                CouponCode = "PEDRO_2022_10",
                DiscountAmount = 10
            });

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                Id = 2,
                CouponCode = "PEDRO_2022_15",
                DiscountAmount = 15
            });
        }
    }
}
