using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Mobile_DAO
{
    public class CLS_Mobile
    {
        public static int AddMobile(mobile_details mob)
        {
            using (var ctx = new MobileStoreEntities())
            {
                ctx.mobile_details.Add(mob);
                ctx.SaveChanges();
                return mob.mobileId;
            }
        }
        public static List<mobile_details> ViewMobile()
        {
            
            using (var ctx = new MobileStoreEntities())
            {
                
                var mob = ctx.mobile_details.Include(e => e.user_details).ToList();
                return mob;
            }
        }

        public static List<mobile_details> ViewMobileById(int id)
        {

            using (var ctx = new MobileStoreEntities())
            {

                var mob = ctx.mobile_details.Include(e => e.user_details).Where(f => f.user_id == id).ToList();
                return mob;  //linq
            }
        }

        public static List<user_details> ViewUser()
        {
            using (var ctx = new MobileStoreEntities())
            {
                var usr = ctx.user_details.ToList();
                return usr;
            }
        }

        public static List<user_details> ViewSeller()
        {
            using (var ctx = new MobileStoreEntities())
            {
                return ctx.user_details.Where(user_details=>user_details.user_type.Equals("seller")).ToList();
            }
        }

        public static bool UpdateMobile(int mobileId,string mobileName,string description,int price,int stock_quantity)
        {

            using (var ctx = new MobileStoreEntities())
            {
                var mob = ctx.mobile_details.Where(a => a.mobileId== mobileId).SingleOrDefault();
                mob.mobileName = mobileName;
                mob.description = description;
                mob.price= Convert.ToInt32(price);
                mob.stock_quantity=Convert.ToInt32(stock_quantity);
                ctx.Entry(mob).State = EntityState.Modified;
                ctx.SaveChanges();
                return true;
            }
        }

        public static bool UpdateQuantity(int mobileId, int stock_quantity)
        {

            using (var ctx = new MobileStoreEntities())
            {
                var mob = ctx.mobile_details.Where(a => a.mobileId == mobileId).SingleOrDefault();
                mob.stock_quantity = stock_quantity - 1;
                ctx.Entry(mob).State = EntityState.Modified;
                ctx.SaveChanges();
                return true;
            }
        }

        public static bool DeleteMobile(int mobileId)
        {

            using (var ctx = new MobileStoreEntities())
            {
                var mob = ctx.mobile_details.Where(a => a.mobileId.Equals(mobileId)).SingleOrDefault();
                ctx.mobile_details.Remove(mob);
                ctx.SaveChanges();
                return true;
            }

        }

        public static mobile_details FetchMobile(int? mobileId)
        {
            using (var ctx = new MobileStoreEntities())
            {
                var mob = ctx.mobile_details.Include(e => e.user_details).Where(a => a.mobileId == mobileId).SingleOrDefault();
                return mob;
            }
        }

    }

    public class MobileConfiguration : EntityTypeConfiguration<mobile_details>
    {
        public MobileConfiguration()
        {
            // Primary Key
            this.HasKey(e => e.mobileId);

            // Properties
            this.Property(e => e.user_id)
                .IsRequired();

            this.Property(e => e.mobileName)
                .IsOptional()
                .HasMaxLength(50);

            this.Property(e => e.description)
                .IsOptional()
                .HasMaxLength(50);

            this.Property(e => e.price)
                .IsRequired();

            this.Property(e => e.stock_quantity)
                .IsRequired();

            this.Property(e => e.image_name)
                .IsOptional()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("mobile_details"); // Name of your table in the database
            this.Property(e => e.mobileId).HasColumnName("mobileId");
            this.Property(e => e.user_id).HasColumnName("user_id");
            this.Property(e => e.mobileName).HasColumnName("mobileName");
            this.Property(e => e.description).HasColumnName("description");
            this.Property(e => e.price).HasColumnName("price");
            this.Property(e => e.stock_quantity).HasColumnName("stock_quantity");
            this.Property(e => e.image_name).HasColumnName("image_name");

        }

    }
}
