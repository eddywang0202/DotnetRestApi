namespace FourtitudeAsiaTest.Loggers
{
    public interface IAppLogger<T>
    {
        void LogInfo(string message);
        void LogDebug(string message);
        void LogWarning(string message);
        void LogError(string message, Exception ex);
    }
}
