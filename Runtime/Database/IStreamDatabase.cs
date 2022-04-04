#nullable enable
using System;
using System.IO;

namespace TeamZero.StorageSystem
{
    /*public class PushTransaction : IDisposable
    {
        public Stream Stream() => _stream;
        private readonly Stream _stream;

        private readonly Action? _closeHandler;

        public PushTransaction(Stream stream, Action? closeHandler = null)
        {
            _stream = stream;
            _closeHandler = closeHandler;
        }

        public void Dispose() => _closeHandler?.Invoke();
    }*/
    
    public interface IStreamDatabase<in TAddress>
    {
        bool CreatePullStream(TAddress address, out Stream stream);
        bool CreatePushStream(TAddress address, out Stream stream);
    }
}
