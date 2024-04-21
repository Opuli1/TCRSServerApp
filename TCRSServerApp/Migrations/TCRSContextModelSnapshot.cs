﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TCRSServerApp.Data;

#nullable disable

namespace TCRSServerApp.Migrations
{
    [DbContext(typeof(TCRSContext))]
    partial class TCRSContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TCRSServerApp.Data.BannerSettings", b =>
                {
                    b.Property<int>("BannerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BannerId"));

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Visible")
                        .HasColumnType("bit");

                    b.HasKey("BannerId");

                    b.ToTable("BannerSettings");

                    b.HasData(
                        new
                        {
                            BannerId = 1,
                            Message = "*Important Message Goes Here*",
                            Visible = false
                        });
                });

            modelBuilder.Entity("TCRSServerApp.Data.Entities.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            Name = "Uncategorized",
                            Slug = "uncategorized"
                        },
                        new
                        {
                            CategoryId = 2,
                            Name = "FAQ",
                            Slug = "faq"
                        },
                        new
                        {
                            CategoryId = 3,
                            Name = "Documentation",
                            Slug = "documentation"
                        });
                });

            modelBuilder.Entity("TCRSServerApp.Data.Entities.ContentPost", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PostId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EditedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("PublishedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PostId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("ContentPosts");
                });

            modelBuilder.Entity("TCRSServerApp.Data.Entities.FileMetaData", b =>
                {
                    b.Property<int>("FileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FileId"));

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.HasKey("FileId");

                    b.HasIndex("PostId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("TCRSServerApp.Data.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EditedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnType("varchar(25)");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnType("varchar(25)");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            CreatedOn = new DateTime(2024, 4, 20, 19, 16, 19, 753, DateTimeKind.Local).AddTicks(5077),
                            Email = "john.doe@gmail.com",
                            FirstName = "John",
                            Hash = "iewfbukrficruyewreob",
                            LastName = "Doe",
                            Salt = "iergberihb"
                        });
                });

            modelBuilder.Entity("TCRSServerApp.Data.Entities.ContentPost", b =>
                {
                    b.HasOne("TCRSServerApp.Data.Entities.Category", "Category")
                        .WithMany("ContentPosts")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TCRSServerApp.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TCRSServerApp.Data.Entities.FileMetaData", b =>
                {
                    b.HasOne("TCRSServerApp.Data.Entities.ContentPost", "ContentPost")
                        .WithMany("Files")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ContentPost");
                });

            modelBuilder.Entity("TCRSServerApp.Data.Entities.Category", b =>
                {
                    b.Navigation("ContentPosts");
                });

            modelBuilder.Entity("TCRSServerApp.Data.Entities.ContentPost", b =>
                {
                    b.Navigation("Files");
                });
#pragma warning restore 612, 618
        }
    }
}
