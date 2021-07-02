namespace SPNR.Core.Services.Python
{
    public interface IPythonService
    {
        void Initialize();
        string Exec(string script, string arguments);
    }
}