﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StoriesProject.API.Common.Repository;

#nullable disable

namespace StoriesProject.API.Migrations
{
    [DbContext(typeof(StoriesContext))]
    [Migration("20240310032002_AuthorRegister")]
    partial class AuthorRegister
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StoriesProject.Model.BaseEntity.Accountant", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("Coin")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<short>("Gender")
                        .HasColumnType("smallint");

                    b.Property<string>("ImgAvatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Introduce")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("bit");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Accountants");
                });

            modelBuilder.Entity("StoriesProject.Model.BaseEntity.AuthorRegister", b =>
                {
                    b.Property<Guid>("AuthorRegisterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("AuthorRegisterId");

                    b.HasIndex("AccountantId")
                        .IsUnique();

                    b.ToTable("AuthorRegister");
                });

            modelBuilder.Entity("StoriesProject.Model.BaseEntity.Chapter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedByNavigationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByNavigationId");

                    b.HasIndex("StoryId");

                    b.ToTable("Chapters");
                });

            modelBuilder.Entity("StoriesProject.Model.BaseEntity.CoinLog", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CoinTransacted")
                        .HasColumnType("int");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<decimal>("MoneyConvert")
                        .HasColumnType("money");

                    b.Property<short>("Type")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.ToTable("CoinLog", (string)null);
                });

            modelBuilder.Entity("StoriesProject.Model.BaseEntity.FavoriteStories", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("StoryId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AccountantId");

                    b.HasIndex("StoryId");

                    b.ToTable("FavoriteStories");
                });

            modelBuilder.Entity("StoriesProject.Model.BaseEntity.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CoinTransacted")
                        .HasColumnType("int");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Discounts")
                        .HasColumnType("money");

                    b.Property<short>("PaymentMethod")
                        .HasColumnType("smallint");

                    b.Property<short>("Status")
                        .HasColumnType("smallint");

                    b.Property<decimal>("Taxes")
                        .HasColumnType("money");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("StoriesProject.Model.BaseEntity.OrderDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<string>("ProductDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("StoriesProject.Model.BaseEntity.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RoleDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("StoriesProject.Model.BaseEntity.RoleAccountant", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleAccountant", (string)null);
                });

            modelBuilder.Entity("StoriesProject.Model.BaseEntity.Story", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<decimal>("RateScore")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<short>("TypeOfStory")
                        .HasColumnType("smallint");

                    b.Property<string>("VideoLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ViewAccess")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("ModifiedBy");

                    b.ToTable("Stories");
                });

            modelBuilder.Entity("StoriesProject.Model.BaseEntity.StoryAccountGeneric", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<short>("TypeOfStory")
                        .HasColumnType("smallint");

                    b.Property<string>("VideoLink")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StoryAccountGeneric");
                });

            modelBuilder.Entity("StoriesProject.Model.BaseEntity.StoryAccoutant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccoutantID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AccoutantIDNavigationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsReaded")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("StoryID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("StoryIDNavigationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AccoutantIDNavigationId");

                    b.HasIndex("StoryIDNavigationId");

                    b.ToTable("StoryAccoutant");
                });

            modelBuilder.Entity("StoriesProject.Model.BaseEntity.Topic", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Topic", (string)null);
                });

            modelBuilder.Entity("StoriesProject.Model.BaseEntity.TopicStory", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TopicId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StoryId");

                    b.HasIndex("TopicId");

                    b.ToTable("TopicStory", (string)null);
                });

            modelBuilder.Entity("StoriesProject.Model.BaseEntity.ViewedChapterStories", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ChapterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("IsLastest")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AccountantId");

                    b.HasIndex("ChapterId");

                    b.ToTable("ViewedChapterStories");
                });

            modelBuilder.Entity("StoriesProject.Model.BaseEntity.Accountant", b =>
                {
                    b.HasOne("StoriesProject.Model.BaseEntity.CoinLog", "IdNavigation")
                        .WithOne("Accountant")
                        .HasForeignKey("StoriesProject.Model.BaseEntity.Accountant", "Id")
                        .IsRequired()
                        .HasConstraintName("FK_Accountants_CoinLog");

                    b.Navigation("IdNavigation");
                });

            modelBuilder.Entity("StoriesProject.Model.BaseEntity.AuthorRegister", b =>
                {
                    b.HasOne("StoriesProject.Model.BaseEntity.Accountant", "Accountant")
                        .WithOne("AuthorRegister")
                        .HasForeignKey("StoriesProject.Model.BaseEntity.AuthorRegister", "AccountantId")
                        .IsRequired()
                        .HasConstraintName("FK_Accountants_AuthorRegister");

                    b.Navigation("Accountant");
                });

            modelBuilder.Entity("StoriesProject.Model.BaseEntity.Chapter", b =>
                {
                    b.HasOne("StoriesProject.Model.BaseEntity.Accountant", "CreatedByNavigation")
                        .WithMany()
                        .HasForeignKey("CreatedByNavigationId");

                    b.HasOne("StoriesProject.Model.BaseEntity.Story", "StoryIdNavigation")
                        .WithMany()
                        .HasForeignKey("StoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedByNavigation");

                    b.Navigation("StoryIdNavigation");
                });

            modelBuilder.Entity("StoriesProject.Model.BaseEntity.FavoriteStories", b =>
                {
                    b.HasOne("StoriesProject.Model.BaseEntity.Accountant", "AccountantIdNavigation")
                        .WithMany()
                        .HasForeignKey("AccountantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StoriesProject.Model.BaseEntity.Story", "StoryIdNavigation")
                        .WithMany()
                        .HasForeignKey("StoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccountantIdNavigation");

                    b.Navigation("StoryIdNavigation");
                });

            modelBuilder.Entity("StoriesProject.Model.BaseEntity.Order", b =>
                {
                    b.HasOne("StoriesProject.Model.BaseEntity.Accountant", "CreatedByNavigation")
                        .WithMany("Orders")
                        .HasForeignKey("CreatedBy")
                        .HasConstraintName("FK_Orders_Accountants");

                    b.Navigation("CreatedByNavigation");
                });

            modelBuilder.Entity("StoriesProject.Model.BaseEntity.OrderDetail", b =>
                {
                    b.HasOne("StoriesProject.Model.BaseEntity.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .IsRequired()
                        .HasConstraintName("FK_OrderDetails_Orders");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("StoriesProject.Model.BaseEntity.RoleAccountant", b =>
                {
                    b.HasOne("StoriesProject.Model.BaseEntity.Accountant", "Account")
                        .WithMany("RoleAccountantAccounts")
                        .HasForeignKey("AccountId")
                        .IsRequired()
                        .HasConstraintName("FK_RoleAccountant_Accountants");

                    b.HasOne("StoriesProject.Model.BaseEntity.Accountant", "CreatedByNavigation")
                        .WithMany("RoleAccountantCreatedByNavigations")
                        .HasForeignKey("CreatedBy")
                        .HasConstraintName("FK_RoleAccountant_Accountants1");

                    b.HasOne("StoriesProject.Model.BaseEntity.Role", "Role")
                        .WithMany("RoleAccountants")
                        .HasForeignKey("RoleId")
                        .IsRequired()
                        .HasConstraintName("FK_RoleAccountant_Roles");

                    b.Navigation("Account");

                    b.Navigation("CreatedByNavigation");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("StoriesProject.Model.BaseEntity.Story", b =>
                {
                    b.HasOne("StoriesProject.Model.BaseEntity.Accountant", "CreatedByNavigation")
                        .WithMany("StoryCreatedByNavigations")
                        .HasForeignKey("CreatedBy")
                        .HasConstraintName("FK_Stories_Accountants");

                    b.HasOne("StoriesProject.Model.BaseEntity.Accountant", "ModifiedByNavigation")
                        .WithMany("StoryModifiedByNavigations")
                        .HasForeignKey("ModifiedBy")
                        .HasConstraintName("FK_Stories_Accountants1");

                    b.Navigation("CreatedByNavigation");

                    b.Navigation("ModifiedByNavigation");
                });

            modelBuilder.Entity("StoriesProject.Model.BaseEntity.StoryAccoutant", b =>
                {
                    b.HasOne("StoriesProject.Model.BaseEntity.Accountant", "AccoutantIDNavigation")
                        .WithMany("AccoutantIDByNavigations")
                        .HasForeignKey("AccoutantIDNavigationId");

                    b.HasOne("StoriesProject.Model.BaseEntity.Story", "StoryIDNavigation")
                        .WithMany("StoryIDByNavigations")
                        .HasForeignKey("StoryIDNavigationId");

                    b.Navigation("AccoutantIDNavigation");

                    b.Navigation("StoryIDNavigation");
                });

            modelBuilder.Entity("StoriesProject.Model.BaseEntity.TopicStory", b =>
                {
                    b.HasOne("StoriesProject.Model.BaseEntity.Story", "Story")
                        .WithMany("TopicStories")
                        .HasForeignKey("StoryId")
                        .IsRequired()
                        .HasConstraintName("FK_TopicStory_Stories");

                    b.HasOne("StoriesProject.Model.BaseEntity.Topic", "Topic")
                        .WithMany("TopicStories")
                        .HasForeignKey("TopicId")
                        .IsRequired()
                        .HasConstraintName("FK_TopicStory_Topic");

                    b.Navigation("Story");

                    b.Navigation("Topic");
                });

            modelBuilder.Entity("StoriesProject.Model.BaseEntity.ViewedChapterStories", b =>
                {
                    b.HasOne("StoriesProject.Model.BaseEntity.Accountant", "AccountantIdNavigation")
                        .WithMany()
                        .HasForeignKey("AccountantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StoriesProject.Model.BaseEntity.Chapter", "ChapterIdNavigation")
                        .WithMany()
                        .HasForeignKey("ChapterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccountantIdNavigation");

                    b.Navigation("ChapterIdNavigation");
                });

            modelBuilder.Entity("StoriesProject.Model.BaseEntity.Accountant", b =>
                {
                    b.Navigation("AccoutantIDByNavigations");

                    b.Navigation("AuthorRegister")
                        .IsRequired();

                    b.Navigation("Orders");

                    b.Navigation("RoleAccountantAccounts");

                    b.Navigation("RoleAccountantCreatedByNavigations");

                    b.Navigation("StoryCreatedByNavigations");

                    b.Navigation("StoryModifiedByNavigations");
                });

            modelBuilder.Entity("StoriesProject.Model.BaseEntity.CoinLog", b =>
                {
                    b.Navigation("Accountant");
                });

            modelBuilder.Entity("StoriesProject.Model.BaseEntity.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("StoriesProject.Model.BaseEntity.Role", b =>
                {
                    b.Navigation("RoleAccountants");
                });

            modelBuilder.Entity("StoriesProject.Model.BaseEntity.Story", b =>
                {
                    b.Navigation("StoryIDByNavigations");

                    b.Navigation("TopicStories");
                });

            modelBuilder.Entity("StoriesProject.Model.BaseEntity.Topic", b =>
                {
                    b.Navigation("TopicStories");
                });
#pragma warning restore 612, 618
        }
    }
}
