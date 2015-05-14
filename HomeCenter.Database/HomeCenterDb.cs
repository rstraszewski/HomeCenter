using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations.History;
using HomeCenter.AuthenticationDomain;
using HomeCenter.CommonDomain;
using HomeCenter.DeviceDomain;
using System.Data.Entity.Infrastructure;
using HomeCenter.HistoryDomain;

namespace HomeCenter.Database
{
    public class HomeCenterDbInitializer : CreateDatabaseIfNotExists<HomeCenterDb>
    {
        protected override void Seed(HomeCenterDb db)
        {
            db.Set<DeviceType>().Add(new DeviceType()
            {
                Binary = false,
                Changable = true,
                Description = "Let you dimm lights from website",
                IsVirtual = true,
                Range = new Range() { MaxValue = 255, MinValue = 0 },
                Type = "Virtual Dimmer"
            });
            db.Set<DeviceType>().Add(new DeviceType()
            {
                Binary = false,
                Changable = true,
                Description = "Let you dimm lights",
                IsVirtual = false,
                Range = new Range() { MaxValue = 255, MinValue = 0 },
                Type = "Dimmer"
            });
            db.Set<DeviceType>().Add(new DeviceType()
            {
                Binary = true,
                Changable = true,
                Description = "Let you switch lights from website",
                IsVirtual = true,
                Range = new Range() { MaxValue = 255, MinValue = 0},
                Type = "Virtual Switcher"
            });
            db.Set<DeviceType>().Add(new DeviceType()
            {
                Binary = true,
                Changable = true,
                Description = "Let you switch lights",
                IsVirtual = false,
                Range = new Range() { MaxValue = 255, MinValue = 0 },
                Type = "Switcher"
            });
            db.Set<DeviceType>().Add(new DeviceType()
            {
                Binary = false,
                Changable = false,
                Description = "Let you check temperature on website",
                IsVirtual = true,
                Range = new Range() { MaxValue = -50, MinValue = 50 },
                Type = "Virtual Temperature Monitor"
            });
            db.Set<DeviceType>().Add(new DeviceType()
            {
                Binary = false,
                Changable = false,
                Description = "Let you check temperature",
                IsVirtual = false,
                Range = new Range() { MaxValue = -50, MinValue = 50 },
                Type = "Virtual Temperature Monitor"
            });
            db.Set<User>().Add(new User()
            {
                PasswordHash = "CZZ4Uf3LmeRducc7SUWC4hoiWopnVus296PbL3QS8+M=",
                Roles = "User;Administrator",
                Salt = "bA8ZaD11HI6gyenaN4fv2avPw/4B2Fgf",
                Username = "admin"
            });
            for (int i = 1; i < 255; i++)
            {
                db.Set<Address>().Add(new Address()
                {
                    AddressNumber = i,
                    IsAssigned = true,
                    DeviceId = null
                });
            }

            db.Set<DeviceType>().Add(new DeviceType()
            {
                Binary = true,
                Changable = true,
                Description = "Shows if open on website",
                IsVirtual = true,
                Range = new Range() { MaxValue = 1, MinValue = 0 },
                Type = "Virtual Contactron"
            });
            db.Set<DeviceType>().Add(new DeviceType()
            {
                Binary = true,
                Changable = true,
                Description = "Checks if open",
                IsVirtual = false,
                Range = new Range() { MaxValue = 1, MinValue = 0 },
                Type = "Contactron"
            });

            db.SaveChanges();
        }
    }

    public class HomeCenterDb : DbContext, IDbContext
    {
        public HomeCenterDb()
            : base()
        {
            

        }
        public HomeCenterDb(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Device>().MapToStoredProcedures(s => s
                    .Update(u => u.Parameter(p => p.Id, "device_Id"))
                    .Delete(u => u.Parameter(p => p.Id, "device_Id")))
                    .HasKey(m => m.Id);

            modelBuilder.Entity<Building>()
                .MapToStoredProcedures(s => s
                    .Update(u => u.Parameter(p => p.Id, "building_Id"))
                    .Delete(u => u.Parameter(p => p.Id, "building_Id")))
                .HasKey(b => b.Id)
                .HasMany(b => b.Floors)
                .WithRequired()
                .HasForeignKey(f => f.BuildingId);

            modelBuilder.Entity<Floor>().MapToStoredProcedures(s => s
                    .Update(u => u.Parameter(p => p.Id, "floor_Id"))
                    .Delete(u => u.Parameter(p => p.Id, "floor_Id")))
                .HasKey(m => m.Id)
                .HasMany(f => f.Rooms)
                .WithRequired()
                .HasForeignKey(f => f.FloorId);

            modelBuilder.Entity<Room>().MapToStoredProcedures(s => s
                    .Update(u => u.Parameter(p => p.Id, "room_Id"))
                    .Delete(u => u.Parameter(p => p.Id, "room_Id")))
                .HasKey(m => m.Id);

            modelBuilder.Entity<User>().MapToStoredProcedures(s => s
                    .Update(u => u.Parameter(p => p.Id, "user_Id"))
                    .Delete(u => u.Parameter(p => p.Id, "user_Id")));

            modelBuilder.Entity<HistoryLog>().MapToStoredProcedures(s => s
                    .Update(u => u.Parameter(p => p.Id, "log_Id"))
                    .Delete(u => u.Parameter(p => p.Id, "log_Id")));

            modelBuilder.Entity<DeviceRelation>().MapToStoredProcedures(s => s
                    .Update(u => u.Parameter(p => p.Id, "relation_Id"))
                    .Delete(u => u.Parameter(p => p.Id, "relation_Id")));

            modelBuilder.Entity<DeviceJob>().MapToStoredProcedures(s => s
                    .Update(u => u.Parameter(p => p.Id, "deviceJob_Id"))
                    .Delete(u => u.Parameter(p => p.Id, "deviceJob_Id")));

            modelBuilder.Entity<Address>().MapToStoredProcedures(s => s
                    .Update(u => u.Parameter(p => p.Id, "address_Id"))
                    .Delete(u => u.Parameter(p => p.Id, "address_Id")));

            base.OnModelCreating(modelBuilder);
            
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
           
        }

        public void Rollback()
        {
            foreach (var entry in ChangeTracker.Entries())
                entry.Reload();
        }

        public void CreateDatabaseIfNotExisting()
        {
            Database.CreateIfNotExists();
        }

        public void EnableLazyLoading()
        {
            Configuration.LazyLoadingEnabled = true;
        }
    }

}
