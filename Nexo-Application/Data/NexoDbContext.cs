using Microsoft.EntityFrameworkCore;
using NexoApplication.Models.Entities;
using System;

namespace NexoApplication.Data
{
    public class NexoDbContext : DbContext
    {
        public NexoDbContext(DbContextOptions<NexoDbContext> options)
            : base(options)
        {

        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }

    }
}
