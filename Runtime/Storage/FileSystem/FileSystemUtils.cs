#nullable enable

namespace TeamZero.StorageSystem
{
    public static class FileSystemUtils
    {
        /// <summary>
        /// Gets absolute path to an internal storage, if it exists and allowed to access, otherwise null.
        /// </summary>
        public static string GetInternalStoragePath()
        {
#if UNITY_EDITOR
            return UnityEngine.Application.persistentDataPath;
#elif UNITY_IOS
            return UnityEngine.Application.persistentDataPath;
#elif UNITY_ANDROID
            return AndroidFileSystemUtils.GetInternalStoragePath();
#else
            #error Not implemented
#endif
        }
    }
}
