using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Varneon.TextureToolkit
{
    public static class TextureUtilities
    {
        /// <summary>
        /// Gets the prefix shared by two strings
        /// </summary>
        /// <returns></returns>
        public static string GetCommonPrefix(string string1, string string2)
        {
            int commonPrefixLength = 0;

            for (int i = 0; i < Mathf.Min(string1.Length, string2.Length); i++)
            {
                if (string1[i] != string2[i]) { commonPrefixLength = i; break; }
            }

            string commonPrefix = string1.Substring(0, commonPrefixLength);

            return commonPrefix;
        }

        /// <summary>
        /// Attempts to get the original size of the texture
        /// </summary>
        /// <param name="texture">Texture</param>
        /// <param name="width">How wide the original texture is in pixels</param>
        /// <param name="height">How high the original texture is in pixels</param>
        /// <param name="importer">Importer of the texture</param>
        /// <returns>Was the original texture size successfully read</returns>
        public static bool TryGetOriginalTextureSize(Texture2D texture, out int width, out int height, out TextureImporter importer)
        {
            if (texture != null)
            {
                string path = AssetDatabase.GetAssetPath(texture);

                importer = AssetImporter.GetAtPath(path) as TextureImporter;

                if (importer != null)
                {
                    object[] args = new object[] { 0, 0 };

                    MethodInfo method = typeof(TextureImporter).GetMethod("GetWidthAndHeight", BindingFlags.NonPublic | BindingFlags.Instance);

                    method.Invoke(importer, args);

                    width = (int)args[0];
                    height = (int)args[1];

                    return true;
                }
            }

            importer = null;
            width = 0;
            height = 0;
            return false;
        }
    }
}
