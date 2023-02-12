using System;
using System.Collections.Generic;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace PrimitiveCreator
{
    public class ObjectPanel : MonoBehaviour
    {

        public ObjectPanel(IntPtr ptr) : base(ptr)
        {
        }

        public ObjectPanel() : base(ClassInjector.DerivedConstructorPointer<Main>())
        {
            ClassInjector.DerivedConstructorBody(this);
        }

        public static GUILayoutOption[] ButtonSize;
        private Rect _windowRectMy = new Rect((Screen.height - 100) / 2, (Screen.height - 100) / 2, 300, 600);
        private Rect _windowOptionPanel = new Rect((Screen.height - 100) / 2, (Screen.height - 100) / 2, 300, 100);
        public bool _showPanelMy;
        public bool _showOptionPanel = false;

        private string Old_name = "";
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

        private Color modelcolor = new Color(0.2F, 0.3F, 0.4F, 0.5F);
        private Vector3 modelScale = new Vector3(1, 1, 1);
        private Quaternion modelRotate = new Quaternion(0, 0, 0, 0);
        private Vector3 CustomPos = new Vector3(0, 0, 0);
        private bool Collision;

        public List<GameObject> PrimitiveObjects = new List<GameObject>();
        public List<String> PrimitiveObjects_name = new List<String>();
        public List<int> PrimitiveObjects_name_del = new List<int>();
        private GameObject PrimitiveObject;


        public Vector2 scrollPosition;

        public void OnGUI()
        {
            if (_showPanelMy) {
                _windowRectMy = GUILayout.Window(647, _windowRectMy, (GUI.WindowFunction)TexWindowMy, "Object panel",
                    new GUILayoutOption[0]);
            }
            if (_showOptionPanel)
            {
                _windowOptionPanel = GUILayout.Window(648, _windowOptionPanel, (GUI.WindowFunction)TexWindowOptionPanel, "Setting Object",
                    new GUILayoutOption[0]);
            }
        }

        public void TexWindowMy(int id)
        {
            if (id != 647) return;

            ButtonSize = new[]
                {
                    GUILayout.Width(40),
                    GUILayout.Height(20)
                };

            GUILayout.Space(10);

            PrimitiveObjects_name_del = new List<int>();
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, new GUILayoutOption[0]);
            foreach (var i in PrimitiveObjects)
            {
                if (i.name != "PrimitiveModObjects")
                {
                    int obj_index = PrimitiveObjects.IndexOf(i);
                    GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                    if (GUILayout.Button(PrimitiveObjects_name[obj_index], new GUILayoutOption[0]))
                    {
                        PrimitiveObject = i.gameObject;
                        OpenOption(PrimitiveObject.name);
                    }
                    if (GUILayout.Button("X", ButtonSize))
                    {
                        PrimitiveObjects_name_del.Add(obj_index);
                    }
                    GUILayout.EndHorizontal();
                }
            }
            GUILayout.EndScrollView();
            foreach (var i in PrimitiveObjects_name_del)
            {
                if (PrimitiveObject != null)
                {
                    if (PrimitiveObject.name == PrimitiveObjects_name[i]) _showOptionPanel = false;
                }
                Destroy(PrimitiveObjects[i].gameObject);
                PrimitiveObjects_name.RemoveAt(i);
                PrimitiveObjects.RemoveAt(i);
            }
            
            GUI.DragWindow();
        }

        public void TexWindowOptionPanel(int id)
        {
            if (id != 648) return;

            ButtonSize = new[]
                {
                    GUILayout.Width(40),
                    GUILayout.Height(20)
                };

            GUILayout.Space(10);

            GUILayout.Label($"Object name: {PrimitiveObject.name}", new GUILayoutOption[0]);

            GUILayout.Space(4);

            if (PrimitiveObject.GetComponent<Collider>().enabled == true) Collision = true;
            else Collision = false;


            Collision = GUILayout.Toggle(Collision, "Collision", new GUILayoutOption[0]);

            if (Collision) PrimitiveObject.GetComponent<Collider>().enabled = true;
            else PrimitiveObject.GetComponent<Collider>().enabled = false;

            //char chr = Event.current.character;
            //if ((chr < '0' || chr > '9') && (chr < '.' || chr > '.'))
            //{
            //    Event.current.character = '\0';
            //}
            GUILayout.Space(10);
            if (GUILayout.Button("Reset", new GUILayoutOption[0]))
                Reset_option();
            GUILayout.Space(3);

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

            if (Scale_minus_y == "-")
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

            modelRotate = PrimitiveObject.transform.rotation;
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

            PrimitiveObject.transform.position = CustomPos;
            PrimitiveObject.transform.localScale = modelScale;
            PrimitiveObject.transform.rotation = modelRotate;
            PrimitiveObject.GetComponent<MeshRenderer>().material.color = modelcolor;

            GUI.DragWindow();
        }

        public void Update()
        {
            if (_showPanelMy)
                FocusedMy = false;

            if (PrimitiveObject == null)
            {
                _showOptionPanel = false;
            }

        }

        public void Reset_option()
        {
            modelcolor = new Color(0.2F, 0.3F, 0.4F, 0.5F);
            CustomPos = new Vector3(0, 0, 0);
            modelScale = new Vector3(1, 1, 1);
            modelRotate = new Quaternion(0, 0, 0, 0);
        }
        public void OpenOption(string i)
        {
            if (i != Old_name)
            {
                Old_name = PrimitiveObject.name;
                //Position
                if (0 < PrimitiveObject.transform.position.x){Pos_x = PrimitiveObject.transform.position.x.ToString(); minus_x = "+";}
                else {Pos_x = (-PrimitiveObject.transform.position.x).ToString();minus_x = "-";}

                if (0 < PrimitiveObject.transform.position.y) { Pos_y = PrimitiveObject.transform.position.y.ToString(); minus_y = "+"; }
                else { Pos_y = (-PrimitiveObject.transform.position.y).ToString(); minus_y = "-"; }

                if (0 < PrimitiveObject.transform.position.z) { Pos_z = PrimitiveObject.transform.position.z.ToString(); minus_z = "+"; }
                else { Pos_z = (-PrimitiveObject.transform.position.z).ToString(); minus_z = "-"; }


                //Scale
                if (0 < PrimitiveObject.transform.localScale.x) { Scale_x = PrimitiveObject.transform.localScale.x.ToString(); Scale_minus_x = "+"; }
                else { Scale_x = (-PrimitiveObject.transform.localScale.x).ToString(); Scale_minus_x = "-"; }

                if (0 < PrimitiveObject.transform.localScale.y) { Scale_y = PrimitiveObject.transform.localScale.y.ToString(); Scale_minus_y = "+"; }
                else { Scale_y = (-PrimitiveObject.transform.localScale.y).ToString(); Scale_minus_y = "-"; }

                if (0 < PrimitiveObject.transform.localScale.z) { Scale_z = PrimitiveObject.transform.localScale.z.ToString(); Scale_minus_z = "+"; }
                else { Scale_z = (-PrimitiveObject.transform.localScale.z).ToString(); Scale_minus_z = "-"; }

                modelcolor.r = PrimitiveObject.GetComponent<MeshRenderer>().material.color.r;
                modelcolor.g = PrimitiveObject.GetComponent<MeshRenderer>().material.color.g;
                modelcolor.b = PrimitiveObject.GetComponent<MeshRenderer>().material.color.b;


                _showOptionPanel = true;
            }
            else
            {
                Old_name = "";
                _showOptionPanel = false;
            }
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
}
