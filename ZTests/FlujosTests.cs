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
                flujo = service.GetFlujoByNro("999R");
            }
            catch (Exception ex)
            {
                Assert.Null(ex);
            }

            // Prueba
            Assert.NotNull(flujo);
            Assert.False(flujo.EsValido);
        }

        [Fact]
        public void TestFlujo_FlujoOk1()
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
            Assert.NotNull(flujo);
            Assert.True(flujo.EsValido);
            Assert.Equal("280R", flujo.FlujoNro);
            Assert.Equal("ANZOATEGUI VACA WILFREDO", flujo.Cliente);
            Assert.Equal("ET", flujo.Placa);
        }

        [Fact]
        public void TestFlujo_FlujoOk2()
        {
            // Prepara
            IRepositoryService service = new RepositoryService();

            // Ejecuta
            Flujo flujo = null;
            try
            {
                flujo = service.GetFlujoByNro("281R");
            }
            catch (Exception ex)
            {
                Assert.Null(ex);
            }

            // Prueba
            Assert.NotNull(flujo);
            Assert.True(flujo.EsValido);
            Assert.Equal("281R", flujo.FlujoNro);
        }

        [Fact]
        public void TestFlujo_FlujoOk3()
        {
            // Prepara
            IRepositoryService service = new RepositoryService();

            // Ejecuta
            Flujo flujo = null;
            try
            {
                flujo = service.GetFlujoByNro("282R");
            }
            catch (Exception ex)
            {
                Assert.Null(ex);
            }

            // Prueba
            Assert.NotNull(flujo);
            Assert.True(flujo.EsValido);
            Assert.Equal("282R", flujo.FlujoNro);
            Assert.Equal("MONTENEGRO ERNST PABLO ALEJANDRO ROBERTO", flujo.Cliente);
            Assert.Equal("3467-LTC", flujo.Placa);
        }
    }
}
