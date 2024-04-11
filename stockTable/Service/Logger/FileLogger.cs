namespace stockTable.Service.Logger
{
    public class FileLogger : ILogger, IDisposable
    {
        string filePath;
        string nameLogger;
        static object _lock = new object();
        public FileLogger(string path,string nameLogger)
        {
            filePath = path;
            this.nameLogger = nameLogger;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return this;
        }

        public void Dispose()
        {
            
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            lock(_lock)
            {
                File.AppendAllText(filePath+nameLogger+".txt", formatter(state, exception) + Environment.NewLine);
            }
        }
    }
}
