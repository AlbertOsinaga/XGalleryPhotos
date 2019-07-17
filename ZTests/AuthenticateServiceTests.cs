using System;
using Xunit;
using XGaleryPhotos.Interfaces;
using XGaleryPhotos.Services;

namespace ZTests
{
    public class AuthenticateServiceTests
    {
        [Fact]
        public void TestAuthenticate_BadUsuario1()
        {
            // Prepara
            IAuthenticateService service = new AuthenticateService();

            // Ejecuta
            bool Ok = true;
            try
            {
                Ok = service.Authenticate("nnn", "Mensaje1");
            }
            catch (Exception ex)
            {
                Assert.Null(ex);
            }

            // Prueba
            Assert.False(Ok);
        }

        [Fact]
        public void TestAuthenticate_BadUsuario2()
        {
            // Prepara
            IAuthenticateService service = new AuthenticateService();

            // Ejecuta
            bool Ok = true;
            try
            {
                Ok = service.Authenticate("siles", "Mensaje1");
            }
            catch (Exception ex)
            {
                Assert.Null(ex);
            }

            // Prueba
            Assert.False(Ok);
        }

        [Fact]
        public void TestAuthenticate_BadPassword()
        {
            // Prepara
            IAuthenticateService service = new AuthenticateService();

            // Ejecuta
            bool Ok = true;
            try
            {
                Ok = service.Authenticate("WSiles", "");
            }
            catch (Exception ex)
            {
                Assert.Null(ex);
            }

            // Prueba
            Assert.False(Ok);
        }

        [Fact]
        public void TestAuthenticate_Ok1()
        {
            // Prepara
            IAuthenticateService service = new AuthenticateService();

            // Ejecuta
            bool Ok = false;
            try
            {
                Ok = service.Authenticate("WSiles", "Mensaje1");
            }
            catch (Exception ex)
            {
                Assert.Null(ex);
            }

            // Prueba
            Assert.True(Ok);
        }

        [Fact]
        public void TestAuthenticate_Ok2()
        {
            // Prepara
            IAuthenticateService service = new AuthenticateService();

            // Ejecuta
            bool Ok = false;
            try
            {
                Ok = service.Authenticate("wsiles", "Mensaje1");
            }
            catch (Exception ex)
            {
                Assert.Null(ex);
            }

            // Prueba
            Assert.True(Ok);
        }

        [Fact]
        public void TestAuthenticate_Ok3()
        {
            // Prepara
            IAuthenticateService service = new AuthenticateService();

            // Ejecuta
            bool Ok = false;
            try
            {
                Ok = service.Authenticate("CPalenque", "Mensaje1");
            }
            catch (Exception ex)
            {
                Assert.Null(ex);
            }

            // Prueba
            Assert.True(Ok);
        }

        [Fact]
        public void TestAuthenticate_Ok4()
        {
            // Prepara
            IAuthenticateService service = new AuthenticateService();

            // Ejecuta
            bool Ok = false;
            try
            {
                Ok = service.Authenticate("cpalenque", "Mensaje1");
            }
            catch (Exception ex)
            {
                Assert.Null(ex);
            }

            // Prueba
            Assert.True(Ok);
        }
    }
}
