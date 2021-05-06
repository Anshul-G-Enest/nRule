using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Rule.WebAPI.Model;
using Rule.WebAPI.Model.DTO;
using System;
using System.Collections.Generic;

namespace Rule.WebAPI.Context
{
    public class RuleDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContext;

        public RuleDbContext(DbContextOptions options, IHttpContextAccessor httpContext)
            : base(options)
        {
            _httpContext = httpContext;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StatementConnector>().HasData(new List<StatementConnector>() {
            new StatementConnector { Id = 1, Name = "And" },
            new StatementConnector { Id = 2, Name = "Or" }
            });

            modelBuilder.Entity<Operation>().HasData(GetOperation());
            modelBuilder.Entity<EntityType>().HasData(GetEntityTypes());
        }

        private List<Operation> GetOperation()
        {
            List<Operation> operations = new List<Operation>();
            foreach (FilterOperation operation in Enum.GetValues(typeof(FilterOperation)))
            {
                operations.Add(new Operation { Id = (int)operation, Name = operation.ToString() });
            }
            return operations;
        }

        private List<EntityType> GetEntityTypes()
        {
            List<EntityType> entityTypes = new List<EntityType>();
            foreach (EntityTypeEnum entityType in Enum.GetValues(typeof(EntityTypeEnum)))
            {
                entityTypes.Add(new EntityType { Id = (int)entityType, Name = entityType.ToString() });
            }
            return entityTypes;
        }

        public DbSet<NRule> NRules { get; set; }
        public DbSet<RuleEngine> RuleEngines { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<StatementConnector> StatementConnectors { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<EntityType> EntityTypes { get; set; }

    }
}
