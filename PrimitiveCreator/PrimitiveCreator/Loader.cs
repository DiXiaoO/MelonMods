using System;
using MelonLoader;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace PrimitiveCreator
{
    public static class BuildInfo
    {
        public const string Name = "PrimitiveCreator";
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
        
        private GameObject _PrimitiveCreatorObj;

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
            if (_PrimitiveCreatorObj != null) return;
            _PrimitiveCreatorObj = new GameObject();
            _PrimitiveCreatorObj.name = "PrimitiveMod";
            _PrimitiveCreatorObj.AddComponent<Main>();
            GameObject.DontDestroyOnLoad(_PrimitiveCreatorObj);
        }
    }
}