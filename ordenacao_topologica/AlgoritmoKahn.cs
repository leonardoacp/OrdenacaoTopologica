using System;
using System.Collections.Generic;
using System.Linq;
using ordenacao_topologica.Interfaces;

namespace ordenacao_topologica
{

    public class AlgoritmoKahn : IAlgoritmoKahn
    {
        public enum OrderVertice
        {
            Ascending,
            Descending,
            None
        }

        private List<object> OrderObjects(OrderVertice orderVertice, List<object> objects)
        {
            return orderVertice == OrderVertice.Ascending ? objects.OrderBy(a => a).ToList() :
                   orderVertice == OrderVertice.Descending ? objects.OrderByDescending(a => a).ToList() : objects;
        }

        private  List<object> VerticesPorMatriz(List<MatrizAdjacencia> matriz)
        {
            var inicio = matriz.Select(a => a.Inicio);
            var fim = matriz.Select(a => a.Fim);
            return (inicio.Concat(fim)).GroupBy(a => a).Select(a => a.FirstOrDefault()).ToList();
        }

        private  List<object> VerticeSemArestaDeEntrada(List<MatrizAdjacencia> matriz)
        {
            var vertices = VerticesPorMatriz(matriz);
            return (vertices.Where(n => matriz.All(e => e.Fim.Equals(n) == false))).ToList();
            //return new HashSet<object>(vertices.Where(n => matriz.All(e => e.Fim.Equals(n) == false)));
        }

        public string OrdenacaoTopologica(OrderVertice orderVertice)
        {

            var matrizAdjacencia = new List<MatrizAdjacencia> {
                new MatrizAdjacencia{ Inicio = 7, Fim = 11 },
                new MatrizAdjacencia{ Inicio = 7, Fim = 8 },
                new MatrizAdjacencia{ Inicio = 5, Fim = 11 },
                new MatrizAdjacencia{ Inicio = 3, Fim = 8 },
                new MatrizAdjacencia{ Inicio = 3, Fim = 10 },
                new MatrizAdjacencia{ Inicio = 11, Fim = 2 },
                new MatrizAdjacencia{ Inicio = 11, Fim = 9 },
                new MatrizAdjacencia{ Inicio = 11, Fim = 10 },
                new MatrizAdjacencia{ Inicio = 8, Fim = 9 }};


            var elementosOrdenados = new List<object>();
            var verticesSemArestaDeEntrada = VerticeSemArestaDeEntrada(matrizAdjacencia);
            verticesSemArestaDeEntrada = OrderObjects(orderVertice, verticesSemArestaDeEntrada);


            while (verticesSemArestaDeEntrada.Any())
            {
                var n = verticesSemArestaDeEntrada.First();
                verticesSemArestaDeEntrada.Remove(n);
                elementosOrdenados.Add(n);


                foreach (var e in matrizAdjacencia.Where(e => e.Inicio.Equals(n)).ToList())
                {
                    var m = e.Fim;
                    matrizAdjacencia.Remove(e);

                    if (matrizAdjacencia.All(me => me.Fim.Equals(m) == false))
                    {
                        verticesSemArestaDeEntrada.Add(m);
                        verticesSemArestaDeEntrada = OrderObjects(orderVertice, verticesSemArestaDeEntrada);
                    }
                }
            }

            if (matrizAdjacencia.Any())
                return null;

            return string.Join(" -> ", elementosOrdenados);
        }


        public void Get()
        {
            var orderVertices = Enum.GetValues(typeof(OrderVertice)).Cast<OrderVertice>();

            foreach (var orderVertice in orderVertices)
            {
                Console.Write(string.Concat(orderVertice.ToString(), ": "));
                Console.Write(OrdenacaoTopologica(orderVertice));
                Console.WriteLine("\n");
            }

            Console.ReadLine();
        }

    }
}