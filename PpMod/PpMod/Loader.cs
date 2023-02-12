using System;
using MelonLoader;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace PpMod
{
    public static class BuildInfo
    {
        public const string Name = "PpMod";
        public const string Description = null;
        public const string Author = "=)";
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
        
        private GameObject _PpModObj;

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
            if (_PpModObj != null) return;
            _PpModObj = new GameObject();
            _PpModObj.name = "PpMod";
            _PpModObj.AddComponent<Main>();
            GameObject.DontDestroyOnLoad(_PpModObj);
        }
    }
}