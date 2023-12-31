﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVG_TASK_APP.Models.Configuaration
{
    public class UserTableConfiguration : IEntityTypeConfiguration<UserTable>
    {
        public void Configure(EntityTypeBuilder<UserTable> builder)
        {
            builder.ToTable("User Tables");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Role).IsRequired();
            builder.HasOne(x => x.User).WithMany(x => x.UserTables).HasForeignKey(x => x.Id_User);
            builder.HasOne(x => x.Table).WithMany(x => x.UserTables).HasForeignKey(x => x.Id_Table);
        }
    }
}
