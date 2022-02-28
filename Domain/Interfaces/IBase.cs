namespace Domain.Interfaces
{
    public interface IBase<T>
    {
        List<T> Lista();
        T RetornaPorId(int id);
        bool Insere(T entidade);
        bool Exclui(int id);
        bool Atualiza(int id, T entidade);
        int ProximoId();
    }
}
