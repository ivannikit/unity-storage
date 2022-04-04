#nullable enable
using System.IO;

namespace TeamZero.StorageSystem
{
    public interface IStreamDatabase<in TAddress>
    {
        bool CreatePullStream(TAddress address, out Stream stream);
        bool CreatePushStream(TAddress address, out Stream stream);
    }
}
