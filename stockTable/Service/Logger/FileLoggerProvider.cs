namespace stockTable.Service.Logger
{
    public class FileLoggerProvider : ILoggerProvider
    {
        string path;
        public FileLoggerProvider(string path)
        {
            this.path = path;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(path,categoryName);
        }

        public void Dispose()
        {
            
        }
    }
}
