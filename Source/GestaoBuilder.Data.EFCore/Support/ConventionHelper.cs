using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modular.Data.EFCore.Support
{
    public static class ConventionHelper
    {
        public static void ResolveFields(this ModelBuilder modelBuilder, DbContextOptions<EFDataContext> options)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes()) {
                foreach (var property in entity.GetProperties()) {
                    //ResolveSqliteFields(property);
                }
            }
        }

        //static void ResolveSqliteFields(IMutableProperty property)
        //{
        //    if (property.ClrType.Equals(typeof(DateTime)) || property.ClrType.Equals(typeof(DateTime?))) {
        //        property.Relational().ColumnType = "DATETIME";
        //    }
        //    else if (property.ClrType.Equals(typeof(short))) {
        //        property.Relational().ColumnType = "SMALLINT";
        //    }
        //    else if (property.ClrType.Equals(typeof(bool))) {
        //        property.Relational().ColumnType = "BOOLEAN";
        //    }
        //    else if (property.ClrType.Equals(typeof(decimal))) {
        //        property.Relational().ColumnType = "MONEY";
        //    }
        //    else if (property.ClrType.Equals(typeof(double))) {
        //        property.Relational().ColumnType = "DOUBLE";
        //    }
        //    else if (property.ClrType.Equals(typeof(TimeSpan))) {
        //        property.Relational().ColumnType = "TIME";
        //    }

        //    // Default Value
        //    // property.GetOrAddAnnotation("", )
        //}
    }
}
