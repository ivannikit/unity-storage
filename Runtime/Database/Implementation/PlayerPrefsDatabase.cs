#nullable enable
using UnityEngine;

namespace TeamZero.StorageSystem
{
    public abstract class PlayerPrefsDatabase : 
        IDatabase<string, int>, IDatabase<string, float>, IDatabase<string, string>
    {
        public bool Pull(string address, out int data)
        {
            data = PlayerPrefs.GetInt(address);
            return true;
        }

        public bool Push(string address, int data)
        {
            PlayerPrefs.SetInt(address, data);
            return true;
        }

        public bool Pull(string address, out float data)
        {
            data = PlayerPrefs.GetFloat(address);
            return true;
        }

        public bool Push(string address, float data)
        {
            PlayerPrefs.SetFloat(address, data);
            return true;
        }

        public bool Pull(string address, out string data)
        {
            data = PlayerPrefs.GetString(address);
            return true;
        }

        public bool Push(string address, string data)
        {
            PlayerPrefs.SetString(address, data);
            return true;
        }
    }
}