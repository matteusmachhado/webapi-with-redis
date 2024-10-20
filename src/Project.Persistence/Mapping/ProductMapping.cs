using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistence.Mapping
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("tb_products");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .HasColumnType("varchar(100)");

            builder.Property(t => t.Description)
                .HasColumnType("varchar(1000)");

            builder.Property(t => t.Price)
                .IsRequired()
                .HasColumnType("decimal(18, 2)");
        }
    }
}
