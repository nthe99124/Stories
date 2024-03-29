﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StoriesProject.Model.BaseEntity;
using System.ComponentModel.DataAnnotations;

namespace StoriesProject.API.Common.Repository;

public partial class StoriesContext : DbContext
{
    private IConfiguration _configuration;

    public StoriesContext(DbContextOptions<StoriesContext> options)
        : base(options)
    {

    }

    public virtual DbSet<Accountant> Accountants { get; set; }
    public virtual DbSet<AuthorRegister> AuthorRegister { get; set; }
    public virtual DbSet<CoinLog> CoinLogs { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<OrderDetail> OrderDetails { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<RoleAccountant> RoleAccountants { get; set; }
    public virtual DbSet<Story> Stories { get; set; }
    public virtual DbSet<Topic> Topics { get; set; }
    public virtual DbSet<TopicStory> TopicStories { get; set; }
    public virtual DbSet<FavoriteStories> FavoriteStories { get; set; }
    public virtual DbSet<Chapter> Chapters { get; set; }
    public virtual DbSet<ViewedChapterStories> ViewedChapterStories { get; set; }

    #region Các model hứng dữ liệu
    public virtual DbSet<StoryAccountGeneric> StoryAccountGeneric { get; set; }
    public virtual DbSet<StoryInforAdmin> StoryInforAdmin { get; set; }
    
    #endregion

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Ignore<StoryAccountGeneric>();
        modelBuilder.Entity<Accountant>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Accountant)
                .HasForeignKey<Accountant>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Accountants_CoinLog");

            entity.HasOne(d => d.AuthorRegister).WithOne(p => p.Accountant)
                 .HasForeignKey<AuthorRegister>(d => d.AccountantId)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_Accountants_AuthorRegister");
        });

        modelBuilder.Entity<AuthorRegister>(entity =>
        {
            entity.Property(e => e.AuthorRegisterId).ValueGeneratedNever();
            entity.Property(e => e.AccountantType).HasDefaultValue(1);
            entity.Property(e => e.PaymentType).HasDefaultValue(1);
            entity.Property(e => e.BankName).HasMaxLength(255);
            entity.Property(e => e.BankNumber).HasMaxLength(50);
            entity.Property(e => e.BankAccount).HasMaxLength(50);
        });

        modelBuilder.Entity<CoinLog>(entity =>
        {
            entity.ToTable("CoinLog");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.MoneyConvert).HasColumnType("money");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Discounts).HasColumnType("money");
            entity.Property(e => e.Taxes).HasColumnType("money");
            entity.Property(e => e.TotalAmount).HasColumnType("money");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_Orders_Accountants");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Price).HasColumnType("money");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetails_Orders");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<RoleAccountant>(entity =>
        {
            entity.ToTable("RoleAccountant");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Account).WithMany(p => p.RoleAccountantAccounts)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoleAccountant_Accountants");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.RoleAccountantCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_RoleAccountant_Accountants1");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleAccountants)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoleAccountant_Roles");
        });

        modelBuilder.Entity<Story>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.RateScore).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.StoryCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_Stories_Accountants");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.StoryModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_Stories_Accountants1");
        });

        modelBuilder.Entity<Topic>(entity =>
        {
            entity.ToTable("Topic");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<TopicStory>(entity =>
        {
            entity.ToTable("TopicStory");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Story).WithMany(p => p.TopicStories)
                .HasForeignKey(d => d.StoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TopicStory_Stories");

            entity.HasOne(d => d.Topic).WithMany(p => p.TopicStories)
                .HasForeignKey(d => d.TopicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TopicStory_Topic");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    #region override custom savechanges
    public override int SaveChanges()
    {
        var validModel = ValidateModel();
        if (!validModel.IsValid && !string.IsNullOrEmpty(validModel.ErrorMessage))
        {
            // Ném ra một exception với thông điệp lỗi
            throw new InvalidOperationException(validModel.ErrorMessage);
        }
        TrimStringPropertype();
        return base.SaveChanges();
    }

    /// <summary>
    /// Xử lý trim dữ liệu trước khi lưu
    /// CreatedBy ntthe 25.02.2024
    /// </summary>
    private void TrimStringPropertype()
    {
        var entities = ChangeTracker.Entries()
                        .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);
        foreach (var item in entities)
        {
            var properties = item.Properties.Where(p => p.CurrentValue is string).Select(p => p);
            foreach (var property in properties)
            {
                var currentValue = property.CurrentValue?.ToString();
                if (currentValue != null)
                {
                    property.CurrentValue = currentValue.Trim();
                }
            }
        }
    }

    /// <summary>
    /// Kiểm tra dữ liệu null có phù hợp kiểu dữ liệu không trước khi lưu (chỉ xảy ra với string)
    /// CreatedBy ntthe 25.02.2024
    /// </summary>
    public (bool IsValid, string ErrorMessage) ValidateModel()
    {
        var entities = ChangeTracker.Entries()
                        .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).Select(e => e.Entity);
        var validationContext = new ValidationContext(entities);
        var validationResults = new List<ValidationResult>();

        // Kiểm tra tính hợp lệ của model
        bool isValid = Validator.TryValidateObject(entities, validationContext, validationResults, true);
        if (!isValid)
        {
            // Lặp qua các lỗi và tạo thông điệp lỗi
            string errorMessage = string.Join(Environment.NewLine, validationResults.Select(r => r.ErrorMessage));
            return (false, errorMessage);
        }

        return (true, "");
    }
    #endregion

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
