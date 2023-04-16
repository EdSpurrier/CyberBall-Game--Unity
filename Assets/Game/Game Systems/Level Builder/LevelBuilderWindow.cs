#if (UNITY_EDITOR)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public static class LevelBuilderCore
{
    public static LevelBuilderWindow levelBuilderWindow;
}


public class LevelBuilderWindow : EditorWindow
{
    public LevelManager levelManager;
    public LevelBuilder levelBuilder;
    bool ToolsFoldout = true;

    // Add menu named "My Window" to the Window menu
    [MenuItem("Hacker Ball/Level Builder")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        LevelBuilderWindow window = (LevelBuilderWindow)EditorWindow.GetWindow(typeof(LevelBuilderWindow));
        
        window.Show();

        LevelBuilderCore.levelBuilderWindow = window;
    }

    static void OnEnable ()
    {
        LevelBuilderWindow window = (LevelBuilderWindow)EditorWindow.GetWindow(typeof(LevelBuilderWindow));

        window.titleContent = new GUIContent("Level Builder 1.0");
        
        LevelBuilderCore.levelBuilderWindow = window;

    }

    string levelManagerStatus = "Not Found...";
    string levelBuilderStatus = "Not Found...";



    void OnGUI()
    {
        ScanScene();




        GUILayout.Label("Scene Status", EditorStyles.boldLabel);
        
        GUILayout.Space(10);

        levelManagerStatus = EditorGUILayout.TextField("LevelManager:", levelManagerStatus);
        levelBuilderStatus = EditorGUILayout.TextField("LevelBuilder:", levelBuilderStatus);

        GUILayout.Space(25);




        GUILayout.Label("Level Builder Actions", EditorStyles.boldLabel);

        GUILayout.Space(10);

        if (GUILayout.Button("Scan Scene"))
        {
            levelManager = null;
            levelBuilder = null;
        };

        GUILayout.Space(10);

        if (levelBuilder)
        {
            if (GUILayout.Button("Close Builder"))
            {
                DestroyImmediate(levelBuilder.gameObject);
            };
        }
        else {
            if (!levelBuilder)
            {
                if (GUILayout.Button("Open Builder"))
                {
                    CreateLevelBuilder();
                    ScanScene();
                    levelBuilder.UpdateBuilder();
                };
            };
        };


        if (!levelManager)
        {
            if (GUILayout.Button("Setup Level Manager"))
            {
                CreateLevelManager();
            };
        };


        BuilderUpdate();
    }

    void BuilderUpdate()
    {
        if (levelBuilder)
        {
            Selection.activeObject = levelBuilder.gameObject;
        };
    }




    void ScanScene()
    {
        if (!levelManager)
        {
            LevelManager[] LevelManagersFound = FindObjectsOfType(typeof(LevelManager)) as LevelManager[];

            if (LevelManagersFound.Length > 1)
            {
                levelManagerStatus = "ERROR >> Multiple LevelManagers in Scene....";
                levelManager = LevelManagersFound[0];
            }
            else {
                if (LevelManagersFound.Length == 1)
                {
                    levelManagerStatus = "LevelManager Found in Scene";
                    levelManager = LevelManagersFound[0];
                }
                else
                {
                    levelManagerStatus = "Not Found...";
                };
            };
        };


        if (levelManager)
        {
            levelManagerStatus = "Found In Scene";
        }




        if (!levelBuilder)
        {
            LevelBuilder[] LevelBuildersFound = FindObjectsOfType(typeof(LevelBuilder)) as LevelBuilder[];

            if (LevelBuildersFound.Length > 1)
            {
                levelBuilderStatus = "ERROR >> Multiple LevelBuilders in Scene....";
                levelBuilder = LevelBuildersFound[0];
            }
            else
            {
                if (LevelBuildersFound.Length == 1)
                {
                    levelBuilderStatus = "LevelBuilder Found in Scene";
                    levelBuilder = LevelBuildersFound[0];
                } else {
                    levelBuilderStatus = "Not Found...";
                };
            }
        };

        if (levelBuilder)
        {
            levelBuilderStatus = "Found In Scene";

            if (!levelBuilder.levelManager && levelManager)
            {
                levelBuilder.levelManager = levelManager;
            };
            if (!levelBuilder.levelRoot && levelManager)
            {
                levelBuilder.levelRoot = levelManager.transform;
            };
        };
    }

    public static void ShowWindow()
    {
        GetWindow<LevelBuilderWindow>("Done Crawling!");
    }

    //Force unity to save changes or Unity may not save when we have instantiated/removed prefabs despite pressing save button
    private void MarkSceneAsDirty()
    {
        UnityEngine.SceneManagement.Scene activeScene = UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene();

        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(activeScene);
    }

    //Instantiate prefab
    public Transform SpawnPrefab(Transform obj)
    {
        Debug.Log("Spawning Object: " + obj);

        Transform newObj = PrefabUtility.InstantiatePrefab(obj) as Transform;

        MarkSceneAsDirty();

        return newObj;
    }

    void CreateLevelManager()
    {
        Debug.Log("Loading LevelManager Into Scene...");

        string[] guids = AssetDatabase.FindAssets("t:Object", new[] { "Assets/Game/Game Systems/Level Builder/Prefabs" });

        foreach (string guid in guids)
        {
            //Debug.Log(AssetDatabase.GUIDToAssetPath(guid));
            string myObjectPath = AssetDatabase.GUIDToAssetPath(guid);
            Object[] myObjs = AssetDatabase.LoadAllAssetsAtPath(myObjectPath);


            Object levelManagerPrefab = null;

            //Debug.Log("printing myObs now...");
            foreach (Object thisObject in myObjs)
            {
                //Debug.Log(thisObject.name);
                //Debug.Log(thisObject.GetType().Name); 
                string myType = thisObject.GetType().Name;
                if (myType == "LevelManager")
                {
                    Debug.Log("Level Manager Found in...  " + thisObject.name + " at " + myObjectPath);
                    levelManagerPrefab = thisObject;
                };
            };

            if (levelManagerPrefab != null)
            {
                PrefabUtility.InstantiatePrefab(levelManagerPrefab);
            };
        };

        MarkSceneAsDirty();
    }

    void CreateLevelBuilder()
    {
        Debug.Log("Loading LevelBuilder Into Scene...");

        string[] guids = AssetDatabase.FindAssets("t:Object", new[] { "Assets/Game/Game Systems/Level Builder/Prefabs" });

        foreach (string guid in guids)
        {
            //Debug.Log(AssetDatabase.GUIDToAssetPath(guid));
            string myObjectPath = AssetDatabase.GUIDToAssetPath(guid);
            Object[] myObjs = AssetDatabase.LoadAllAssetsAtPath(myObjectPath);


            Object levelBuilderPrefab = null;

            //Debug.Log("printing myObs now...");
            foreach (Object thisObject in myObjs)
            {
                //Debug.Log(thisObject.name);
                //Debug.Log(thisObject.GetType().Name); 
                string myType = thisObject.GetType().Name;
                if (myType == "LevelBuilder")
                {
                    Debug.Log("Level Builder Found in...  " + thisObject.name + " at " + myObjectPath);
                    levelBuilderPrefab = thisObject;
                };
            };

            if (levelBuilderPrefab != null)
            {
                PrefabUtility.InstantiatePrefab(levelBuilderPrefab);
            };
        };

        MarkSceneAsDirty();

    }
}


#endif