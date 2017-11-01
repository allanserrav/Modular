using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Modular.Data.EFCore;
using Modular.Shared;

namespace Modular.Data.EFCoreMigrations.Migrations
{
    [DbContext(typeof(EFDataContext))]
    partial class EFDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GestaoBuilder.Shared.Empresa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Codigo")
                        .HasMaxLength(20);

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDesabilitado");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("Id");

                    b.HasIndex("Codigo")
                        .IsUnique();

                    b.ToTable("empresa");
                });

            modelBuilder.Entity("GestaoBuilder.Shared.Modulo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Categoria")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Codigo")
                        .HasMaxLength(20);

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<bool>("IsAgrupamento");

                    b.Property<bool>("IsDesabilitado");

                    b.Property<bool>("IsObsoleto");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("Id");

                    b.HasIndex("Codigo")
                        .IsUnique();

                    b.ToTable("modulo");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Modulo");
                });

            modelBuilder.Entity("GestaoBuilder.Shared.ModuloOrdem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDesabilitado");

                    b.Property<bool>("IsPrincipal");

                    b.Property<int?>("ModuloAgrupamentoId");

                    b.Property<int?>("ModuloExecutorId");

                    b.Property<int>("Ordem");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("Id");

                    b.HasIndex("ModuloAgrupamentoId");

                    b.HasIndex("ModuloExecutorId");

                    b.ToTable("modulo_ordem");
                });

            modelBuilder.Entity("GestaoBuilder.Shared.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Codigo")
                        .HasMaxLength(20);

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDesabilitado");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int>("SegundosLoginExpirar");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("Id");

                    b.HasIndex("Codigo")
                        .IsUnique();

                    b.ToTable("usuario");
                });

            modelBuilder.Entity("GestaoBuilder.Shared.AssemblyModulo", b =>
                {
                    b.HasBaseType("GestaoBuilder.Shared.Modulo");

                    b.Property<string>("Assembly")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.Property<string>("AssemblyFullPath")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.ToTable("AssemblyModulo");

                    b.HasDiscriminator().HasValue("AssemblyModulo");
                });

            modelBuilder.Entity("GestaoBuilder.Shared.ScriptModulo", b =>
                {
                    b.HasBaseType("GestaoBuilder.Shared.Modulo");

                    b.Property<string>("ScriptMethod")
                        .HasMaxLength(20);

                    b.Property<int>("ScriptResourceId");

                    b.Property<int>("ScriptTipo");

                    b.ToTable("ScriptModulo");

                    b.HasDiscriminator().HasValue("ScriptModulo");
                });

            modelBuilder.Entity("GestaoBuilder.Shared.ModuloOrdem", b =>
                {
                    b.HasOne("GestaoBuilder.Shared.Modulo", "ModuloAgrupamento")
                        .WithMany("AgrupamentoOrdenacaoIn")
                        .HasForeignKey("ModuloAgrupamentoId");

                    b.HasOne("GestaoBuilder.Shared.Modulo", "ModuloExecutor")
                        .WithMany("ExecutaOrdems")
                        .HasForeignKey("ModuloExecutorId");
                });
        }
    }
}
