using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MySQLOracle
{
    public partial class BookSqlContext : DbContext
    {
        public BookSqlContext()
        {
        }

        public BookSqlContext(DbContextOptions<BookSqlContext> options)
            : base(options)
        {
        }

        public virtual DbSet<NewTable> NewTables { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var spath = Application.StartupPath + @"DB\book_sql.db";
                optionsBuilder.UseSqlite($" Data Source= {spath}");

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NewTable>(entity =>
            {
                entity.ToTable("new_table");

                entity.HasIndex(e => e.Name, "IX_new_table_Name")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.StrPlplus).HasColumnName("Str_plplus");

                entity.Property(e => e.StrPlsql).HasColumnName("Str_plsql");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
