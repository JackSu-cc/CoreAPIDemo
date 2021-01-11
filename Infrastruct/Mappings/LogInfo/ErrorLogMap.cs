
//******************************************************************/
// Copyright (C) 2021 All Rights Reserved.
// CLR版本：4.0.30319.42000
// 版本号：V1.0.0
// 文件名：ErrorLogMap
// 创建者：名字 (cc)
// 创建时间：2021/1/11 22:15:30
//
// 描述：
//
// 
//=================================================================
// 修改人：
// 时间：
// 修改说明：
// 
//******************************************************************/


using Domain.Models.LogInfo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastruct.Mappings.LogInfo
{
    public class ErrorLogMap : IEntityTypeConfiguration<ErrorLog>
    {
        public void Configure(EntityTypeBuilder<ErrorLog> builder)
        {
            builder.ToTable(nameof(ErrorLog));
            builder.HasKey(c => c.Id);
            builder.Property(c => c.LogInfo).HasColumnType("nvarchar(MAX)");
            builder.Property(c => c.Module).HasColumnType("nvarchar(200)").HasMaxLength(200);
            builder.Property(c => c.Action).HasColumnType("nvarchar(200)").HasMaxLength(200);
            builder.Property(c => c.Ip).HasColumnType("nvarchar(200)").HasMaxLength(200);

            builder.Property(c => c.CreateBy).HasColumnType("nvarchar(50)").HasMaxLength(50);
            builder.Property(c => c.CreateTime).HasColumnType("datetime");

        }
    }
}