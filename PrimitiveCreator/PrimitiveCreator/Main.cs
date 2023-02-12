using System;
using System.Collections.Generic;
using UnhollowerRuntimeLib;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;

namespace PrimitiveCreator
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
        private GameObject PrimitiveMod = new GameObject();
        private Color modelcolor = new Color(0.2F, 0.3F, 0.4F, 0.5F);
        private Vector3 modelPos = new Vector3(1, 1, 1);
        private Vector3 modelScale = new Vector3(1, 1, 1);
        private Quaternion modelRotate = new Quaternion(0,0,0,0);
        private Quaternion PlayerRotate;
        private string PlayerPosin = "Player: Null";
        private Vector3 CustomPos = new Vector3(0, 0, 0);
        private bool _showPanelMy;
        private bool Collision;
        private bool PosType;
        private bool Primvkl = false;
        private bool MOvkl = false;
        private bool PrimitivePath = true;
        private string Pos_x = "0";
        private string Pos_y = "0";
        private string Pos_z = "0";
        private string minus_x = "+";
        private string minus_y = "+";
        private string minus_z = "+";
        private string Scale_x = "1 ";
        private string Scale_y = "1";
        private string Scale_z = "1";
        private string Scale_minus_x = "+";
        private string Scale_minus_y = "+";
        private string Scale_minus_z = "+";
        private string moFilePath = "model.mo";
        private string TextPath = "texture.png";
        public static GUILayoutOption[] ButtonSize;
        public static GUILayoutOption[] ButtonSize2;
        private Rect _windowRectMy = new Rect((Screen.height - 100) / 2, (Screen.height - 100) / 2, 300, 100);
        private float num_object;
        public PrimitiveCreator.ObjectPanel ObjectMenu = new PrimitiveCreator.ObjectPanel();
        #endregion

        public void OnGUI()
        {
            if (_showPanelMy)
            {
                _windowRectMy = GUILayout.Window(645, _windowRectMy, (GUI.WindowFunction)TexWindowMy, "Primitive Creator",
                    new GUILayoutOption[0]);
            }
            ObjectMenu.OnGUI();
        }

        public void TexWindowMy(int id)
        {
            if (id != 645) return;

            ButtonSize = new[]
                {
                    GUILayout.Width(40),
                    GUILayout.Height(20)
                };
            if (GUILayout.Button("Object Panel (+Lags)", new GUILayoutOption[0]))
            {
                ObjectMenu._showPanelMy = !ObjectMenu._showPanelMy;
            }
            GUILayout.Space(10);
            PosType = GUILayout.Toggle(PosType, "Player Position", new GUILayoutOption[0]);
            Collision = GUILayout.Toggle(Collision, "Collision", new GUILayoutOption[0]);
            if (GUILayout.Button("Delete All Object", new GUILayoutOption[0]))
                DeleteAllModel();
            Primvkl = GUILayout.Toggle(Primvkl, "Primitive", new GUILayoutOption[0]);
            if (Primvkl)
            {
                if (GUILayout.Button("Load Plane", new GUILayoutOption[0]))
                    Load_Plane();
                if (GUILayout.Button("Load Cube", new GUILayoutOption[0]))
                    Load_Cube();
                if (GUILayout.Button("Load Sphere", new GUILayoutOption[0]))
                    Load_Sphere();
                if (GUILayout.Button("Load Capsule", new GUILayoutOption[0]))
                    Load_Capsule();
                if (GUILayout.Button("Load Cylinder", new GUILayoutOption[0]))
                    Load_Cylinder();
                if (GUILayout.Button("Load Quad", new GUILayoutOption[0]))
                    Load_Quad();
            }
            MOvkl = GUILayout.Toggle(MOvkl, ".mo objects", new GUILayoutOption[0]);
            if (MOvkl)
            {
                GUILayout.Label("Model Path", new GUILayoutOption[0]);
                moFilePath = GUILayout.TextField(moFilePath, new GUILayoutOption[0]);
                GUILayout.Label("Texture Path", new GUILayoutOption[0]);
                TextPath = GUILayout.TextField(TextPath, new GUILayoutOption[0]);
                if (GUILayout.Button("Load .mo", new GUILayoutOption[0]))
                    Load_Mo();
            }
            GUILayout.Space(10);
            if (GUILayout.Button("Reset", new GUILayoutOption[0]))
                Reset_option();
            GUILayout.Space(3);
            //char chr = Event.current.character;
            //if ((chr < '0' || chr > '9') && (chr < '.' || chr > '.'))
            //{
            //    Event.current.character = '\0';
            //}

            if (!PosType)
            {
                GUILayout.Label("Position", new GUILayoutOption[0]);
                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                if (GUILayout.Button(minus_x, ButtonSize))
                {
                    if (minus_x == "+")
                    {
                        minus_x = "-";
                    }
                    else
                    {
                        minus_x = "+";
                    }
                }
                Pos_x = GUILayout.TextField(Pos_x.ToString(), new GUILayoutOption[0]);
                GUILayout.EndHorizontal();
                if (Pos_x == "")
                {
                    CustomPos.x = 0;
                }
                else
                {
                    CustomPos.x = float.Parse(Pos_x);
                }

                if (minus_x == "-")
                {
                    CustomPos.x = -CustomPos.x;
                }

                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                if (GUILayout.Button(minus_y, ButtonSize))
                {
                    if (minus_y == "+")
                    {
                        minus_y = "-";
                    }
                    else
                    {
                        minus_y = "+";
                    }
                }
                Pos_y = GUILayout.TextField(Pos_y.ToString(), new GUILayoutOption[0]);
                GUILayout.EndHorizontal();
                if (Pos_y == "")
                {
                    CustomPos.y = 0;
                }
                else
                {
                    CustomPos.y = float.Parse(Pos_y);
                }

                if (minus_y == "-")
                {
                    CustomPos.y = -CustomPos.y;
                }

                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                if (GUILayout.Button(minus_z, ButtonSize))
                {
                    if (minus_z == "+")
                    {
                        minus_z = "-";
                    }
                    else
                    {
                        minus_z = "+";
                    }
                }
                Pos_z = GUILayout.TextField(Pos_z.ToString(), new GUILayoutOption[0]);
                GUILayout.EndHorizontal();
                if (Pos_z == "")
                {
                    CustomPos.z = 0;
                }
                else
                {
                    CustomPos.z = float.Parse(Pos_z);
                }
                if (minus_z == "-")
                {
                    CustomPos.z = -CustomPos.z;
                }
            }
            GUILayout.Label(PlayerPosin, new GUILayoutOption[0]);
            GUILayout.Space(10);

            GUILayout.Label("Scale", new GUILayoutOption[0]);

            GUILayout.BeginHorizontal(new GUILayoutOption[0]);
            if (GUILayout.Button(Scale_minus_x, ButtonSize))
            {
                if (Scale_minus_x == "+")
                {
                    Scale_minus_x = "-";
                }
                else
                {
                    Scale_minus_x = "+";
                }
            }
            Scale_x = GUILayout.TextField(Scale_x.ToString(), new GUILayoutOption[0]);
            GUILayout.EndHorizontal();
            if (Scale_x == "")
            {
                modelScale.x = 0;
            }
            else
            {
                modelScale.x = float.Parse(Scale_x);
            }

            if (Scale_minus_x == "-")
            {
                modelScale.x = -modelScale.x;
            }

            GUILayout.BeginHorizontal(new GUILayoutOption[0]);
            if (GUILayout.Button(Scale_minus_y, ButtonSize))
            {
                if (Scale_minus_y == "+")
                {
                    Scale_minus_y = "-";
                }
                else
                {
                    Scale_minus_y = "+";
                }
            }
            Scale_y = GUILayout.TextField(Scale_y.ToString(), new GUILayoutOption[0]);
            GUILayout.EndHorizontal();
            if (Scale_y == "")
            {
                modelScale.y = 0;
            }
            else
            {
                modelScale.y = float.Parse(Scale_y);
            }

            if (minus_y == "-")
            {
                modelScale.y = -modelScale.y;
            }

            GUILayout.BeginHorizontal(new GUILayoutOption[0]);
            if (GUILayout.Button(Scale_minus_z, ButtonSize))
            {
                if (Scale_minus_z == "+")
                {
                    Scale_minus_z = "-";
                }
                else
                {
                    Scale_minus_z = "+";
                }
            }
            Scale_z = GUILayout.TextField(Scale_z.ToString(), new GUILayoutOption[0]);
            GUILayout.EndHorizontal();
            if (Scale_z == "")
            {
                modelScale.z = 0;
            }
            else
            {
                modelScale.z = float.Parse(Scale_z);
            }
            if (Scale_minus_z == "-")
            {
                modelScale.z = -modelScale.z;
            }

            GUILayout.Space(10);

            GUILayout.Label("Model Rotate", new GUILayoutOption[0]);
            GUILayout.Label($"X: {modelRotate.x * 180}", new GUILayoutOption[0]); ;
            GUILayout.Label($"Y: {modelRotate.y * 180}", new GUILayoutOption[0]); ;
            GUILayout.Label($"Z: {modelRotate.z * 180}", new GUILayoutOption[0]); ;
            modelRotate.x = GUILayout.HorizontalSlider(modelRotate.x, -1.0F, 1.0F, new GUILayoutOption[0]);
            modelRotate.y = GUILayout.HorizontalSlider(modelRotate.y, -1.0F, 1.0F, new GUILayoutOption[0]);
            modelRotate.z = GUILayout.HorizontalSlider(modelRotate.z, -1.0F, 1.0F, new GUILayoutOption[0]);
            GUILayout.Space(10);

            GUILayout.Label("Color RGB", new GUILayoutOption[0]);
            GUILayout.Label($"R: {modelcolor.r * 255}", new GUILayoutOption[0]); ;
            GUILayout.Label($"G: {modelcolor.g * 255}", new GUILayoutOption[0]); ;
            GUILayout.Label($"B: {modelcolor.b * 255}", new GUILayoutOption[0]); ;
            modelcolor.r = GUILayout.HorizontalSlider(modelcolor.r, 0.0F, 1.0F, new GUILayoutOption[0]);
            modelcolor.g = GUILayout.HorizontalSlider(modelcolor.g, 0.0F, 1.0F, new GUILayoutOption[0]);
            modelcolor.b = GUILayout.HorizontalSlider(modelcolor.b, 0.0F, 1.0F, new GUILayoutOption[0]);
            GUI.DragWindow();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.F10))
            {
                _showPanelMy = !_showPanelMy;
                if (ObjectMenu._showPanelMy) ObjectMenu._showPanelMy = _showPanelMy;
                if (ObjectMenu._showOptionPanel) ObjectMenu._showOptionPanel = _showPanelMy;
            }

            ObjectMenu.Update();

            if (_showPanelMy)
                FocusedMy = false;

            if (_avatarRootMy == null)
                _avatarRootMy = GameObject.Find("/EntityRoot/AvatarRoot");

            if (!_avatarRootMy) return;
            try
            {
                if (_activeAvatarMy == null)
                    FindActiveAvatar();
                if (!_activeAvatarMy.activeInHierarchy)
                    FindActiveAvatar();
                if (_activeAvatarMy != null)
                    PlayerPosUpdate();
            }
            catch
            {
            }
        }

        public void DeleteAllModel()
        {
            if (!PrimitivePath)
            {
                ObjectMenu._showOptionPanel = false;
                Destroy(PrimitiveMod);
                PrimitivePathCreate();
            }
        }

        public void Load_Plane()
        {
            if (PrimitivePath)
            {
                PrimitivePathCreate();
            }
            PlayerPos();
            GameObject PrimObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
            string name_obj = $"Plane_{num_object}";
            PrimObject.name = name_obj;
            ObjectMenu.PrimitiveObjects_name.Add(name_obj);
            LoadModel(PrimObject);
        }

        public void Load_Cube()
        {
            if (PrimitivePath)
            {
                PrimitivePathCreate();
            }
            PlayerPos();
            GameObject PrimObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            PrimObject.transform.SetParent(PrimitiveMod.transform);
            string name_obj = $"Cube_{num_object}";
            PrimObject.name = name_obj;
            ObjectMenu.PrimitiveObjects_name.Add(name_obj);
            LoadModel(PrimObject);

        }

        public void Load_Sphere()
        {
            if (PrimitivePath)
            {
                PrimitivePathCreate();
            }
            PlayerPos();
            GameObject PrimObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            string name_obj = $"Sphere_{num_object}";
            PrimObject.name = name_obj;
            ObjectMenu.PrimitiveObjects_name.Add(name_obj);
            LoadModel(PrimObject);
        }

        public void Load_Capsule()
        {
            if (PrimitivePath)
            {
                PrimitivePathCreate();
            }
            PlayerPos();
            GameObject PrimObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            string name_obj = $"Capsule_{num_object}";
            PrimObject.name = name_obj;
            ObjectMenu.PrimitiveObjects_name.Add(name_obj);
            LoadModel(PrimObject);
        }

        public void Load_Cylinder()
        {
            if (PrimitivePath)
            {
                PrimitivePathCreate();
            }
            PlayerPos();
            GameObject PrimObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            string name_obj = $"Cylinder_{num_object}";
            PrimObject.name = name_obj;
            ObjectMenu.PrimitiveObjects_name.Add(name_obj);
            LoadModel(PrimObject);
        }

        public void Load_Quad()
        {
            if (PrimitivePath)
            {
                PrimitivePathCreate();
            }
            PlayerPos();
            GameObject PrimObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
            string name_obj = $"Quad_{num_object}";
            PrimObject.name = name_obj;
            ObjectMenu.PrimitiveObjects_name.Add(name_obj);
            LoadModel(PrimObject);
        }

        public void Load_Mo()
        {
            if (PrimitivePath)
            {
                PrimitivePathCreate();
            }
            PlayerPos();
            GameObject PrimObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            num_object += 1f;

            JObject data = JObject.Parse(File.ReadAllText(moFilePath));

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

            //生成物体
            MeshFilter meshFilter = PrimObject.GetComponent<MeshFilter>();
            meshFilter.mesh = mesh;

            string _filePath = Path.Combine(Application.dataPath, "tex_test", TextPath);
            byte[] _fileData = File.ReadAllBytes(_filePath);
            Texture2D _tex = new Texture2D(1024, 1024);
            ImageConversion.LoadImage(_tex, _fileData);

            PrimObject.GetComponent<MeshRenderer>().materials[0].mainTexture = _tex;

            DestroyImmediate(PrimObject.GetComponent<BoxCollider>());
            PrimObject.AddComponent<MeshCollider>();

            string name_obj = $"Mo_{num_object}";
            PrimObject.name = name_obj;
            ObjectMenu.PrimitiveObjects_name.Add(name_obj);
            LoadModel(PrimObject);
        }

        public void LoadModel(GameObject Object)
        {
            Object.transform.SetParent(PrimitiveMod.transform);
            Object.transform.position = modelPos;
            Object.transform.localScale = modelScale;
            Object.transform.rotation = modelRotate;
            Object.GetComponent<MeshRenderer>().material.color = modelcolor;
            num_object += 1f;
            Object.layer = 8;
            if (!Collision)
            {
                Object.GetComponent<Collider>().enabled = false;
            }
            ObjectMenu.PrimitiveObjects.Add(Object);
        }

        public void Reset_option()
        {
            modelcolor = new Color(0.2F, 0.3F, 0.4F, 0.5F);
            modelPos = new Vector3(1, 1, 1);
            modelScale = new Vector3(1, 1, 1);
            modelRotate = new Quaternion(0, 0, 0, 0);
        }
        private void FindActiveAvatar()
        {
            if (_avatarRootMy.transform.childCount == 0) return;
            foreach (var a in _avatarRootMy.transform)
            {
                var active = a.Cast<Transform>();
                if (!active.gameObject.activeInHierarchy) continue;
                _activeAvatarMy = active.gameObject;
            }
        }

        public void PlayerPos()
        {
            if (PosType)
            {
                modelPos = _activeAvatarMy.transform.localPosition;
                modelPos.y = modelPos.y + 2;
                PlayerRotate = _activeAvatarMy.transform.localRotation;
            }
            else
            {
                modelPos = CustomPos;
                modelPos += GameObject.Find("/BigWorld(Clone)").transform.localPosition;
            }

        }

        public void PlayerPosUpdate()
        {
            PlayerPosin = $"Player: {(_activeAvatarMy.transform.localPosition - GameObject.Find("/BigWorld(Clone)").transform.localPosition).ToString()}";
        }

        public void PrimitivePathCreate()
        {
            PrimitiveMod = new GameObject("PrimitiveModObjects");
            DontDestroyOnLoad(PrimitiveMod);
            PrimitiveMod.transform.SetParent(GameObject.Find("/BigWorld(Clone)").transform);
            num_object = 0;
            PrimitivePath = false;
            ObjectMenu.PrimitiveObjects = new List<GameObject>();
            ObjectMenu.PrimitiveObjects_name = new List<String>();
        }

        private static bool FocusedMy
        {
            get => Cursor.lockState == CursorLockMode.Locked;
            set
            {
                Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None;
                Cursor.visible = value == false;
            }
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