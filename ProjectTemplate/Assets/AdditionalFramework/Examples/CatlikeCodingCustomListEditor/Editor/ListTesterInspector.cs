﻿using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CustomListDemo)), CanEditMultipleObjects]
public class ListTesterInspector : Editor {

	public override void OnInspectorGUI () {
		serializedObject.Update();
		EditorList.Show(serializedObject.FindProperty("integers"), EditorListOption.ListSize);
		EditorList.Show(serializedObject.FindProperty("vectors"));
        EditorList.Show(serializedObject.FindProperty("strings"), EditorListOption.Buttons);
		EditorList.Show(serializedObject.FindProperty("objects"), EditorListOption.ListLabel | EditorListOption.Buttons);
		EditorList.Show(serializedObject.FindProperty("notAList"));
		serializedObject.ApplyModifiedProperties();
	}
}
