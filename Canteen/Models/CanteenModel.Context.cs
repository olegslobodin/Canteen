﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Canteen.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CanteenEntities : DbContext
    {
        public CanteenEntities()
            : base("name=CanteenEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Benefit> Benefits { get; set; }
        public virtual DbSet<Dish> Dishes { get; set; }
        public virtual DbSet<Dishes_Products> Dishes_Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Orders_Dishes> Orders_Dishes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Students_Benefits> Students_Benefits { get; set; }
        public virtual DbSet<Worker> Workers { get; set; }
    }
}