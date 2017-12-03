using ordenacao_topologica.Interfaces;

namespace ordenacao_topologica
{
    
    public class Program
    {
        private readonly IAlgoritmoKahn algoritmoKahn;

        public Program() => algoritmoKahn = new AlgoritmoKahn();

        static void Main()
        {

            var program = new Program();
            program.GetKahn();
        }

        public void GetKahn()
        {
            algoritmoKahn.Get();
        }
    }
}