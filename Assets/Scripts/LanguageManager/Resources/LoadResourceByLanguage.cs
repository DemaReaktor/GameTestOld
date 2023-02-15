using UnityEditor;
using System;

namespace Language.Resources
{
    public class LoadResourceByLanguage
    {
        public static object LoadObject(string path, string language, Type type)
        {
            path = path.Replace("{{language}}", language);

            return AssetDatabase.LoadAssetAtPath(path, type);
        }
        public static bool TryLoadObject(string path, string language, Type type, out object resource)
        {
            resource = null;
            try
            {
                path = path.Replace("{{language}}", language);
                resource = AssetDatabase.LoadAssetAtPath(path, type);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}