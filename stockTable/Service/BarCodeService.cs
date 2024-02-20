using BarcodeStandard;
using SkiaSharp;
using stockTable.Interfaces;


namespace stockTable.Service
{
    public class BarCodeService : IBarCodeService
    {
        private readonly Barcode _barcode;

        public BarCodeService()
        {
            _barcode = new Barcode();
        }

        public string GetPathImage(string code)
        {
            return string.Empty;
        }
    }
}
