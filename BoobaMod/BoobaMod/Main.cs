using System;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace BoobaMod
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

        private GameObject pathBoob;

        private GameObject _avatarBoob_1;
        private GameObject _avatarBoob_2;


        private bool Boob_1 = false;
        private bool Boob_2 = false;


        private Vector3 BoobScale = new Vector3(1, 1, 1);
        private float BoobScale_Custom = 1;
        private bool _showPanelMy;

        private Rect _windowRectMy = new Rect((Screen.width - 150) / 2, (Screen.height - 50) / 2, 300, 100);

        #endregion

        public void OnGUI()
        {
            if (_showPanelMy)
            {
                _windowRectMy = GUILayout.Window(655, _windowRectMy, (GUI.WindowFunction)TexWindowMy, "Boobs mod",
                    new GUILayoutOption[0]);
            }
        }

        public void TexWindowMy(int id)
        {
            if (id != 655) return;


            GUILayout.Space(10);
            if (_avatarRootMy != null)
            {
                if (!Boob_1 && !Boob_2)
                {
                    GUILayout.Label($"No boobs (", new GUILayoutOption[0]);
                }
                else
                {

                    GUILayout.Label($"Boob scale: {BoobScale_Custom}", new GUILayoutOption[0]);
                    BoobScale_Custom = GUILayout.HorizontalSlider(BoobScale_Custom, (float)0, (float)10, new GUILayoutOption[0]);

                    if (GUILayout.Button("Normal boobs", new GUILayoutOption[0]))
                    {
                        BoobScale_Custom = 1;
                        BoobScale = new Vector3(1, 1, 1);
                    }

                    BoobScale = new Vector3(BoobScale_Custom, BoobScale_Custom, BoobScale_Custom);

                    _avatarBoob_1.transform.localScale = BoobScale;
                    _avatarBoob_2.transform.localScale = BoobScale;

                }
                }
            else
            {
                GUILayout.Label($"No boobs (", new GUILayoutOption[0]);
            }
            GUI.DragWindow();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.F4))
            {
                _showPanelMy = !_showPanelMy;
            }


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
                FindBoobs();
                if (Boob_1)
                {
                    BoobPathCreate();
                }
            }
        }

        private void FindBody()
        {
            foreach (var a in _activeAvatarMy.GetComponentsInChildren<Transform>())
            {
                if (a.name == "Body") { 
                    _activeAvatarBody = a.gameObject;
                    break;
                }
            }
        }

        private void FindBoobs()
        {
            Boob_1 = false;
            Boob_2 = false;


            foreach (var a in _activeAvatarBody.transform.GetComponent<SkinnedMeshRenderer>().bones)
            {
                switch (a.name)
                {
                    case "+Breast L A01":
                        _avatarBoob_1 = a.gameObject;
                        Boob_1 = true;
                        break;
                    case "+Breast R A01":
                        _avatarBoob_2 = a.gameObject;
                        Boob_2 = true;
                        break;

                }
            }


        }
        public void BoobPathCreate()
        {
                if (_avatarBoob_1.transform.parent.FindChild("Boobs"))
                {
                    pathBoob = _avatarBoob_1.transform.parent.FindChild("Boobs").gameObject;
                }
                else
                {
                    pathBoob = new GameObject("Boobs");
                    pathBoob.transform.parent = _avatarBoob_1.transform.parent;
                    pathBoob.transform.localScale = _avatarBoob_1.transform.parent.localScale;
                    pathBoob.transform.rotation = _avatarBoob_1.transform.parent.rotation;
                    pathBoob.transform.position = _avatarBoob_1.transform.parent.position;
                    if (Boob_1) _avatarBoob_1.transform.parent = pathBoob.transform;
                    if (Boob_2)  _avatarBoob_2.transform.parent = pathBoob.transform;
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

    public static class StringExtensions
    {
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source?.IndexOf(toCheck, comp) >= 0;
        }
    }
}