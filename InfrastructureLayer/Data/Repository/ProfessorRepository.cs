using ApplicationLayer;
using DomainLayer.Interfaces.Repository;
using DomainLayer.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Data.Repository
{
    public class ProfessorRepository : IProfessorRepository
    {

        private static List<Professor> _professores = default!;

        public ProfessorRepository()
        {
            _professores = new List<Professor>();
        }

        /// <summary>
        /// Método responsável por salvar os dados de um professor
        /// </summary>
        /// <param name="professor"></param>
        /// <returns>professor</returns>
        public Professor Registra(Professor professor)
        {
            // gero o ID
            professor.Id = Guid.NewGuid();

            // persisto o professor no nosso array estático
            Persiste(professor);

            // retorno o professor cadastro com o GUID
            return professor;
        }

        public IEnumerable<Professor> Lista()
        {
            return _professores;
        }

        public IEnumerable<Professor> Busca(string nome)
        {
            /*return _professores.Find(professor => professor.Nome.Equals(
                value: nome, 
                comparisonType: StringComparison.InvariantCultureIgnoreCase)
            )!;*/

            return _professores.FindAll(prof => prof.Nome.ToLower().Contains(nome.ToLower()));
        }

        public Professor Atualiza(Professor professor)
        {
            var prof = _professores.Find(professor => professor.Id.ToString().Equals(professor.Id.ToString()))!;

            var idx = _professores.IndexOf(prof);

            _professores[idx].Nome = professor.Nome;
            _professores[idx].Conhecimentos = professor.Conhecimentos;

            return professor;
        }

        public void Apaga(Guid id)
        {
            var professor = _professores.Find(prof => prof.Id == id);

            if (professor != null)
            {
                _professores.Remove(professor);
            }
        }

        // persistência em memória
        private static void Persiste(Professor professor)
        {
            _professores.Add(professor);
        }
    }
}
