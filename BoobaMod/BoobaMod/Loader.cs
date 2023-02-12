using System;
using MelonLoader;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace BoobaMod
{
    public static class BuildInfo
    {
        public const string Name = "BoobaMod";
        public const string Description = null;
        public const string Author = "DiXiao";
        public const string Company = null;
        public const string Version = "0.0.1";
        public const string DownloadLink = null;
    }
    public class Loader : MelonMod
    {
        public static GameObject IsRunning;
        public static Action<string> Msg;
        public static Action<string> Warning;
        public static Action<string> Error;
        
        private GameObject _BoobaModObj;

        public override void OnApplicationStart()
        {
            ClassInjector.RegisterTypeInIl2Cpp<Main>();
            Msg = LoggerInstance.Msg;
            Warning = LoggerInstance.Warning;
            Error = LoggerInstance.Error;
        }
        
        public override void OnApplicationLateStart()
        {
            Load();
        }
        
        private void Load()
        {
            if (_BoobaModObj != null) return;
            _BoobaModObj = new GameObject();
            _BoobaModObj.name = "BoobaMod";
            _BoobaModObj.AddComponent<Main>();
            GameObject.DontDestroyOnLoad(_BoobaModObj);
        }
    }
}