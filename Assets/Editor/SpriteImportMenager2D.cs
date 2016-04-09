using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpriteImportMenager2D : EditorWindow
{
    private static Dictionary<string, string> pathsDictionary = new Dictionary<string, string>();

    [MenuItem("Window/2D Tools/Sprite menager")]
    public static void ShowWindow()
    {
        GetWindow(typeof(SpriteImportMenager2D));
    }

    private void OnEnable()
    {
        pathsDictionary.Add("charAnim", "Assets/Graphics/Animations/Characters");
    }

    private void OnGUI()
    {
        if (GUILayout.Button(""))
            foreach (var o in Selection.objects)
            {
                if (!(o is Texture2D)) continue;
                var s = AssetRooting(o);
                GUILayout.Label(AssetDatabase.MoveAsset(s[0], s[1]));
            }
    }

    private string[] AssetRooting(Object _object)
    {
        var paths = new string[2];
        paths[0] = AssetDatabase.GetAssetPath(_object);
        var s = _object.name.Split('_');
        if (!AssetDatabase.IsValidFolder(pathsDictionary[s[0]] + "/" + s[1]))
        {
            AssetDatabase.CreateFolder(pathsDictionary[s[0]], s[1]);
        }
        else
        {
            if (!AssetDatabase.IsValidFolder(pathsDictionary[s[0]] + "/" + s[1] + "/" + s[2]))
            {
                AssetDatabase.CreateFolder(pathsDictionary[s[0]] + "/" + s[1], s[2]);
            }
            else
            {
                var ss = paths[0].Split('/');
                paths[1] = pathsDictionary[s[0]] + "/" + s[1] + "/" + s[2] + "/" + ss[1];
            }
        }
        return paths;
    }
}