﻿// <auto-generated />
using System;
using Mercury.Server.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Mercury.Server.Migrations
{
    [DbContext(typeof(MercuryContext))]
    [Migration("20230801120847_personupdate")]
    partial class personupdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AspNetUserRole", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Mercury.Shared.Models.AspNetUser.AspNetRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "NormalizedName" }, "RoleNameIndex")
                        .IsUnique();

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Mercury.Shared.Models.AspNetUser.AspNetRoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "RoleId" }, "IX_AspNetRoleClaims_RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Mercury.Shared.Models.AspNetUser.AspNetUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "NormalizedEmail" }, "EmailIndex");

                    b.HasIndex(new[] { "NormalizedUserName" }, "UserNameIndex")
                        .IsUnique();

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Mercury.Shared.Models.AspNetUser.AspNetUserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "UserId" }, "IX_AspNetUserClaims_UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Mercury.Shared.Models.AspNetUser.AspNetUserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex(new[] { "UserId" }, "IX_AspNetUserLogins_UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Mercury.Shared.Models.AspNetUser.AspNetUserToken", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Mercury.Shared.Models.Mercury.Address", b =>
                {
                    b.Property<int>("AddressID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AddressID"));

                    b.Property<string>("AddressLine1")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<string>("AddressLine2")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<string>("AddressLine3")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<int>("AddressTypeID")
                        .HasColumnType("integer");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("boolean");

                    b.Property<int>("PersonID")
                        .HasColumnType("integer");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("Remarks")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<int?>("SortKey")
                        .HasColumnType("integer");

                    b.HasKey("AddressID");

                    b.HasIndex("AddressTypeID");

                    b.HasIndex("PersonID");

                    b.ToTable("Address", "Mercury");
                });

            modelBuilder.Entity("Mercury.Shared.Models.Mercury.AddressType", b =>
                {
                    b.Property<int>("AddressTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AddressTypeID"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("character varying(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<int?>("SortKey")
                        .HasColumnType("integer");

                    b.HasKey("AddressTypeID");

                    b.ToTable("AddressType", "Mercury");
                });

            modelBuilder.Entity("Mercury.Shared.Models.Mercury.Email", b =>
                {
                    b.Property<int>("EmailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("EmailID"));

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("EmailTypeID")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("boolean");

                    b.Property<int>("PersonID")
                        .HasColumnType("integer");

                    b.Property<string>("Remarks")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<int?>("SortKey")
                        .HasColumnType("integer");

                    b.HasKey("EmailID");

                    b.HasIndex("EmailTypeID");

                    b.HasIndex("PersonID");

                    b.ToTable("Email", "Mercury");
                });

            modelBuilder.Entity("Mercury.Shared.Models.Mercury.EmailType", b =>
                {
                    b.Property<int>("EmailTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("EmailTypeID"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("character varying(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<int?>("SortKey")
                        .HasColumnType("integer");

                    b.HasKey("EmailTypeID");

                    b.ToTable("EmailType", "Mercury");
                });

            modelBuilder.Entity("Mercury.Shared.Models.Mercury.Person", b =>
                {
                    b.Property<int>("PersonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PersonID"));

                    b.Property<decimal?>("AnnualRevenue")
                        .HasColumnType("numeric");

                    b.Property<string>("AppUserID")
                        .HasColumnType("text");

                    b.Property<string>("Culture")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<string>("Remarks")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<string>("VatNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("PersonID");

                    b.HasIndex("AppUserID");

                    b.ToTable("Person", "Mercury");
                });

            modelBuilder.Entity("Mercury.Shared.Models.Mercury.PersonProfile", b =>
                {
                    b.Property<int>("PersonProfileID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PersonProfileID"));

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("date");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<int>("PersonID")
                        .HasColumnType("integer");

                    b.Property<byte[]>("Photo")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.HasKey("PersonProfileID");

                    b.HasIndex("PersonID");

                    b.ToTable("PersonProfile", "Mercury");
                });

            modelBuilder.Entity("Mercury.Shared.Models.Mercury.Phone", b =>
                {
                    b.Property<int>("PhoneID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PhoneID"));

                    b.Property<bool>("IsDefault")
                        .HasColumnType("boolean");

                    b.Property<int>("PersonID")
                        .HasColumnType("integer");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<int>("PhoneTypeID")
                        .HasColumnType("integer");

                    b.Property<string>("Remarks")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<int?>("SortKey")
                        .HasColumnType("integer");

                    b.HasKey("PhoneID");

                    b.HasIndex("PersonID");

                    b.HasIndex("PhoneTypeID");

                    b.ToTable("Phone", "Person");
                });

            modelBuilder.Entity("Mercury.Shared.Models.Mercury.PhoneType", b =>
                {
                    b.Property<int>("PhoneTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PhoneTypeID"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("character varying(2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<int?>("SortKey")
                        .HasColumnType("integer");

                    b.HasKey("PhoneTypeID");

                    b.ToTable("PhoneType", "Mercury");
                });

            modelBuilder.Entity("AspNetUserRole", b =>
                {
                    b.HasOne("Mercury.Shared.Models.AspNetUser.AspNetRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mercury.Shared.Models.AspNetUser.AspNetUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Mercury.Shared.Models.AspNetUser.AspNetRoleClaim", b =>
                {
                    b.HasOne("Mercury.Shared.Models.AspNetUser.AspNetRole", "Role")
                        .WithMany("AspNetRoleClaims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Mercury.Shared.Models.AspNetUser.AspNetUserClaim", b =>
                {
                    b.HasOne("Mercury.Shared.Models.AspNetUser.AspNetUser", "User")
                        .WithMany("AspNetUserClaims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Mercury.Shared.Models.AspNetUser.AspNetUserLogin", b =>
                {
                    b.HasOne("Mercury.Shared.Models.AspNetUser.AspNetUser", "User")
                        .WithMany("AspNetUserLogins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Mercury.Shared.Models.AspNetUser.AspNetUserToken", b =>
                {
                    b.HasOne("Mercury.Shared.Models.AspNetUser.AspNetUser", "User")
                        .WithMany("AspNetUserTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Mercury.Shared.Models.Mercury.Address", b =>
                {
                    b.HasOne("Mercury.Shared.Models.Mercury.AddressType", "AddressType")
                        .WithMany("Addresses")
                        .HasForeignKey("AddressTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mercury.Shared.Models.Mercury.Person", "Person")
                        .WithMany("Addresses")
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AddressType");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Mercury.Shared.Models.Mercury.Email", b =>
                {
                    b.HasOne("Mercury.Shared.Models.Mercury.EmailType", "EmailType")
                        .WithMany("Emails")
                        .HasForeignKey("EmailTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mercury.Shared.Models.Mercury.Person", "Person")
                        .WithMany("Emails")
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EmailType");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Mercury.Shared.Models.Mercury.Person", b =>
                {
                    b.HasOne("Mercury.Shared.Models.AspNetUser.AspNetUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserID");

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("Mercury.Shared.Models.Mercury.PersonProfile", b =>
                {
                    b.HasOne("Mercury.Shared.Models.Mercury.Person", "Person")
                        .WithMany("PersonProfiles")
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Mercury.Shared.Models.Mercury.Phone", b =>
                {
                    b.HasOne("Mercury.Shared.Models.Mercury.Person", "Person")
                        .WithMany("Phones")
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mercury.Shared.Models.Mercury.PhoneType", "PhoneType")
                        .WithMany("Phones")
                        .HasForeignKey("PhoneTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");

                    b.Navigation("PhoneType");
                });

            modelBuilder.Entity("Mercury.Shared.Models.AspNetUser.AspNetRole", b =>
                {
                    b.Navigation("AspNetRoleClaims");
                });

            modelBuilder.Entity("Mercury.Shared.Models.AspNetUser.AspNetUser", b =>
                {
                    b.Navigation("AspNetUserClaims");

                    b.Navigation("AspNetUserLogins");

                    b.Navigation("AspNetUserTokens");
                });

            modelBuilder.Entity("Mercury.Shared.Models.Mercury.AddressType", b =>
                {
                    b.Navigation("Addresses");
                });

            modelBuilder.Entity("Mercury.Shared.Models.Mercury.EmailType", b =>
                {
                    b.Navigation("Emails");
                });

            modelBuilder.Entity("Mercury.Shared.Models.Mercury.Person", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Emails");

                    b.Navigation("PersonProfiles");

                    b.Navigation("Phones");
                });

            modelBuilder.Entity("Mercury.Shared.Models.Mercury.PhoneType", b =>
                {
                    b.Navigation("Phones");
                });
#pragma warning restore 612, 618
        }
    }
}
