using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public enum Algorithm
{
    PerlinNoise
}

[System.Serializable]
[CreateAssetMenu(fileName = "NewMapSettings", menuName = "Map Settings", order = 0)]
public class MapSettings : ScriptableObject
{
    public int width;
    public int height;

    public Algorithm algorithm;
    public bool randomSeed;
    public float seed;
}


#if UNITY_EDITOR
//Custom UI for our class
[CustomEditor(typeof(MapSettings))]
public class MapSettingsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapSettings mapSettings = (MapSettings)target;
        GUI.changed = false;
        EditorGUILayout.LabelField(mapSettings.name, EditorStyles.boldLabel);

        mapSettings.width = EditorGUILayout.IntField("Width", mapSettings.width);
        mapSettings.height = EditorGUILayout.IntField("Height", mapSettings.height);

        mapSettings.algorithm = (Algorithm)EditorGUILayout.EnumPopup(new GUIContent("Generation Method", "The generation method we want to use to generate the map"), mapSettings.algorithm);
        mapSettings.randomSeed = EditorGUILayout.Toggle("Random Seed", mapSettings.randomSeed);

        //Only appear if we have the random seed set to false
        if (!mapSettings.randomSeed)
        {
            mapSettings.seed = EditorGUILayout.FloatField("Seed", mapSettings.seed);
        }

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        AssetDatabase.SaveAssets();

        if (GUI.changed)
            EditorUtility.SetDirty(mapSettings);
    }
}
#endif
