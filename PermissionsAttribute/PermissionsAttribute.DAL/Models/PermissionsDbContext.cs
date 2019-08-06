﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq;

namespace PermissionsAttribute.DAL.Models
{
    public partial class PermissionsDbContext : DbContext
    {
        public PermissionsDbContext()
        {
        }

        public PermissionsDbContext(DbContextOptions<PermissionsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Permission> Permission { get; set; }

        public virtual DbSet<Profile> Profile { get; set; }

        public virtual DbSet<Role> Role { get; set; }

        public virtual DbSet<RolePermission> RolePermission { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Profile>(entity =>
            {
                entity.Property(e => e.Email).HasMaxLength(300);

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Profile)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Profile_RoleId");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.RolePermission)
                    .HasForeignKey(d => d.PermissionId)
                    .HasConstraintName("FK_RolePermission_PermissionId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RolePermission)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_RolePermission_RoleId");
            });
        }
    }
}
