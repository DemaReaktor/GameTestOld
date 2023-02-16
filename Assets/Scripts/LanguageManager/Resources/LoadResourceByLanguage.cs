using UnityEditor;
using System;

namespace Language.Resources
{
    /// <summary>
    /// has static methods for loading objects by path and language
    /// </summary>
    public class LoadResourceByLanguage
    {
        /// <summary>
        /// load object by path and language
        /// can be throwed exception if object of this path is not exist
        /// </summary>
        /// <param name="path">path of object
        /// {{language}} will be changed into value language</param>
        public static object LoadObject(string path, string language)
        {
            path = path.Replace("{{language}}", language);
            object resource = AssetDatabase.LoadAssetAtPath(path, typeof(object));

            if (resource is null)
                throw new Exception($"object of path {path} does not exist");

            return resource;
        }
        /// <summary>
        /// load object by path and language 
        /// </summary>
        /// <param name="path">path of object</param>
        /// <param name="resource">loaded object</param>
        /// <returns>true if object is found and loaded oherwise false</returns>
        public static bool TryLoadObject(string path, string language, out object resource)
        {
            path = path.Replace("{{language}}", language).Replace("{{Language}}", language);
            resource = AssetDatabase.LoadAssetAtPath(path, typeof(object));

            return resource != null;
        }
    }
}