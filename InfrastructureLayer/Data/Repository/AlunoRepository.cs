using ApplicationLayer;
using Dapper;
using DomainLayer.Interfaces.Infrastructure;
using DomainLayer.Interfaces.Repository;
using DomainLayer.Models;
using DomainLayer.ViewModels;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace InfrastructureLayer.Data.Repository
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly IDbContext _context;
        private readonly ILogger<AlunoRepository> _logger;

        public AlunoRepository(ILogger<AlunoRepository> logger, IDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task Registra(AlunoCadastroViewModel viewModel)
        {
            _logger.LogInformation($"[AlunoRepository]-[Registra] -> [Start]: Payload -> {JsonSerializer.Serialize(viewModel)}");

            using var connection = _context.CreateConnection;

            const string query = "INSERT INTO aluno(Nome, Matricula, DataNascimento) VALUES (@Nome, @Matricula, @DataNascimento);";
            var parameters = new
            {
                viewModel.Nome,
                viewModel.Matricula,
                DataNascimento = viewModel.DataNascimento.ToString("yyyy-MM-dd")
            };
            
            try
            {
                await connection.ExecuteAsync(query, parameters);
            } catch (Exception exception) {
                _logger.LogError($"[AlunoRepository]-[Registra] -> [Exception]: Message -> {exception.Message}");
                _logger.LogError($"[AlunoRepository]-[Registra] -> [InnerException]: Message -> {exception.InnerException}");
                throw;
            }

            _logger.LogInformation("[AlunoRepository]-[Registra] -> [Finish]");
        }

        public async Task<IEnumerable<Aluno>> Lista()
        {
            _logger.LogInformation($"[AlunoRepository]-[Lista] -> [Start]");

            using var connection = _context.CreateConnection;

            const string query = "SELECT * FROM aluno;";

            try
            {
                var result = await connection.QueryAsync<Aluno>(query);
             
                _logger.LogInformation("[AlunoRepository]-[Lista] -> [Finish]");
                return result;
            }
            catch (Exception exception)
            {
                _logger.LogError($"[AlunoRepository]-[Lista] -> [Exception]: Message -> {exception.Message}");
                _logger.LogError($"[AlunoRepository]-[Lista] -> [InnerException]: Message -> {exception.InnerException}");
                throw;
            }
        }

        public async Task Apaga(Guid id)
        {
            _logger.LogInformation($"[AlunoRepository]-[Apaga] -> [Start]: GUID -> {id}");

            using var connection = _context.CreateConnection;

            const string query = "DELETE FROM aluno WHERE Id = @Id";
            var param = new
            {
                Id = id
            };

            try
            {
                await connection.ExecuteAsync(query, param);
            }
            catch (Exception exception)
            {
                _logger.LogError($"[AlunoRepository]-[Apaga] -> [Exception]: Message -> {exception.Message}");
                _logger.LogError($"[AlunoRepository]-[Apaga] -> [InnerException]: Message -> {exception.InnerException}");
                throw;
            }

            _logger.LogInformation("[AlunoRepository]-[Apaga] -> [Finish]");
        }

        public async Task Atualiza(Aluno aluno)
        {
            _logger.LogInformation($"[AlunoRepository]-[Atualiza] -> [Start]: Payload -> {JsonSerializer.Serialize(aluno)}");

            using var connection = _context.CreateConnection;

            const string query = "UPDATE aluno SET Nome = @Nome, DataNascimento = @DataNascimento WHERE Matricula = @Matricula;";
            var parameters = new
            {
                aluno.Nome,
                aluno.Matricula,
                DataNascimento = aluno.DataNascimento.ToString("yyyy-MM-dd")
            };

            try
            {
                await connection.ExecuteAsync(query, parameters);
            }
            catch (Exception exception)
            {
                _logger.LogError($"[AlunoRepository]-[Atualiza] -> [Exception]: Message -> {exception.Message}");
                _logger.LogError($"[AlunoRepository]-[Atualiza] -> [InnerException]: Message -> {exception.InnerException}");
                throw;
            }

            _logger.LogInformation("[AlunoRepository]-[Atualiza] -> [Finish]");
        }

        public async Task<IEnumerable<Aluno>> Busca(string nome)
        {
            _logger.LogInformation($"[AlunoRepository]-[Busca] -> [Start]: Nome -> {nome}");

            using var connection = _context.CreateConnection;

            const string query = "SELECT * FROM aluno WHERE nome LIKE @Nome;";
            var param = new {
                Nome = $"%{nome}%"
            };

            try
            {
                var result = await connection.QueryAsync<Aluno>(query, param);

                _logger.LogInformation("[AlunoRepository]-[Busca] -> [Finish]");
                return result;
            }
            catch (Exception exception)
            {
                _logger.LogError($"[AlunoRepository]-[Busca] -> [Exception]: Message -> {exception.Message}");
                _logger.LogError($"[AlunoRepository]-[Busca] -> [InnerException]: Message -> {exception.InnerException}");
                throw;
            }
        }

        public async Task RegistraNotas(AlunoNotasViewModel viewModel)
        {
            _logger.LogInformation($"[AlunoRepository]-[RegistraNotas] -> [Start]: Payload -> {JsonSerializer.Serialize(viewModel)}");

            using var connection = _context.CreateConnection;

            var query = new StringBuilder("INSERT INTO NotaAluno(AlunoId, Nota) VALUES ");
            var parameters = new DynamicParameters();
            var paramIndex = 0;

            foreach(var nota in viewModel.Notas)
            {
                query.Append($"(@AlunoId{paramIndex}, @Nota{paramIndex}), ");
                parameters.Add($"AlunoId{paramIndex}", viewModel.AlunoId);
                parameters.Add($"Nota{paramIndex}", nota);
                paramIndex++;
            }

            query.Length -= 2;

            try
            {
                await connection.ExecuteAsync(query.ToString(), parameters);
            }
            catch (Exception exception)
            {
                _logger.LogError($"[AlunoRepository]-[RegistraNotas] -> [Exception]: Message -> {exception.Message}");
                _logger.LogError($"[AlunoRepository]-[RegistraNotas] -> [InnerException]: Message -> {exception.InnerException}");
                throw;
            }

            _logger.LogInformation("[AlunoRepository]-[RegistraNotas] -> [Finish]");
        }

        public async Task<AlunoNotasViewModel> BuscaNotas(string matricula)
        {
            _logger.LogInformation($"[AlunoRepository]-[BuscaNotas] -> [Start]: Matricula -> {matricula}");

            using var connection = _context.CreateConnection;

            const string query = "SELECT * FROM NotaAluno WHERE AlunoId = (SELECT Id FROM aluno WHERE Matricula = @Matricula)";

            var param = new
            {
                Matricula = matricula
            };

            try
            {
                var result = await connection.QueryAsync<NotaAluno>(query, param);

                if(!result.Any())
                {
                    _logger.LogWarning("[AlunoRepository]-[BuscaNotas] -> [NotFound]: Nenhuma nota encontrada");
                    return new AlunoNotasViewModel
                    {
                        Notas = new List<double> { 0 }
                    };
                }

                var viewModel = new AlunoNotasViewModel
                {
                    AlunoId = result.FirstOrDefault()!.AlunoId,
                    Notas = result.Select(nota => nota.Nota)
                };

                _logger.LogInformation("[AlunoRepository]-[BuscaNotas] -> [Finish]");
                return viewModel;
            }
            catch (Exception exception)
            {
                _logger.LogError($"[AlunoRepository]-[BuscaNotas] -> [Exception]: Message -> {exception.Message}");
                _logger.LogError($"[AlunoRepository]-[BuscaNotas] -> [InnerException]: Message -> {exception.InnerException}");
                throw;
            }
        }

        public async Task<dynamic> BuscaAlunoNotas(string matricula)
        {
            _logger.LogInformation($"[AlunoRepository]-[BuscaAlunoNotas] -> [Start]: Matricula -> {matricula}");

            using var connection = _context.CreateConnection;

            const string query = "SELECT * FROM view_aluno_notas WHERE Matricula = @Matricula";

            var param = new
            {
                Matricula = matricula
            };

            try
            {
                var result = await connection.QueryAsync<AlunoNotaBuscaViewModel>(query, param);


                var viewModel = new
                {
                    result.FirstOrDefault()!.Nome,
                    result.FirstOrDefault()!.Matricula,
                    result.FirstOrDefault()!.DataNascimento,
                    Notas = result.Select(nota => nota.Nota)
                };

                _logger.LogInformation("[AlunoRepository]-[BuscaAlunoNotas] -> [Finish]");
                return viewModel;
            }
            catch (Exception exception)
            {
                _logger.LogError($"[AlunoRepository]-[BuscaAlunoNotas] -> [Exception]: Message -> {exception.Message}");
                _logger.LogError($"[AlunoRepository]-[BuscaAlunoNotas] -> [InnerException]: Message -> {exception.InnerException}");
                throw;
            }
        }
    }
}