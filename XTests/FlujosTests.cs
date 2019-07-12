using System;
using Xunit;
using XGaleryPhotos.Interfaces;
using XGaleryPhotos.Models;
using XGaleryPhotos.Services;

namespace ZTests
{
    public class FlujosTests
    {
        [Fact]
        public void TestFlujo_BadFlujo1()
        {
            // Prepara
            IRepositoryService service = new RepositoryService();

            // Ejecuta
            Flujo flujo = null;
            try
            {
                flujo = service.GetFlujoByNro("280R");
            }
            catch (Exception ex)
            {
                Assert.Null(ex);
            }

            // Prueba
            Assert.Null(flujo);
        }
    }
}
