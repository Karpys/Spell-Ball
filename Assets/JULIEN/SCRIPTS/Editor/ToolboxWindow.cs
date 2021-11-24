using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Codice.CM.Client.Differences.Merge;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToolboxWindows : EditorWindow
{
    private List<Scene> _scenes;
    private Vector2 _sceneScrollPosition;
    private Vector2 _sceneDuplicatorScrollPosition;

    private bool _showSceneTools;
    private bool _showSelectScene;
    private bool _showSceneDuplicator;

    private bool _showMeshTools;

    private GUIStyle _fakeButtonLarge;
    private GUIStyle _fakeButtonMedium;

    Object _sceneToDuplicate;
    GameObject _objectToDestroy;

    private string m_savePath = null;
    private string m_prefabDestroyerPath = null;

    private int _numberOfPiecesDestroyed;

    [MenuItem("Toolbox/Utilities")]
    static void InitWindows()
    {
        ToolboxWindows window = GetWindow<ToolboxWindows>();

        window.minSize = new Vector2(300, 200);

        window.titleContent = new GUIContent("Toolbox");

        window.Show();
    }

    void OnEnable()
    {
        _scenes = new List<Scene>();
    }

    void OnGUI()
    {
        _fakeButtonLarge = new GUIStyle(GUI.skin.textField);
        _fakeButtonLarge.alignment = TextAnchor.MiddleCenter;

        _fakeButtonMedium = new GUIStyle(GUI.skin.textField);
        _fakeButtonMedium.alignment = TextAnchor.MiddleCenter;
        _fakeButtonMedium.margin = new RectOffset(100, 100, 0, 0);

        var scenesFiles = AssetDatabase.FindAssets("t:Scene");

        var scenePaths = scenesFiles.Select(AssetDatabase.GUIDToAssetPath).Where(s => s.StartsWith("Assets/"));

        int index = 0;

        #region Scene Tools
        _showSceneTools = GUILayout.Toggle(_showSceneTools, "Scene Tools", _fakeButtonLarge);
        if (_showSceneTools)
        {
            #region Scene Selection
            _showSelectScene = GUILayout.Toggle(_showSelectScene, "Scene Selector", _fakeButtonMedium);
            if (_showSelectScene)
            {
                _sceneScrollPosition = GUILayout.BeginScrollView(_sceneScrollPosition, GUILayout.Height(200));
                foreach (string path in scenePaths)
                {
                    GUIStyle style = new GUIStyle(GUI.skin.button);
                    style.normal.textColor = getColorBasedOnIndex(index);
                    style.hover.textColor = getColorBasedOnIndex(index);
                    if (GUILayout.Button(path.Substring(path.LastIndexOf("/") + 1, path.LastIndexOf(".") - (path.LastIndexOf("/") + 1)), style))
                    {
                        EditorSceneManager.OpenScene(path);
                    }

                    index++;
                }
                GUILayout.EndScrollView();
            }
            #endregion

            #region Scene Duplicator

            _showSceneDuplicator = GUILayout.Toggle(_showSceneDuplicator, "Scene Duplicator", _fakeButtonMedium);
            if (_showSceneDuplicator)
            {
                _sceneDuplicatorScrollPosition = GUILayout.BeginScrollView(_sceneDuplicatorScrollPosition, GUILayout.Height(200));

                GUILayout.BeginHorizontal();
                GUILayout.Label("File path");
                if (GUILayout.Button("Select save path"))
                {
                    m_savePath = EditorUtility.SaveFilePanel("Choose where to save the new scene", Application.dataPath, "duplicate_scene", "unity");
                }
                GUILayout.EndHorizontal();

                if (GUILayout.Button("Duplicate active scene"))
                {
                    if (m_savePath != null && m_savePath.Length > 0)
                    {
                        if (m_savePath.StartsWith(Application.dataPath))
                        {
                            string relativePath = "Assets" + m_savePath.Substring(Application.dataPath.Length);
                            bool success = AssetDatabase.CopyAsset(SceneManager.GetActiveScene().path, relativePath);
                        }
                    }
                }

                GUILayout.Space(25);
                GUILayout.BeginHorizontal();
                GUILayout.Label("Select Scene :");
                _sceneToDuplicate = EditorGUILayout.ObjectField(_sceneToDuplicate, typeof(Object), true);
                GUILayout.EndHorizontal();

                if (GUILayout.Button("Duplicate selected scene"))
                {
                    if (m_savePath != null && m_savePath.Length > 0 && _sceneToDuplicate != null)
                    {
                        if (m_savePath.StartsWith(Application.dataPath))
                        {
                            string relativePath = "Assets" + m_savePath.Substring(Application.dataPath.Length);
                            AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(_sceneToDuplicate), relativePath);
                        }
                    }
                }


                GUILayout.EndScrollView();
            }

            #endregion
        }
        #endregion

        #region Mesh Tools

        _showMeshTools = GUILayout.Toggle(_showMeshTools, "Mesh Tools", _fakeButtonLarge);
        if (_showMeshTools)
        {
            GUILayout.Label("Mesh Destructor");
            GUILayout.Space(25);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Select Object To Destroy :");
            _objectToDestroy = (GameObject) EditorGUILayout.ObjectField(_objectToDestroy, typeof(GameObject), true);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Prefab path");
            if (GUILayout.Button("Select prefab path"))
            {
                m_prefabDestroyerPath = EditorUtility.SaveFilePanel("Choose where to save the new prefab", Application.dataPath, "object_destroyed", "prefab");
            }
            GUILayout.EndHorizontal();
            
            _numberOfPiecesDestroyed = EditorGUILayout.IntSlider("Number of pieces", _numberOfPiecesDestroyed, 0, 100);

            if (GUILayout.Button("Slice object"))
            {
                if (m_prefabDestroyerPath != null && m_prefabDestroyerPath.Length > 0 && _objectToDestroy != null)
                {
                    if (m_prefabDestroyerPath.StartsWith(Application.dataPath))
                    {
                        string relativePath = "Assets" + m_prefabDestroyerPath.Substring(Application.dataPath.Length);

                        MeshDestroyer meshDestroyer = new MeshDestroyer();
                        List<GameObject> list = meshDestroyer.DestroyMesh(_objectToDestroy, _numberOfPiecesDestroyed / 2);

                        GameObject prefabParent = new GameObject(_objectToDestroy.name + "_destroyed");

                        foreach (GameObject part in list)
                        {
                            part.transform.parent = prefabParent.transform;
                        }

                        PrefabUtility.SaveAsPrefabAsset(prefabParent, relativePath);
                    }
                }
            }


        }

        #endregion
    }

    Color getColorBasedOnIndex(int index)
    {
        switch (index % 4)
        {
            case 0:
                return Color.yellow;
            case 1:
                return Color.green;
            case 2:
                return Color.magenta;
            case 3:
                return Color.cyan;
            default:
                return Color.red;
        }
    }
}
