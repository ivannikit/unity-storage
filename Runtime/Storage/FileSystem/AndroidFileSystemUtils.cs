#if !UNITY_EDITOR && UNITY_ANDROID
using System;
using UnityEngine;

namespace TeamZero.StorageSystem
{
    internal static class AndroidFileSystemUtils
    {
        private static string _internalStoragePath;
        public static string GetInternalStoragePath()
        {
            if (_internalStoragePath != null)
                return _internalStoragePath;
                
            try
            {
                AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject activity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
                AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext");
                AndroidJavaObject filesDir = context.Call<AndroidJavaObject>("getFilesDir");
                _internalStoragePath = filesDir.Call<string>("getAbsolutePath");
                return _internalStoragePath;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return null;
            }
        }
        
        private const string EXTERNAL_STORAGE_STATE_MEDIA_MOUNTED = "mounted";
        private static string _externalStoragePath;
        public static string GetExternalStoragePath() 
        {
            if (_externalStoragePath != null)
                return _externalStoragePath;
            
            try
            {
                AndroidJavaClass environmentClass = new AndroidJavaClass("android.os.Environment");
                string state = environmentClass.CallStatic<string>("getExternalStorageState");
                if (state == EXTERNAL_STORAGE_STATE_MEDIA_MOUNTED)
                {
                    AndroidJavaObject directory = environmentClass.CallStatic<AndroidJavaObject>("getExternalStorageDirectory");
                    _externalStoragePath = directory.Call<string>("getAbsolutePath");
                }

                return _externalStoragePath;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return null;
            }
        }
    }
}
#endif
