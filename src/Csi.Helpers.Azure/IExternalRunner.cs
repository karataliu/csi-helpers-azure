using System.Threading.Tasks;

namespace Csi.Helpers.Azure
{
    public interface IExternalRunner
    {
        Task<int> RunExecutable(string path, params string[] arguments);
    }
}
