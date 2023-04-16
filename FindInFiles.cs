using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using System.Linq;

namespace LegendaryTools.FindInFiles
{
    public class FindInFiles : EditorWindow
    {
        public string pattern = "*.unity,*.prefab,*.asset,*.mat";
        public UnityEngine.Object[] files;
        List<FindInFilesItem> allItems;

        Vector2 scrollPosition;

        [MenuItem("Window/Find In Files")]
        static void OpenFindWindow()
        {
            EditorWindow.GetWindow(typeof(FindInFiles), false, "Find in Files");
        }

        void OnGUI()
        {

            ScriptableObject scriptableObj = this;
            SerializedObject serialObj = new SerializedObject(scriptableObj);
            SerializedProperty serialPattern = serialObj.FindProperty("pattern");
            SerializedProperty serialProp = serialObj.FindProperty("files");

            GUILayout.Space(20);
            if (GUILayout.Button("Find in Files", GUILayout.Height(50)))
                Find();
            GUILayout.Space(10);

            EditorGUILayout.PropertyField(serialPattern, true);
            GUILayout.Space(10);

            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.ExpandWidth(true),
                GUILayout.ExpandHeight(true));
            EditorGUILayout.PropertyField(serialProp, true);
            GUILayout.EndScrollView();

            serialObj.ApplyModifiedProperties();
        }

        void Find()
        {

            DateTime init = DateTime.Now;

            FindInFilesOutput win = EditorWindow.GetWindow<FindInFilesOutput>("Found in Files");

            string assetsFolder = Application.dataPath;
            string[] allfiles = Directory.GetFiles(assetsFolder, "*.*", SearchOption.AllDirectories)
                .Where(s => pattern.Contains(Path.GetExtension(s).ToLower())).ToArray<string>();

            Dictionary<string, FindInFilesItem> itemsDict = new Dictionary<string, FindInFilesItem>();
            foreach (UnityEngine.Object inFile in files)
            {
                string guid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(inFile));
                string objFileName = AssetDatabase.GetAssetPath(inFile);
                FindInFilesItem item = new FindInFilesItem(inFile, guid, inFile.name, objFileName);
                itemsDict[guid] = item;
            }

            foreach (string fileName in allfiles)
            {
                string contents = File.ReadAllText(fileName);

                foreach (UnityEngine.Object inFile in files)
                {
                    string guid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(inFile));
                    string objFileName = AssetDatabase.GetAssetPath(inFile);
                    FindInFilesItem item = new FindInFilesItem(inFile, guid, inFile.name, objFileName);

                    if (contents.Contains(guid))
                    {
                        string relativeFileName =
                            string.Format("Assets{0}", fileName.Replace(Application.dataPath, ""));
                        UnityEngine.Object assetObj =
                            AssetDatabase.LoadAssetAtPath(relativeFileName, typeof(UnityEngine.Object));
                        itemsDict[guid].files.Add(assetObj);
                    }
                }
            }

            allItems = new List<FindInFilesItem>();
            foreach (var item in itemsDict.Values)
            {
                FindInFilesItem outItem = (FindInFilesItem)item;
                outItem.Name += " ::: " + item.files.Count().ToString();
                allItems.Add(outItem);
            }

            win.items = allItems.ToArray();

            DateTime end = DateTime.Now;
            Debug.Log(string.Format("Search finished! {0}s", end.Subtract(init).TotalSeconds).ToString());

        }
    }
}