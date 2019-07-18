using System;
using Xunit;
using XGaleryPhotos.Interfaces;
using XGaleryPhotos.Models;
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
            User user = null;
            try
            {
                user = service.Authenticate("nnn", "Mensaje1");
            }
            catch (Exception ex)
            {
                Assert.Null(ex);
            }

            // Prueba
            Assert.NotNull(user);
            Assert.Equal("0", user.CodigoEstado);
        }

        [Fact]
        public void TestAuthenticate_BadUsuario2()
        {
            // Prepara
            IAuthenticateService service = new AuthenticateService();

            // Ejecuta
            User user = null;
            try
            {
                user = service.Authenticate("siles", "Mensaje1");
            }
            catch (Exception ex)
            {
                Assert.Null(ex);
            }

            // Prueba
            Assert.NotNull(user);
            Assert.Equal("0", user.CodigoEstado);
        }

        [Fact]
        public void TestAuthenticate_BadPassword()
        {
            // Prepara
            IAuthenticateService service = new AuthenticateService();

            // Ejecuta
            User user = null;
            try
            {
                user = service.Authenticate("WSiles", "");
            }
            catch (Exception ex)
            {
                Assert.Null(ex);
            }

            // Prueba
            Assert.NotNull(user);
            Assert.Equal("0", user.CodigoEstado);
        }

        [Fact]
        public void TestAuthenticate_user1()
        {
            // Prepara
            IAuthenticateService service = new AuthenticateService();

            // Ejecuta
            User user = null;
            try
            {
                user = service.Authenticate("WSiles", "Mensaje1");
            }
            catch (Exception ex)
            {
                Assert.Null(ex);
            }

            // Prueba
            Assert.NotNull(user);
            Assert.Equal("1", user.CodigoEstado);
        }

        [Fact]
        public void TestAuthenticate_user2()
        {
            // Prepara
            IAuthenticateService service = new AuthenticateService();

            // Ejecuta
            User user = null;
            try
            {
                user = service.Authenticate("wsiles", "Mensaje1");
            }
            catch (Exception ex)
            {
                Assert.Null(ex);
            }

            // Prueba
            Assert.NotNull(user);
            Assert.Equal("1", user.CodigoEstado);
        }

        [Fact]
        public void TestAuthenticate_user3()
        {
            // Prepara
            IAuthenticateService service = new AuthenticateService();

            // Ejecuta
            User user = null;
            try
            {
                user = service.Authenticate("CPalenque", "Mensaje1");
            }
            catch (Exception ex)
            {
                Assert.Null(ex);
            }

            // Prueba
            Assert.NotNull(user);
            Assert.Equal("1", user.CodigoEstado);
        }

        [Fact]
        public void TestAuthenticate_user4()
        {
            // Prepara
            IAuthenticateService service = new AuthenticateService();

            // Ejecuta
            User user = null;
            try
            {
                user = service.Authenticate("cpalenque", "Mensaje1");
            }
            catch (Exception ex)
            {
                Assert.Null(ex);
            }

            // Prueba
            Assert.NotNull(user);
            Assert.Equal("1", user.CodigoEstado);
        }
    }
}
