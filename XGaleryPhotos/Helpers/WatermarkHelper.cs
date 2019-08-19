using System;
namespace XGaleryPhotos.Helpers
{
    public static class WatermarkHelper
    {
        public static string ArmaWatermark()
        {
            string watermark = string.Empty;
            if (Globals.IncluirPrefijoEnWatermark)
            {
                if(!string.IsNullOrEmpty(Globals.PrefijoWatermark))
                    watermark += Globals.PrefijoWatermark;
            }
            if (Globals.IncluirFechaEnWatermark)
            {
                if (!string.IsNullOrEmpty(watermark))
                    watermark += " ";
                watermark += DateTime.Now.ToString("dd/MM/yy");
            }
            if (Globals.IncluirHoraEnWatermark)
            {
                if (!string.IsNullOrEmpty(watermark))
                    watermark += " ";
                watermark += $" {DateTime.Now.ToString("HH:mm:ss")}";
            }
            if (Globals.IncluirNombreUsuarioEnWatermark)
            {
                if (!string.IsNullOrEmpty(watermark))
                    watermark += " ";
                watermark += Globals.AuthenticateService.AuthenticatedUser.UserName;
            }
            return watermark;
        }
    }
}
