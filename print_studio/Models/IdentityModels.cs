using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using print_studio.Models;

namespace print_studio.Models
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PrintOrder> PrintOrders { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            PrintOrders = new HashSet<PrintOrder>();
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public virtual DbSet<ColorsProduct> ColorsProducts { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<OrderStatu> OrderStatus { get; set; }
        public virtual DbSet<PrintOrder> PrintOrders { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<SavedProduct> SavedProducts { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ColorsProduct>()
               .Property(e => e.color)
               .IsUnicode(false);

            modelBuilder.Entity<ColorsProduct>()
                .HasMany(e => e.SavedProducts)
                .WithOptional(e => e.ColorsProduct)
                .HasForeignKey(e => e.id_colorsproduct)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.PrintOrders)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.id_employee);


            modelBuilder.Entity<PrintOrder>()
                .Property(e => e.id_employee)
                .IsUnicode(false);

            modelBuilder.Entity<Image>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Image>()
                .HasMany(e => e.ColorsProducts)
                .WithOptional(e => e.Image)
                .HasForeignKey(e => e.id_image)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Image>()
                .HasMany(e => e.SavedProducts)
                .WithOptional(e => e.Image)
                .HasForeignKey(e => e.id_image);

            modelBuilder.Entity<OrderStatu>()
                .HasMany(e => e.PrintOrders)
                .WithOptional(e => e.OrderStatu)
                .HasForeignKey(e => e.id_status);

            modelBuilder.Entity<OrderStatu>()
               .Property(e => e.name)
               .IsUnicode(false);

            modelBuilder.Entity<PrintOrder>()
                .Property(e => e.surname)
                .IsUnicode(false);

            modelBuilder.Entity<PrintOrder>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<PrintOrder>()
                .Property(e => e.adress)
                .IsUnicode(false);

            modelBuilder.Entity<PrintOrder>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<ProductType>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<ProductType>()
                .Property(e => e.material)
                .IsUnicode(false);

            modelBuilder.Entity<ProductType>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<ProductType>()
                .Property(e => e.days)
                .IsUnicode(false);

            modelBuilder.Entity<ProductType>()
                .HasMany(e => e.ColorsProducts)
                .WithOptional(e => e.ProductType)
                .HasForeignKey(e => e.id_producttype)
                .WillCascadeOnDelete();

            modelBuilder.Entity<SavedProduct>()
                .HasMany(e => e.PrintOrders)
                .WithOptional(e => e.SavedProduct)
                .HasForeignKey(e => e.id_savedproduct)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Size>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Size>()
                .HasMany(e => e.PrintOrders)
                .WithOptional(e => e.Size)
                .HasForeignKey(e => e.id_size);
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}