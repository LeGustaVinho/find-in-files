using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Object = UnityEngine.Object;

namespace LegendaryTools.FindInFiles
{
    public class FindInFilesOutput : EditorWindow
    {

        Vector2 scrollPosition;
        public FindInFilesItem[] items;

        void OnGUI()
        {

            ScriptableObject scriptableObj = this;
            SerializedObject serialObj = new SerializedObject(scriptableObj);
            SerializedProperty serialProp = serialObj.FindProperty("items");

            if (GUILayout.Button("Select All"))
            {
                var selectObjects = new List<Object>();
                foreach (var filesItem in items)
                {
                    selectObjects.AddRange(filesItem.files);
                }

                Selection.objects = selectObjects.ToArray();
            }

            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.ExpandWidth(true),
                GUILayout.ExpandHeight(true));
            EditorGUILayout.PropertyField(serialProp, true);
            GUILayout.EndScrollView();

            serialObj.ApplyModifiedProperties();
        }

    }
}