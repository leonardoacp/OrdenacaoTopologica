using static ordenacao_topologica.AlgoritmoKahn;

namespace ordenacao_topologica.Interfaces
{
    public interface IAlgoritmoKahn
    {
        string OrdenacaoTopologica(OrderVertice orderVertice);
        void Get();
    }
}
