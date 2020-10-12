using Core.Abstractions;
using Domain.DataModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataLayer
{
	public class FamilyTaskContext : DbContext
	{

		public FamilyTaskContext(DbContextOptions<FamilyTaskContext> options) : base(options)
		{
			//this.ChangeTracker.LazyLoadingEnabled = true;
		}

		public DbSet<Member> Members { get; set; }
		public DbSet<FamilyTask> Task { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Member>(entity =>
			{
				entity.HasKey(k => k.Id);
				entity.ToTable("Member");
			});



			modelBuilder.Entity<FamilyTask>(entity =>
			{
				entity.HasKey(k => k.Id);
				entity.HasOne<Member>(p => p.AssignedMember).WithMany(b => b.Tasks)
				.HasForeignKey(p=>p.AssignedMemberId);
				entity.ToTable("Task");
			});
		}
	}
}