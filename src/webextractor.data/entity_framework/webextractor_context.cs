using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebExtractor.Domain.Models;

namespace WebExtractor.Data.EntityFramework
{
    public class WebExtractorContext : DbContext
    {
        public WebExtractorContext() {}

        public WebExtractorContext(DbContextOptions options)
            : base(options) {
                this.Database.GetMigrations();
                this.Database.Migrate();
            }
        
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
            linkMap.Ignore(p => p.Content);
            linkMap.HasKey(k => k.Id);
            linkMap.Property(p => p.Id).HasColumnName("id");
            linkMap.Property(p => p.Url).HasColumnName("url");
            linkMap.Property(p => p.SiteId).HasColumnName("site_id");

            expressionMap.ToTable("Expression");
            expressionMap.HasKey(k => k.Id);
            expressionMap.Property(p => p.Id).HasColumnName("id");
            expressionMap.Property(p => p.Value).HasColumnName("value");
            expressionMap.Property(p => p.LinkId).HasColumnName("link_id");
            expressionMap.Property(p => p.Order).HasColumnName("order");
            
            siteMap.HasMany(r => r.Links);
            linkMap.HasMany(r => r.Expressions);
        }
    }
}