using BarcodeStandard;
using SkiaSharp;
using stockTable.Interfaces;

namespace stockTable.Service
{
    public class BarCodeService : IBarCodeService
    {
        private readonly Barcode _barcode;
        private readonly BarcodeStandard.Type _type;
        private const int width = 600;
        private const int height = 300;
        public BarCodeService()
        {
            _barcode = new Barcode();
            _type = BarcodeStandard.Type.Code128;
            _barcode.Width = width;
            _barcode.Height = height;
            _barcode.IncludeLabel = true;
            _barcode.Alignment = AlignmentPositions.Center;
        }

        public BarCodeService(int width,int height)
        {
            _barcode = new Barcode();
            _barcode.Width = width;
            _barcode.Height = height;
            _type = BarcodeStandard.Type.Code128;
            _barcode.IncludeLabel= true;
            _barcode.Alignment = AlignmentPositions.Center;
        }

        public BarCodeService(int width,int height, BarcodeStandard.Type type)
        {
            _barcode = new Barcode();
            _type = type;
            _barcode.Width=width;
            _barcode.Height = height;
            _barcode.IncludeLabel = true;
            _barcode.Alignment = AlignmentPositions.Center;
        }

        public int[] GetImage(string code)
        {
            var span = _barcode.Encode(_type, code).Encode().AsSpan();
            int[] array = new int[span.ToArray().Length];
            for(int i =0;i < span.ToArray().Length; i++)
            {
                array[i] = span.ToArray()[i];
            }
            return array;
        }
    }
}
