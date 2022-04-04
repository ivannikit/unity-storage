#nullable enable
using UnityEngine;

namespace TeamZero.StorageSystem
{
    public abstract class PlayerPrefsDatabase : 
        IDatabase<string, int>, IDatabase<string, float>, IDatabase<string, string>
    {
        int IDatabase<string, int>.Pull(string address) => PlayerPrefs.GetInt(address);
        void IDatabase<string, int>.Push(string address, int data) => PlayerPrefs.SetInt(address, data);

        float IDatabase<string, float>.Pull(string address) => PlayerPrefs.GetFloat(address);
        void IDatabase<string, float>.Push(string address, float data) => PlayerPrefs.SetFloat(address, data);
        
        string IDatabase<string, string>.Pull(string address) => PlayerPrefs.GetString(address);
        void IDatabase<string, string>.Push(string address, string data) => PlayerPrefs.SetString(address, data);
    }
}