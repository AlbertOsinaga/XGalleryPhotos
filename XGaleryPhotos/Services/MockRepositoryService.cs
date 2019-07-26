using System;
using System.Collections.Generic;
using System.Linq;
using XGaleryPhotos.Models;

namespace XGaleryPhotos.Interfaces
{
    public class MockRepositoryService : IRepositoryService
    {
        public static List<Flujo> Flujos { get; set; }
        public static User User { get; set; }
        public static MediaFile MediaFile { get; set; }
        public static List<User> Users { get; set; }

        public MockRepositoryService()
        {
            Flujos = new List<Flujo>();
            Flujos.Add(new Flujo { FlujoId = 1, FlujoNro = "123456R", Cliente = "Mario Bross", Placa = "3456DFG" });
            Flujos.Add(new Flujo { FlujoId = 2, FlujoNro = "234567R", Cliente = "Eusebio Ramírez", Placa = "456RUP" });
            Flujos.Add(new Flujo { FlujoId = 3, FlujoNro = "345678R", Cliente = "Miguel Tardío", Placa = "1050JBL" });
            Flujos.Add(new Flujo { FlujoId = 4, FlujoNro = "456789R", Cliente = "Pedro Infante", Placa = "270LPC" });
            Flujos.Add(new Flujo { FlujoId = 5, FlujoNro = "567890R", Cliente = "Johnny Bravo", Placa = "2034ZBS" });

            Users = new List<User>();
            Users.Add(new User {Id = 1, UserName = "juan", Password = "1234" });
            Users.Add(new User { Id = 2, UserName = "maria", Password = "4321" });
            Users.Add(new User { Id = 3, UserName = "felipe", Password = "5678" });

            AddMediaFile(new MediaFile());
        }

        public Flujo GetFlujoByNro(string flujoNro)
        {
            Flujo flujo = (from f in Flujos where f.FlujoNro == flujoNro select f).FirstOrDefault();
            return flujo;
        }

        public void AddUser(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return;
            Users.Add(new User { Id = Users.Count + 1, UserName = "felipe", Password = "5678" });
        }

        public void DeleteUser(int userId)
        {
            User userDel = Users.Find((u) => u.Id == userId);
            if (userDel != null)
                Users.Remove(userDel);
        }

        public User GetUser(string username, string password = null)
        {
            User userLog = null;
            if(password == null)
                userLog = Users.Find((u) => u.UserName == username);
            else
                userLog = Users.Find((u) => u.UserName == username && u.Password == password);
            return userLog;
        }

        public void AddMediaFile(MediaFile mediaFile)
        {
            DeleteMediaFile();
            MockRepositoryService.MediaFile = mediaFile;
        }

        public void DeleteMediaFile()
        {
            MockRepositoryService.MediaFile = null;
            GC.Collect();
        }

        public MediaFile GetMediaFile()
        {
            return MockRepositoryService.MediaFile;
        }

        public Respuesta UpdateFotos(Flujo flujo, string usuarioSistema)
        {
            throw new NotImplementedException();
        }
    }
}
