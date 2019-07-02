using System.Collections.Generic;
using System.Linq;

using XGaleryPhotos.Interfaces;
using XGaleryPhotos.Models;

namespace XGaleryPhotos.Repositories
{
    public class MockRepository : IRepository
    {
        public static List<Flujo> Flujos { get; set; }

        public MockRepository()
        {
            Flujos = new List<Flujo>();
            Flujos.Add(new Flujo { FlujoId = 1, FlujoNro = "123456R", Cliente = "Mario Bross", Placa = "3456DFG" });
            Flujos.Add(new Flujo { FlujoId = 2, FlujoNro = "234567R", Cliente = "Eusebio Ramírez", Placa = "456RUP" });
            Flujos.Add(new Flujo { FlujoId = 3, FlujoNro = "345678R", Cliente = "Miguel Tardío", Placa = "1050JBL" });
            Flujos.Add(new Flujo { FlujoId = 4, FlujoNro = "456789R", Cliente = "Pedro Infante", Placa = "270LPC" });
            Flujos.Add(new Flujo { FlujoId = 5, FlujoNro = "567890R", Cliente = "Johnny Bravo", Placa = "2034ZBS" });
        }

        public Flujo GetFlujoByNro(string flujoNro)
        {
            Flujo flujo = (from f in Flujos where f.FlujoNro == flujoNro select f).FirstOrDefault();
            return flujo;
        }
    }
}
