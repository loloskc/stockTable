namespace stockTable.Service
{
    public class ServiceFolder
    {
        public string GetFolder(string path,string name)
        {
            if (!FolderExists(path, name))
            {
                Directory.CreateDirectory(path+name);
                return name + "/";
            }
            else
            {
                return name + "/";
            }
        }

        public bool FolderExists(string path,string name)
        {
            
            var current = Directory.GetCurrentDirectory();
            return Directory.Exists(path+ GetNormalParhFolder(name));
        }

        private string GetNormalParhFolder(string name)
        {
            return name.Insert(0, "\\");
        }

    }
}
