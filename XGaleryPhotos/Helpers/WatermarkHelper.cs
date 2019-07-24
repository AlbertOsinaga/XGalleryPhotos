using System;
namespace XGaleryPhotos.Helpers
{
    public static class WatermarkHelper
    {
        public static string ArmaWatermark()
        {
            string watermark = string.Empty;
            if(App.IncluirPrefijoEnWatermark)
                watermark += App.PrefijoWatermark;
            if (App.IncluirFechaEnWatermark)
                watermark += DateTime.Now.ToShortDateString();
            if (App.IncluirHoraEnWatermark)
                watermark += $" {DateTime.Now.ToShortTimeString()}";
            return watermark;
        }
    }
}
