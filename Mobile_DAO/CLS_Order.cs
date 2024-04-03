using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;


namespace Mobile_DAO
{
    public class CLS_Order
    {
        public static int AddOrder(order_details ord)
        {
            using (var ctx = new MobileStoreEntities())
            {
                ctx.order_details.Add(ord);
                ctx.SaveChanges();
                return ord.orderId;
            }
        }
        public static List<order_details> ViewOrder()
        {
            using (var ctx = new MobileStoreEntities())
            {
                var ord = ctx.order_details.Include(e => e.user_details).Include(f => f.mobile_details).ToList();
                return ord;
            }
        }

        public static List<user_details> ViewCustomer()
        {
            using (var ctx = new MobileStoreEntities())
            {
                return ctx.user_details.Where(user => user.user_type.Equals("customer")).ToList();
            }
        }

        public static List<mobile_details> ViewProduct()
        {
            using (var ctx = new MobileStoreEntities())
            {
                var ord = ctx.mobile_details.ToList();
                return ord;
            }
        }

        public static bool DeleteOrder(int order_id)
        {

            using (var ctx = new MobileStoreEntities())
            {
                var ord = ctx.order_details.Where(a => a.orderId.Equals(order_id)).SingleOrDefault();
                ctx.order_details.Remove(ord);
                ctx.SaveChanges();
                return true;
            }

        }

        public static order_details FetchOrder(int? order_id)
        {
            using (var ctx = new MobileStoreEntities())
            {

                var prod = ctx.order_details.Include(e => e.user_details).Include(f => f.mobile_details).Where(a => a.orderId == order_id).SingleOrDefault();
                return prod;
            }
        }

    }

    public class orderConfiguration : EntityTypeConfiguration<order_details>
    {
        public orderConfiguration()
        {
            // Primary Key
            this.HasKey(e => e.orderId);

            // Properties
            this.Property(e => e.user_id)
                .IsRequired();

            this.Property(e => e.mobileId)
                .IsOptional();

            this.Property(e => e.quantity)
                .IsOptional();

            this.Property(e => e.total)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("order_details"); // Name of your table in the database
            this.Property(e => e.orderId).HasColumnName("orderId");
            this.Property(e => e.user_id).HasColumnName("user_id");
            this.Property(e => e.mobileId).HasColumnName("mobileId");
            this.Property(e => e.quantity).HasColumnName("quantity");
            this.Property(e => e.total).HasColumnName("total");
        }

    }


}
