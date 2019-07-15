using System;
using Xunit;
using XGaleryPhotos.Interfaces;
using XGaleryPhotos.Models;
using XGaleryPhotos.Services;

namespace ZTests
{
    public class FotosUpdateTests
    {
        [Fact]
        public void TestFotosUpdate_NoFotos()
        {
            // Prepara
            IRepositoryService service = new RepositoryService();

            // Ejecuta
            Flujo flujo = new Flujo
            {
                FlujoNro = "280R",
                TipoDocumento = "RE - RC Atencion",
                DocumentoNro = 4,
                Fotos = null
            };

            try
            {
                bool estadoValido = service.UpdateFotos(flujo, "LeVargas");
            }
            catch (Exception ex)
            {
                Assert.Null(ex);
            }

            // Prueba
            Assert.False(flujo.EsValido);
        }
    }
}

