namespace stockTable.Interfaces
{
    public interface IBarCodeService
    {
        int[] GetImage(string code);
    }
}
