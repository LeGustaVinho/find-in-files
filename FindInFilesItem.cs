using System;
using System.Collections.Generic;
using UnityEngine;

namespace LegendaryTools.FindInFiles
{
    [System.Serializable]
    public class FindInFilesItem
    {
        [HideInInspector] public string Name;
        [HideInInspector] public string guid;
        string fileName;
        public UnityEngine.Object originalFile;
        public List<UnityEngine.Object> files = new List<UnityEngine.Object>();

        public FindInFilesItem(UnityEngine.Object _originalFile, string _guid, string _name, string _fileName)
        {
            this.originalFile = _originalFile;
            this.guid = _guid;
            this.Name = _name;
            this.fileName = _fileName;
        }

        public static explicit operator FindInFilesItem(List<FindInFilesItem> v)
        {
            throw new NotImplementedException();
        }
    }
}