using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Config
{
    public class ReservaPasajeroConfig : IEntityTypeConfiguration<ReservaPasajero>
    {
        public void Configure(EntityTypeBuilder<ReservaPasajero> entityBuilder)
        {
            entityBuilder.ToTable("ReservaPasajero");
            entityBuilder.HasKey(rp => new { rp.ReservaPasajeroId });

            entityBuilder.Property(rp => rp.ReservaPasajeroId).ValueGeneratedOnAdd();
        }
    }
}
