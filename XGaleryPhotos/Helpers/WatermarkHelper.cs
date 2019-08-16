using System;
namespace XGaleryPhotos.Helpers
{
    public static class WatermarkHelper
    {
        public static string ArmaWatermark()
        {
            string watermark = string.Empty;
            if(Globals.IncluirPrefijoEnWatermark)
                watermark += Globals.PrefijoWatermark;
            if (Globals.IncluirFechaEnWatermark)
                watermark += DateTime.Now.ToString("dd/MM/yy");
            if (Globals.IncluirHoraEnWatermark)
                watermark += $" {DateTime.Now.ToString("HH:mm:ss")}";
            return watermark;
        }
    }
}
