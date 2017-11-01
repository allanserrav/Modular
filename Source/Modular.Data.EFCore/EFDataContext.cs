using Microsoft.EntityFrameworkCore;
using System.Linq;
using Modular.CoreShared.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modular.Shared.Data;
using Modular.Shared.Data.System;

namespace Modular.Data.EFCore
{
    public class EFDataContext : DbContext, IDataWrite<IDataSysKey>, IDataRead<IDataSysKey>
    {
        public DbContextOptions<EFDataContext> Options { get; }

        public EFDataContext(DbContextOptions<EFDataContext> options) : base(options)
        {
            Options = options;
        }

        public new void Add<TEntity>(TEntity entity) where TEntity : class, IBaseData, IDataSysKey
        {
            var set = this.Set<TEntity>();
            set.Add(entity);
            SaveChanges();

        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class, IBaseData, IDataSysKey
        {
            Remove(entity);
        }

        public TEntity Get<TEntity>(IDataSysKey key) where TEntity : class, IBaseData, IDataSysKey
        {
            return Find<TEntity>(key.Id);
        }

        public TEntity GetByCodigo<TEntity>(string codigo) where TEntity : class, IBaseData, IDataSysKey
        {
            var query = GetQuery<TEntity>();
            return query.FirstOrDefault(c => c.Codigo == codigo);
        }

        public IQueryable<TEntity> GetQuery<TEntity>() where TEntity : class, IBaseData, IDataSysKey
        {
            return IncludeAll.Resolve(Set<TEntity>());
        }

        public new void Update<TEntity>(TEntity entity) where TEntity : class, IBaseData, IDataSysKey
        {
            var set = this.Set<TEntity>();
            set.Update(entity);
            SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(b => {
                b.ToTable("usuario");
                HasBaseEntidade(b, true, true, true, true, true, true, true, true, true);

                b.Property(e => e.Nome)
                    .HasMaxLength(200)
                    .IsRequired();
                b.Property(e => e.SegundosLoginExpirar)
                    .IsRequired();
                b.Property(e => e.Senha)
                    .HasMaxLength(10)
                    .IsRequired();
                b.Ignore(e => e.EmpresaEmUSo);
            });

            modelBuilder.Entity<Empresa>(b => {
                b.ToTable("empresa");
                HasBaseEntidade(b, true, true, true, true, false, true, true, false);

                b.Property(e => e.ConnectionStringDb);
                b.Property(e => e.DatabaseName);
                b.Ignore(e => e.EmpresaPai);
                b.Ignore(e => e.EmpresaMaster);
            });

            modelBuilder.Entity<Modulo>(b => {
                b.ToTable("modulo");
                HasBaseEntidade(b, true, true, true, true, false, false, false, false);

                b.Property(e => e.Nome)
                    .HasMaxLength(100)
                    .IsRequired();
                b.Property(e => e.IsAgrupamento);
                b.Property(e => e.IsObsoleto);
                b.Property(e => e.Categoria)
                    .HasMaxLength(200)
                    .IsRequired();

                b.Ignore(e => e.AgrupamentoIn);
                b.Ignore(e => e.AnteriorIn);
                b.Ignore(e => e.IsPrincipal);

                b.HasMany(e => e.AgrupamentoOrdenacaoIn)
                    .WithOne(e => e.ModuloAgrupamento)
                    ;
            });

            modelBuilder.Entity<ModuloOrdem>(b => {
                b.ToTable("modulo_ordem");
                HasBaseEntidade(b, false, true, true, true, false, false, false, false);

                b.HasOne(e => e.ModuloExecutor)
                    ;

                b.Property(e => e.IsPrincipal);
                b.Property(e => e.Ordem);
            });

            modelBuilder.Entity<AssemblyModulo>(b => {

                b.Property(e => e.Assembly)
                    .HasMaxLength(1000)
                    .IsRequired();

                b.Property(e => e.AssemblyFullPath)
                    .HasMaxLength(1000)
                    .IsRequired();
            });

            modelBuilder.Entity<ScriptModulo>(b => {

                b.Property(e => e.ScriptMethod)
                    .HasMaxLength(20);

                b.Property(e => e.ScriptResourceId);

                b.Ignore(e => e.ScriptResourceText);

                b.Property(e => e.ScriptTipo)
                    .IsRequired();
            });

        }

        void HasBaseEntidade<TEntidade>(EntityTypeBuilder<TEntidade> builder, bool mapCodigo = true, bool mapCreatedOn = true, bool mapUpdateOn = true, bool mapVersion = true,
                         bool mapSyncro = true, bool mapExterno = true, bool mapStatus = true, bool mapArquivo = true,
                         bool mapEmpresa = false)
            where TEntidade : class, IBaseData, IDataSysKey
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.IsDesabilitado);
            /////
            // Código
            if (mapCodigo) {
                builder.HasIndex(e => e.Codigo)
                    .IsUnique();
                builder.Property(e => e.Codigo)
                    .HasMaxLength(20);
            }
            else {
                builder.Ignore(e => e.Codigo);
            }
            // CreatedOn
            if (mapCreatedOn) {
                builder.Property(e => e.CreatedOn)
                    //.HasDefaultValue(DateTime.Now)
                    .ValueGeneratedOnAdd();
            }
            else {
                builder.Ignore(e => e.CreatedOn);
            }
            /////
            // UpdatedOn
            if (mapUpdateOn) {
                builder.Property(e => e.UpdatedOn)
                    //.HasDefaultValue(DateTime.Now)
                    ;
            }
            else {
                builder.Ignore(e => e.UpdatedOn);
            }
            /////
            // Empresa
            //if (mapEmpresa) {
            //    builder.PropertyParseAndWrite(e => e.EmpresaId);
            //}
            //else {
            //    builder.IgnoreWrite(e => e.EmpresaId);
            //}
            /////
            // Status
            //if (mapStatus) {
            //    builder.PropertyParseAndWrite(e => e.Status);
            //}
            //else {
            //    builder.IgnoreWrite(e => e.Status);
            //}
        }

        
    }
}
