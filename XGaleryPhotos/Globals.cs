using XGaleryPhotos.ViewModels;

namespace XGaleryPhotos
{
    public static class Globals
    {
        #region Constantes

        public const int PorcentajeCompresion = 30;
        public const string PrefijoWatermark = "LBC ";
        public const bool IncluirWatermarkEnFotosGalería = false;
        public const bool IncluirWatermarkEnFotosTomadas = true;
        public const bool IncluirFechaEnWatermark = true;
        public const bool IncluirHoraEnWatermark = true;
        public const bool IncluirPrefijoEnWatermark = true;
        public const bool DimensionesDeviceVisible = false;
        public const int FontSizeMicro = 10;

        #endregion

        #region Objetos

        public static FlujoViewModel FlujoViewModel { get; set; }

        #endregion
    }
}
