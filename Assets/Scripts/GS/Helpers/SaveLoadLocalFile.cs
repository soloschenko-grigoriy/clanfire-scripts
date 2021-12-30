using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace GS.Helpers
{
    [Serializable]
    public class SaveLoadLocalFile
    {
        private string _path;
            
        public SaveLoadLocalFile(string fileName)
        {
            _path = Application.persistentDataPath + Path.DirectorySeparatorChar + fileName;
        }
    
        public void Save(object data)
        {
            using (var fs = File.Open(_path, FileMode.OpenOrCreate))
            {
                AddText(fs, JsonUtility.ToJson(data));
            }
            
            Debug.Log(_path);
        }

        public T Load<T>()
        {
            return JsonUtility.FromJson<T>(File.ReadAllText(_path));
        }

        public void Erase()
        {
            if(File.Exists(_path))
            {
                File.Delete(_path);
            }
        }

        private void AddText(FileStream fs, string value)  
        {  
            byte[] info = new UTF8Encoding(true).GetBytes(value);  
            fs.Write(info, 0, info.Length);  
        }
    }
}
