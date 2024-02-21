namespace stockTable.Interfaces
{
    public interface IBarCodeService
    {
        string GetPathImage(string code);
        void UploadImage(Stream stream);
        void CreateBarCode(string code);
    }
}
