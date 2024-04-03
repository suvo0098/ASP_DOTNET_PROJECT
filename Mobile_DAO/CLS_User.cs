using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_DAO
{
    public class CLS_User
    {
        public static int AddUser(user_details usr)
        {
            using (var ctx = new MobileStoreEntities())
            {
                ctx.user_details.Add(usr);
                ctx.SaveChanges();
                return usr.user_id;
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


        public static bool UpdateUser(int user_id,string user_name,string password,string user_type,string first_name,string last_name,string contactNumber,string email)
        {

            using (var ctx = new MobileStoreEntities())
            {
                var usr = ctx.user_details.Where(a => a.user_id == user_id).SingleOrDefault();
                usr.user_name = user_name;
                usr.password = password;
                usr.user_type = user_type;
                usr.first_name = first_name;
                usr.last_name = last_name;
                usr.contactNumber = contactNumber;
                usr.email = email;
                ctx.Entry(usr).State = EntityState.Modified;
                ctx.SaveChanges();
                return true;
            }
        }

        public static bool AuthenticateUser(string userName, string password)
        {
            using (var ctx = new MobileStoreEntities())
            {
                var usr = ctx.user_details.Where(a => a.user_name.Equals(userName)).SingleOrDefault();
                if(usr!= null && usr.password.Equals(password))
                {
                    return true;
                }
                return false;
            }
        }

        public static bool DeleteUser(int user_id)
        {

            using (var ctx = new MobileStoreEntities())
            {
                var usr = ctx.user_details.Where(a => a.user_id.Equals(user_id)).SingleOrDefault();
                ctx.user_details.Remove(usr);
                ctx.SaveChanges();
                return true;
            }

        }

        public static user_details FetchUser(int? user_id)
        {
            using (var ctx = new MobileStoreEntities())
            {
                var usr= ctx.user_details.Where(a => a.user_id == user_id).SingleOrDefault();
                return usr;
            }
        }

        public static user_details FetchUserByName(string name)
        {
            using (var ctx = new MobileStoreEntities())
            {
                var usr = ctx.user_details.Where(a => a.user_name == name).SingleOrDefault();
                return usr;
            }
        }
    }

    public class UserConfiguration : EntityTypeConfiguration<user_details>
    {
        public UserConfiguration()
        {
            // Primary Key
            this.HasKey(e => e.user_id);

            // Properties
            this.Property(e => e.user_name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(e => e.password)
                .IsOptional()
                .HasMaxLength(50);

            this.Property(e => e.user_type)
                .IsOptional()
                .HasMaxLength(50);

            this.Property(e => e.first_name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(e => e.last_name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(e => e.contactNumber)
                .IsOptional()
                .HasMaxLength(50);

            this.Property(e => e.email)
                .IsOptional()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("user_details"); // Name of your table in the database
            this.Property(e => e.user_id).HasColumnName("user_id");
            this.Property(e => e.user_name).HasColumnName("user_name");
            this.Property(e => e.password).HasColumnName("password");
            this.Property(e => e.user_type).HasColumnName("user_type");
            this.Property(e => e.first_name).HasColumnName("first_name");
            this.Property(e => e.last_name).HasColumnName("last_name");
            this.Property(e => e.contactNumber).HasColumnName("contactNumber");
            this.Property(e => e.email).HasColumnName("email");

        }

    }
}
