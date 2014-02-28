using System;
using UnityEditor;
using UnityEngine;

namespace Nravo.Editors.Utils {

    public class EditorStateWindow : EditorWindow {
        static string _currentState = "";
        static GUIStyle _lableStyle = new GUIStyle();

        [MenuItem("Utils/Editor State")]
        public static void ShowWindow() {
            EditorWindow.GetWindow<EditorStateWindow>(false, "----", true);
        }

        void OnEnable() {
            _lableStyle.fontSize = 24;
            _lableStyle.alignment = TextAnchor.MiddleCenter;
            _lableStyle.padding = new RectOffset(10, 10, 10, 10);
            _lableStyle.normal.textColor = Color.gray;
        }

        void OnGUI() {
            GUILayout.Space(36f);
            EditorGUILayout.LabelField(_currentState, _lableStyle);
        }

        void Update() {
            _currentState = GetCurrentState();
            this.title = _currentState;
            Repaint();
        }

        string GetCurrentState() {
            var state = "Editing";
            _lableStyle.normal.textColor = Color.gray;

            if(EditorApplication.isCompiling) {
                state = "Compiling";
                _lableStyle.normal.textColor = Color.red;
            } else if(EditorApplication.isPaused) {
                state = "Paused";
                _lableStyle.normal.textColor = Color.yellow;
            } else if(EditorApplication.isPlaying) {
                state = "Playing";
                _lableStyle.normal.textColor = Color.green;
            } else if(EditorApplication.isUpdating) {
                state = "Updating";
                _lableStyle.normal.textColor = Color.gray;
            } 

            return state;
        }
    }
}
