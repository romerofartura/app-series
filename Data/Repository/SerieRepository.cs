using Domain.Entities;
using Domain.Interfaces;

namespace Data.Repository
{
    public class SerieRepository : ISerie
	{
		private List<Serie> listaSerie = new List<Serie>();
		public bool Atualiza(int id, Serie objeto)
		{
			listaSerie[id] = objeto;
			return true;
		}

		public bool Exclui(int id)
		{
			listaSerie[id].Excluir();
			return true;
		}

		public bool Insere(Serie objeto)
		{
			listaSerie.Add(objeto);
			return true;
		}

		public List<Serie> Lista()
		{
			return listaSerie;
		}

		public int ProximoId()
		{
			return listaSerie.Count;
		}

		public Serie RetornaPorId(int id)
		{
			return listaSerie[id];
		}
	}
}