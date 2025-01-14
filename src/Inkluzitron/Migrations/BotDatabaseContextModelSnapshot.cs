﻿// <auto-generated />
using System;
using Inkluzitron.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Inkluzitron.Migrations
{
    [DbContext(typeof(BotDatabaseContext))]
    partial class BotDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("Inkluzitron.Data.Entities.BdsmTestOrgItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("ParentId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Score")
                        .HasColumnType("REAL");

                    b.Property<string>("Trait")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("BdsmTestOrgItems");
                });

            modelBuilder.Entity("Inkluzitron.Data.Entities.BdsmTestOrgResult", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Link")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("SubmittedAt")
                        .HasColumnType("TEXT");

                    b.Property<ulong>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Link")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("BdsmTestOrgResults");
                });

            modelBuilder.Entity("Inkluzitron.Data.Entities.RicePurityResult", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<uint>("Score")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("SubmittedAt")
                        .HasColumnType("TEXT");

                    b.Property<ulong>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RicePurityResults");
                });

            modelBuilder.Entity("Inkluzitron.Data.Entities.RoleMenuMessage", b =>
                {
                    b.Property<ulong>("GuildId")
                        .HasColumnType("INTEGER");

                    b.Property<ulong>("ChannelId")
                        .HasColumnType("INTEGER");

                    b.Property<ulong>("MessageId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("CanSelectMultiple")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("GuildId", "ChannelId", "MessageId");

                    b.ToTable("RoleMenuMessages");
                });

            modelBuilder.Entity("Inkluzitron.Data.Entities.RoleMenuMessageRole", b =>
                {
                    b.Property<ulong>("RoleId")
                        .HasColumnType("INTEGER");

                    b.Property<ulong>("GuildId")
                        .HasColumnType("INTEGER");

                    b.Property<ulong>("ChannelId")
                        .HasColumnType("INTEGER");

                    b.Property<ulong>("MessageId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Emote")
                        .HasColumnType("TEXT");

                    b.Property<string>("Mention")
                        .HasColumnType("TEXT");

                    b.HasKey("RoleId", "GuildId", "ChannelId", "MessageId");

                    b.HasIndex("GuildId", "ChannelId", "MessageId");

                    b.ToTable("RoleMenuMessageRoles");
                });

            modelBuilder.Entity("Inkluzitron.Data.Entities.User", b =>
                {
                    b.Property<ulong>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastMessagePointsIncrement")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastReactionPointsIncrement")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<long>("Points")
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("BLOB");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Inkluzitron.Data.Entities.BdsmTestOrgItem", b =>
                {
                    b.HasOne("Inkluzitron.Data.Entities.BdsmTestOrgResult", "Parent")
                        .WithMany("Items")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Inkluzitron.Data.Entities.BdsmTestOrgResult", b =>
                {
                    b.HasOne("Inkluzitron.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Inkluzitron.Data.Entities.RicePurityResult", b =>
                {
                    b.HasOne("Inkluzitron.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Inkluzitron.Data.Entities.RoleMenuMessageRole", b =>
                {
                    b.HasOne("Inkluzitron.Data.Entities.RoleMenuMessage", "Message")
                        .WithMany("Roles")
                        .HasForeignKey("GuildId", "ChannelId", "MessageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Message");
                });

            modelBuilder.Entity("Inkluzitron.Data.Entities.BdsmTestOrgResult", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Inkluzitron.Data.Entities.RoleMenuMessage", b =>
                {
                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}
