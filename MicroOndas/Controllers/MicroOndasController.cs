using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MicroOndas.Models;

namespace MicroOndas.Controllers
{
    public class MicroOndasController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ResultadoTempoMicroOndas(ViewParametrosEntradaMicroOndas parametrosEntradaMicroOndas)
        {
            List<ProgramasPreDefinidos> listProgramas = CarregaProgramasPreDefinidos();
            if (parametrosEntradaMicroOndas.parametrosEntradas != null)
            {
                MicroOndas.Models.MicroOndas microOndas = new MicroOndas.Models.MicroOndas(0, 0);

                string[] parametros = parametrosEntradaMicroOndas.parametrosEntradas.Split(',');

                if (parametros.Count() == 2)
                {
                    parametrosEntradaMicroOndas.caractere = '.';
                    try
                    {
                        microOndas.setTempo(parametros[0]);
                    }
                    catch (Exception e)
                    {
                        parametrosEntradaMicroOndas.erro = "erro";
                        parametrosEntradaMicroOndas.msgerro = e.Message;
                    }

                    try
                    {
                        microOndas.setPotencia(parametros[1]);
                        parametrosEntradaMicroOndas.potencia = microOndas.getPotencia();
                    }
                    catch (Exception e)
                    {
                        parametrosEntradaMicroOndas.erro = "erro";
                        parametrosEntradaMicroOndas.msgerro = e.Message;
                    }
                }
                else if(listProgramas.Exists(m => m.nome == parametrosEntradaMicroOndas.parametrosEntradas.Trim()))
                {
                    var item = listProgramas.First(m => m.nome == parametrosEntradaMicroOndas.parametrosEntradas.Trim());
                    parametrosEntradaMicroOndas.potencia = item.potencia;
                    parametrosEntradaMicroOndas.caractere = item.caractere;
                }
                else
                {
                    parametrosEntradaMicroOndas.erro = "erro";
                    parametrosEntradaMicroOndas.msgerro = "Deve ser informado o tempo de cozimento e a potência separado por virgula. Ex: 1,10. Ou um programa pré-definido";
                }
            }
            else
            {
                parametrosEntradaMicroOndas.erro = "erro";
                parametrosEntradaMicroOndas.msgerro = "Informe um parâmetro de entrada válido";
            }

            return Json(parametrosEntradaMicroOndas);
        }

        [HttpGet]
        public JsonResult ListaProgramasPreDefinidos()
        {
            List<ProgramasPreDefinidos> listProgramas = CarregaProgramasPreDefinidos();
            return Json(listProgramas, JsonRequestBehavior.AllowGet);
        }

        private List<ProgramasPreDefinidos> CarregaProgramasPreDefinidos()
        {
            List<ProgramasPreDefinidos> listProgramas = new List<ProgramasPreDefinidos>();

            listProgramas.Add(new ProgramasPreDefinidos("pipoca", "Instruçãoes para aquecimento da pipoca", '*', 10, 3));
            listProgramas.Add(new ProgramasPreDefinidos("frango", "Instruçãoes para aquecimento e descongelamento do frango", '#', 100, 8));
            listProgramas.Add(new ProgramasPreDefinidos("carne", "Instruçãoes para aquecimento e descongelamento da carne", '@', 110, 10));
            listProgramas.Add(new ProgramasPreDefinidos("macarrao", "Instrução do aquecimento do macarrão", '|', 60, 1));
            listProgramas.Add(new ProgramasPreDefinidos("arroz", "Instrução do aquecimento do arroz", '!', 50, 6));

            return listProgramas;

        }
    }
}