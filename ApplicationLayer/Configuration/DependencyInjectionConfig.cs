

using System.ComponentModel;
using Dapper;
using DomainLayer.Interfaces.Infrastructure;
using DomainLayer.Interfaces.Repository;
using DomainLayer.Interfaces.Service;
using InfrastructureLayer.Data.Repository;
using ServiceLayer;

namespace ApplicationLayer.Configuration
{
    /// <summary>
    /// Classe responsável por gerenciar as dependencias
    /// </summary>
    public static class DependencyInjectionConfig
    {
        /// <summary>
        /// Configura as dependencias das camadas
        /// </summary>
        /// <param name="services"></param>
        public static void Configure(IServiceCollection services)
        {
            ConfigureApplicationLayer(services);
            ConfigureDomainLayer(services);
            ConfigureInfrastructureLayer(services);
            ConfigureServiceLayer(services);

            services.AddLogging();
        }

        /// <summary>
        /// Configura as dependencias da camada de aplicação
        /// </summary>
        /// <param name="services"></param>
        private static void ConfigureApplicationLayer(IServiceCollection services) { }

        /// <summary>
        /// Configura as dependencias da camada de dominio
        /// </summary>
        /// <param name="services"></param>
        private static void ConfigureDomainLayer(IServiceCollection services) { }

        /// <summary>
        /// Configura as dependencias da camada de infraestrutura
        /// </summary>
        /// <param name="services"></param>
        private static void ConfigureInfrastructureLayer(IServiceCollection services) {

            services.AddScoped<IDbContext, AppDbContext>();
            services.AddSingleton<ISqlServerConnectionProvider, SqlServerConnectionProvider>();
            services.AddScoped<IAlunoRepository, AlunoRepository>();
            services.AddScoped<IProfessorRepository, ProfessorRepository>();
            SqlMapper.AddTypeHandler(new DateOnlyHandler());
            SqlMapper.AddTypeHandler(new TimeOnlyHandler());
        }

        /// <summary>
        /// Configura as dependencias da camada de aplicação
        /// </summary>
        /// <param name="services"></param>
        private static void ConfigureServiceLayer(IServiceCollection services)
        {
            services.AddScoped<IProfessorService, ProfessorService>();
            services.AddScoped<IAlunoService, AlunoService>();
        }
    }
}