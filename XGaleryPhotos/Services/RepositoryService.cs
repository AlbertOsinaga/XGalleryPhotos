using System;
using System.Collections.Generic;
using System.Linq;

using XGaleryPhotos.Interfaces;
using XGaleryPhotos.Models;

namespace XGaleryPhotos.Interfaces
{
    public class RepositoryService : IRepositoryService
    {
        public static List<Flujo> Flujos { get; set; }
        public static User User { get; set; }
        public static MediaFile MediaFile { get; set; }

        public RepositoryService()
        {
            Flujos = new List<Flujo>();
            Flujos.Add(new Flujo { FlujoId = 1, FlujoNro = "123456R", Cliente = "Mario Bross", Placa = "3456DFG" });
            Flujos.Add(new Flujo { FlujoId = 2, FlujoNro = "234567R", Cliente = "Eusebio Ramírez", Placa = "456RUP" });
            Flujos.Add(new Flujo { FlujoId = 3, FlujoNro = "345678R", Cliente = "Miguel Tardío", Placa = "1050JBL" });
            Flujos.Add(new Flujo { FlujoId = 4, FlujoNro = "456789R", Cliente = "Pedro Infante", Placa = "270LPC" });
            Flujos.Add(new Flujo { FlujoId = 5, FlujoNro = "567890R", Cliente = "Johnny Bravo", Placa = "2034ZBS" });

            AddMediaFile(new MediaFile());
        }

        public Flujo GetFlujoByNro(string flujoNro)
        {
            Flujo flujo = (from f in Flujos where f.FlujoNro == flujoNro select f).FirstOrDefault();
            return flujo;
        }

        public void AddUser(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return;

            DeleteUser();
            User = new User
            {
                Id = 1,
                UserName = username
            };
        }

        public void DeleteUser()
        {
            User = null;
            GC.Collect();
        }

        public User GetUser()
        {
            return User;
        }

        public void AddMediaFile(MediaFile mediaFile)
        {
            DeleteMediaFile();
            RepositoryService.MediaFile = mediaFile;
        }

        public void DeleteMediaFile()
        {
            RepositoryService.MediaFile = null;
            GC.Collect();
        }

        public MediaFile GetMediaFile()
        {
            return RepositoryService.MediaFile;
        }
    }
}
