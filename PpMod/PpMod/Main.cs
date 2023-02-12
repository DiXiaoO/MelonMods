using System;
using UnhollowerRuntimeLib;
using UnityEngine;
using Newtonsoft.Json.Linq;

using System.IO;
using System.Linq;


namespace PpMod
{
    public class Main : MonoBehaviour
    {
        public Main(IntPtr ptr) : base(ptr)
        {
        }

        public Main() : base(ClassInjector.DerivedConstructorPointer<Main>())
        {
            ClassInjector.DerivedConstructorBody(this);
        }

        #region Properties

        private GameObject _avatarRootMy;
        private GameObject _activeAvatarMy;
        private GameObject _activeAvatarBody;

        private GameObject _avatarAss;

        private GameObject firsdick;
        private GameObject clonedick;

        private bool dick = false;
        private bool CreatePP = false;


        #endregion



        public void Update()
        {
            if (!CreatePP)
            {
                
                DontDestroyOnLoad(firsdick = GameObject.CreatePrimitive(PrimitiveType.Cube));
                firsdick.transform.name = "FirsDick";
                firsdick.transform.localPosition = new Vector3((float)0, (float)9999, (float)0);
                firsdick.transform.localScale = new Vector3((float)0.125, (float)0.125, (float)0.125);

                JObject data = JObject.Parse(PpMod.Properties.Resources.dick_txt);
                Vector3[] VertexArray = new Vector3[data["VertexArray"].Count()];
                Vector2[] UVArray = new Vector2[data["UVArray"].Count()];
                Vector3[] NormalArray = new Vector3[data["NormalArray"].Count()];
                int[] TriangleArray = new int[data["TriangleArray"].Count()];

                int i = 0;

                foreach (var item in data["VertexArray"])
                {
                    VertexArray[i] = new Vector3((float)item["x"], (float)item["y"], (float)item["z"]);
                    i++;
                }
                i = 0;
                foreach (var item in data["UVArray"])
                {
                    UVArray[i] = new Vector2((float)item["x"], (float)item["y"]);
                    i++;
                }
                i = 0;
                foreach (var item in data["NormalArray"])
                {
                    NormalArray[i] = new Vector3((float)item["x"], (float)item["y"], (float)item["z"]);
                    i++;
                }
                i = 0;
                foreach (var item in data["TriangleArray"])
                {
                    TriangleArray[i] = (int)item;
                    i++;
                }

                Mesh mesh = new Mesh();
                mesh.vertices = VertexArray;
                mesh.triangles = TriangleArray;
                if (UVArray.Length > 0)
                    mesh.uv = UVArray;
                if (NormalArray.Length > 0)
                    mesh.normals = NormalArray;
                mesh.RecalculateBounds();

                MeshFilter meshFilter = firsdick.GetComponent<MeshFilter>();
                meshFilter.mesh = mesh;

                Destroy(firsdick.GetComponent<BoxCollider>());

                byte[] _fileData;
                using (var stream = new MemoryStream())
                {
                    PpMod.Properties.Resources.dick_png.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    _fileData =  stream.ToArray();
                }

                Texture2D _tex = new Texture2D(1024, 1024);
                ImageConversion.LoadImage(_tex, _fileData);

                firsdick.GetComponent<MeshRenderer>().materials[0].mainTexture = _tex;

                CreatePP = true;
            }


            if (_avatarRootMy == null)
                _avatarRootMy = GameObject.Find("/EntityRoot/AvatarRoot");

            if (!_avatarRootMy) return;
            try
            {
                if (_activeAvatarMy == null)
                    FindActiveAvatar();
                if (!_activeAvatarMy.activeInHierarchy)
                    FindActiveAvatar();
            }
            catch
            {
            }
        }
        private void FindActiveAvatar()
        {
            if (_avatarRootMy.transform.childCount == 0) return;
            foreach (var a in _avatarRootMy.transform)
            {
                var active = a.Cast<Transform>();
                if (!active.gameObject.activeInHierarchy) continue;
                _activeAvatarMy = active.gameObject;
                FindBody();
                FindAss();
                if (!dick)
                {
                    getdick();
                }
            }
        }

        private void FindBody()
        {
            foreach (var a in _activeAvatarMy.GetComponentsInChildren<Transform>())
            {
                if (a.name == "Body")
                {
                    _activeAvatarBody = a.gameObject;
                    break;
                }
            }
        }

        private void FindAss()
        {
            dick = false;
            foreach (var a in _activeAvatarBody.GetComponentsInChildren<Transform>())
            {
                switch (a.name)
                {
                    case "dick":
                        dick = true;
                        Loader.Msg($"Found dick");
                        break;
                }
            }


            foreach (var a in _activeAvatarBody.transform.GetComponent<SkinnedMeshRenderer>().bones)
            {
                switch (a.name)
                {
                    case "+PelvisTwist CF A01":
                        _avatarAss = a.gameObject;
                        Loader.Msg($"Found ASS: {_avatarAss.name}");
                        break;
                }
            }
        }

        private void getdick()
        {
            clonedick = Instantiate(firsdick);
            clonedick.transform.name = "dick";
            clonedick.transform.parent = _avatarAss.transform;
            clonedick.transform.position = _avatarAss.transform.position;
            Vector3 tempPos = new Vector3 ((float)0, (float)-0.1, (float)0);
            clonedick.transform.localPosition = tempPos;
            Quaternion tempRot = new Quaternion (-0.5f,0.5f,-0.5f,-0.5f);
            clonedick.transform.localRotation = tempRot;
        }
    }

    public static class StringExtensions
    {
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source?.IndexOf(toCheck, comp) >= 0;
        }
    }
}