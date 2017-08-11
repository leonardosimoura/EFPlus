using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Z.EntityFramework.Plus;

namespace EFPlus
{
    public class Test
    {
        private readonly ITestOutputHelper output;
        public Test(ITestOutputHelper output)
        {
            this.output = output;
            using (var context = new DBConn())
            {
                if (!context.Categorias.Any())
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        context.Categorias.Add(new Categoria()
                        {
                            CategoriaId = Guid.NewGuid(),
                            Nome = "Categoria " + i
                        });

                        context.Tipos.Add(new Tipo()
                        {
                            TipoId = Guid.NewGuid(),
                            Nome = "Tipo " + i
                        });

                        context.Fechamentos.Add(new Fechamento()
                        {
                            FechamentoId = Guid.NewGuid(),
                            DataInicio = DateTime.Now.AddMonths(i),
                            DataFim = DateTime.Now.AddMonths(i + 1)
                        });

                        context.SaveChanges();
                    }
                    
                }
            }
        }

        [Fact]
        public void EFPlus()
        {
            using (var context = new DBConn())
            {
                var dataInicioProcesso = DateTime.Now;

                var categoriasQuery = context.Categorias.Future();
                var tiposQuery = context.Tipos.Future();
                var fechamentosQuery = context.Fechamentos.Future();

                var futureFirstFechamento = context.Fechamentos.DeferredFirstOrDefault().FutureValue();
                var futureFechamentoCount = context.Fechamentos.DeferredCount().FutureValue();
                var futureMaxDataFimFechamento = context.Fechamentos.DeferredMax(w => w.DataFim).FutureValue<DateTime>();

                var categorias = categoriasQuery.ToList();
                var tipos = tiposQuery.ToList();
                var fechamentos = fechamentosQuery.ToList();
                var firstFechamento = futureFirstFechamento.Value;
                var fechamentoCount = futureFechamentoCount.Value;
                var MaxDataFimFechamento = futureMaxDataFimFechamento.Value;
                var dataFimProcesso = DateTime.Now;

                var tempoTotal = (dataFimProcesso - dataInicioProcesso).TotalMilliseconds;
                output.WriteLine(tempoTotal.ToString());
            }
        }

        [Fact]
        public void EF()
        {
            using (var context = new DBConn())
            {
                var dataInicioProcesso = DateTime.Now;

                var categorias = context.Categorias.ToList();
                var tipos = context.Tipos.ToList();
                var fechamentos = context.Fechamentos.ToList();
                var firstFechamento = context.Fechamentos.FirstOrDefault();
                var fechamentoCount = context.Fechamentos.Count();
                var MaxDataFimFechamento = context.Fechamentos.Max(w => w.DataFim);

                var dataFimProcesso = DateTime.Now;
                var tempoTotal = (dataFimProcesso - dataInicioProcesso).TotalMilliseconds;
                output.WriteLine(tempoTotal.ToString());
            }
        }

    }
}
