using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebExtractor.Domain.Models;

namespace WebExtractor.Data.EntityFramework
{
    public class WebExtractorContext : DbContext
    {
        public WebExtractorContext() {}

        public WebExtractorContext(DbContextOptions options)
            : base(options) { }
        
        public DbSet<Site> Sites { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<Expression> Expressions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            EntityTypeBuilder<Site> siteMap = modelBuilder.Entity<Site>();
            EntityTypeBuilder<Link> linkMap = modelBuilder.Entity<Link>();
            EntityTypeBuilder<Expression> expressionMap = modelBuilder.Entity<Expression>();
            
            siteMap.ToTable("Site");
            siteMap.HasKey(k => k.Id);
            siteMap.Property(p => p.Id).HasColumnName("id");
            siteMap.Property(p => p.Name).HasColumnName("name");
            siteMap.Property(p => p.Domain).HasColumnName("domain");

            linkMap.ToTable("Link");
            linkMap.HasKey(k => k.Id);
            linkMap.Property(p => p.Id).HasColumnName("id");
            linkMap.Property(p => p.Url).HasColumnName("url");

            expressionMap.ToTable("Expression");
            expressionMap.HasKey(k => k.Id);
            expressionMap.Property(p => p.Id).HasColumnName("id");
            expressionMap.Property(p => p.Value).HasColumnName("value");
            
            siteMap.HasMany(r => r.Links);
            linkMap.HasMany(r => r.Expressions);
        }
    }
}