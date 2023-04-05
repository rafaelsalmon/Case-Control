using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ControleDeCasos.Models
{
    // É possível adicionar dados do perfil do usuário adicionando mais propriedades na sua classe ApplicationUser, visite https://go.microsoft.com/fwlink/?LinkID=317594 para obter mais informações.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Observe que a authenticationType deve corresponder a uma definida em CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Adicionar declarações do usuário personalizadas aqui
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<ControleDeCasos.Models.Caso> Casoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public System.Data.Entity.DbSet<ControleDeCasos.Models.Pais> Pais { get; set; }

        public System.Data.Entity.DbSet<ControleDeCasos.Models.Estado> Estadoes { get; set; }

        public System.Data.Entity.DbSet<ControleDeCasos.Models.Cidade> Cidades { get; set; }

        public System.Data.Entity.DbSet<ControleDeCasos.Models.TipoReacao> TipoReacaos { get; set; }

        public System.Data.Entity.DbSet<ControleDeCasos.Models.Reacao> Reacaos { get; set; }

        public System.Data.Entity.DbSet<ControleDeCasos.Models.GrupoReacao> GrupoReacaos { get; set; }

        public System.Data.Entity.DbSet<ControleDeCasos.Models.Laboratorio> Laboratorios { get; set; }
    }
}