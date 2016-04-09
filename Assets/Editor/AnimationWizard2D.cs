using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Animation
{
    public readonly string Name;

    public int FramesCount
    {
        get { return SpritesList.Count; }
    }

    public int FrameRate
    {
        get { return _franeRate; }
        set
        {
            if (value > 0)
                _franeRate = value;
        }
    }

    private int _franeRate;
    public List<Sprite> SpritesList = new List<Sprite>();
    private readonly AnimationClip _animationClip = new AnimationClip();

    public Animation(string name, int frameRate)
    {
        Name = name;
        //_animationClip.name = name;
        //_franeRate = frameRate;
        //_animationClip.frameRate = _franeRate;
        //_animationClip.wrapMode = WrapMode.Loop;
    }

    public void AddSprite(Texture2D texture2D)
    {
        Sprite newSprite = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), 100f);
        SpritesList.Add(newSprite);
        SpritesList = SpritesList.OrderBy(t => t.texture.name).ToList();
    }

    public void GenerateAnimaton()
    {
        EditorCurveBinding cb = new EditorCurveBinding
        {
            type = typeof(SpriteRenderer),
            path = "",
            propertyName = "m_Sprite"
        };

        var keyFrames = new ObjectReferenceKeyframe[SpritesList.Count];

        for (var i = 0; i < 1; i++)
        {
            keyFrames[i] = new ObjectReferenceKeyframe
            {
                time = i,
                value = SpritesList[i]
            };
        }
        AnimationUtility.SetObjectReferenceCurve(_animationClip, cb, keyFrames);

        AssetDatabase.CreateAsset(_animationClip, "Assets/" + Name + ".anim");
        AssetDatabase.SaveAssets();
    }
}

public class AnimationWizard2D : EditorWindow
{
    private readonly Dictionary<string, Animation> _animationsDictionary = new Dictionary<string, Animation>();

    [MenuItem("Window/2D Tools/2D Animation Wizard")]
    public static void ShowWindow()
    {
        GetWindow(typeof(AnimationWizard2D));
    }

    private void CreateSpriteList()
    {
        _animationsDictionary.Clear();

        foreach (var o in Selection.objects)
        {
            Debug.Log(o.GetType().ToString());
            if (!(o is Texture2D)) continue;

            var texture2D = o as Texture2D;

            var infoData = texture2D.name.Split('_');

            if (_animationsDictionary.ContainsKey(infoData[1]))
            {
                _animationsDictionary[infoData[1]].AddSprite(texture2D);
            }
            else
            {
                _animationsDictionary.Add(infoData[1], new Animation(infoData[1], int.Parse(infoData[2])));
                _animationsDictionary[infoData[1]].AddSprite(texture2D);
            }
        }
        Debug.Log("Done!");
    }

    private void OnGUI()
    {
        if (_animationsDictionary.Count > 0)
        {
            if (GUILayout.Button("Clear")) _animationsDictionary.Clear();

            GUILayout.Label(_animationsDictionary.Count + " anumations added:");
            EditorGUILayout.BeginVertical();
            foreach (var a in _animationsDictionary)
            {
                EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(false));
                GUILayout.Label("Name: " + a.Value.Name, GUILayout.Width(200));
                GUILayout.Label("Framerate:" + a.Value.FrameRate);
                GUILayout.Label("Total frames: " + a.Value.FramesCount);
                foreach (var i in a.Value.SpritesList)
                {
                    GUILayout.Box(i.texture, GUILayout.Width(100), GUILayout.Height(100));
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
            if (GUILayout.Button("Generate animations"))
                foreach (var a in _animationsDictionary)
                {
                    a.Value.GenerateAnimaton();
                }
        }
        else
            if (GUILayout.Button("Add Images")) CreateSpriteList();
    }
}