using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Electronics.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AboutU> AboutUs { get; set; }
        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ContactU> ContactU { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderProduct> OrderProducts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Testimonial> Testimonials { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<Website> Websites { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseOracle("USER ID=TAH10_USER230;PASSWORD=you10;DATA SOURCE=94.56.229.181:3488/traindb");
            }
        }

        internal void Add(OrderProduct orderProduct, Order order)
        {
            throw new NotImplementedException();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TAH10_USER230")
                .HasAnnotation("Relational:Collation", "USING_NLS_COMP");

            modelBuilder.Entity<AboutU>(entity =>
            {
                entity.HasKey(e => e.AboutUsId)
                    .HasName("SYS_C00113090");

                entity.ToTable("ABOUT_US");

                entity.Property(e => e.AboutUsId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ABOUT_US_ID");

                entity.Property(e => e.ImagePath)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE_PATH");
                ////////////////////////////////////////////////////////////////////////////////////////////////////////
                entity.Property(e => e.Info)
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("INFO");

                entity.Property(e => e.WebId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("WEB_ID");

                entity.HasOne(d => d.Web)
                    .WithMany(p => p.AboutUs)
                    .HasForeignKey(d => d.WebId)
                    .HasConstraintName("WEB_ABOUT_US_FK");
            });

            modelBuilder.Entity<Card>(entity =>
            {
                entity.ToTable("CARD");

                entity.Property(e => e.CardId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CARD_ID");

                entity.Property(e => e.Balance)
                    .HasColumnType("FLOAT")
                    .HasColumnName("BALANCE");

                entity.Property(e => e.CardNumber)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CARD_NUMBER");

                entity.Property(e => e.Ccv)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CCV");

                entity.Property(e => e.Expdate)
                    .HasColumnType("DATE")
                    .HasColumnName("EXPDATE");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("CARD_USER_FK");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("CART");

                entity.Property(e => e.CartId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CART_ID");

                entity.Property(e => e.CardId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CARD_ID");

                entity.Property(e => e.OrderId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ORDER_ID");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.Card)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.CardId)
                    .HasConstraintName("CART_CARD_FK");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("CART_ORDER_FK");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("CART_USER_FK");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("CATEGORY_");

                entity.Property(e => e.CategoryId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CATEGORY_ID");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORY_NAME");
            });

            modelBuilder.Entity<ContactU>(entity =>
            {
                entity.HasKey(e => e.ContactUsId)
                    .HasName("SYS_C00113093");

                entity.ToTable("CONTACT_US");

                entity.Property(e => e.ContactUsId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CONTACT_US_ID");

                entity.Property(e => e.Fname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FNAME");

                entity.Property(e => e.Lname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LNAME");

                entity.Property(e => e.Message)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("MESSAGE");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PHONE");

                entity.Property(e => e.WebId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("WEB_ID");

                entity.HasOne(d => d.Web)
                    .WithMany(p => p.ContactUs)
                    .HasForeignKey(d => d.WebId)
                    .HasConstraintName("WEB_CONTACT_US_FK");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("ORDER_");

                entity.Property(e => e.OrderId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ORDER_ID");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("DATE")
                    .HasColumnName("ORDER_DATE");

                entity.Property(e => e.Quntity)
                    .HasColumnType("NUMBER")
                    .HasColumnName("QUNTITY");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.Property(e => e.TotalAmount)
                    .HasColumnType("FLOAT")
                    .HasColumnName("TOTAL_AMOUNT");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("USER_ORDER_FK");
            });

            modelBuilder.Entity<OrderProduct>(entity =>
            {
                entity.ToTable("ORDER_PRODUCT");

                entity.Property(e => e.OrderProductId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ORDER_PRODUCT_ID");

                entity.Property(e => e.OrderId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ORDER_ID");

                entity.Property(e => e.ProductId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PRODUCT_ID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("ORDER_ID_FK");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("PRODUCT_ID_FK");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("PRODUCT");

                entity.Property(e => e.ProductId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PRODUCT_ID");

                entity.Property(e => e.CategoryId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CATEGORY_ID");

                entity.Property(e => e.ImagePath)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE_PATH");

                entity.Property(e => e.IsAvailable)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("IS_AVAILABLE");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("PRODUCT_NAME");

                entity.Property(e => e.ProductPrice)
                    .HasColumnType("FLOAT")
                    .HasColumnName("PRODUCT_PRICE");

                entity.Property(e => e.ProductQuntity)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PRODUCT_QUNTITY");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("PRODUCT_CATEGORY_FK");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("REVIEW");

                entity.Property(e => e.ReviewId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("REVIEW_ID");

                entity.Property(e => e.ReviewValue)
                    .HasColumnType("NUMBER")
                    .HasColumnName("REVIEW_VALUE");

                entity.Property(e => e.WebId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("WEB_ID");

                entity.HasOne(d => d.Web)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.WebId)
                    .HasConstraintName("WEB_REVIEW_FK");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("ROLE_");

                entity.Property(e => e.RoleId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ROLE_ID");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ROLE_NAME");
            });

            modelBuilder.Entity<Testimonial>(entity =>
            {
                entity.HasKey(e => e.TestimonialsId)
                    .HasName("SYS_C00113096");

                entity.ToTable("TESTIMONIALS");

                entity.Property(e => e.TestimonialsId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("TESTIMONIALS_ID");

                entity.Property(e => e.Feedback)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("FEEDBACK_");

                entity.Property(e => e.Fname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FNAME");

                entity.Property(e => e.ImagePath)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE_PATH");

                entity.Property(e => e.Lname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LNAME");

                entity.Property(e => e.WebId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("WEB_ID");

                entity.HasOne(d => d.Web)
                    .WithMany(p => p.Testimonials)
                    .HasForeignKey(d => d.WebId)
                    .HasConstraintName("WEB_TESTIMONIAL_FK");
            });

            modelBuilder.Entity<UserLogin>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("SYS_C00113081");

                entity.ToTable("USER_LOGIN");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("USER_ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                

                entity.Property(e => e.Fname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FNAME");

                entity.Property(e => e.Lname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LNAME");

                entity.Property(e => e.RoleId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ROLE_ID");

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USER_EMAIL");

                entity.Property(e => e.UserImagepath)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("USER_IMAGEPATH");

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USER_PASSWORD");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserLogins)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("USER_ROLE_FK");

                entity.Property(e => e.Salary)
                    .HasColumnType("NUMBER")
                    .HasColumnName("SALARY");
            });

            modelBuilder.Entity<Website>(entity =>
            {
                entity.HasKey(e => e.WebId)
                    .HasName("SYS_C00113087");

                entity.ToTable("WEBSITE");

                entity.Property(e => e.WebId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("WEB_ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.CardId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CARD_ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PHONE");

                entity.Property(e => e.WebImagePackgroundPath)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("WEB_IMAGE_PACKGROUND_PATH");

                entity.Property(e => e.WebImagelogoPath)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("WEB_IMAGELOGO_PATH");

                entity.Property(e => e.WebName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("WEB_NAME");

                entity.HasOne(d => d.Card)
                    .WithMany(p => p.Websites)
                    .HasForeignKey(d => d.CardId)
                    .HasConstraintName("WEB_CARD_FK");
            });

            modelBuilder.HasSequence("D_ID_SEQ").IncrementsBy(3);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
