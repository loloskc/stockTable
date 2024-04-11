namespace stockTable.Service
{
    public class ServiceFolder
    {
        public string GetFolder()
        {
            return "";
        }

        public bool FolderExists(string path,string name)
        {
            var current = Directory.GetCurrentDirectory();
            return Directory.Exists(path+name);
        }

    }
}
