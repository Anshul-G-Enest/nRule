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
        public DbSet<NRule> NRules { get; set; }
        public DbSet<RuleEngine> RuleEngines { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<StatementConnector> StatementConnectors { get; set; }
        public DbSet<Operation> Operations { get; set; }

    }
}
