using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(BossBehavior))]
public class BossBehaviorEditor : Editor
{
    public int ActualPhase = 0;
    public string[] PhaseNames = new string[] { "Phase 1", "Phase 2", "Phase 3","Phase 4" };
    public string[] BossActionType = new string[] {"SHOOTER", "LASER", "SHIELD"};
    public string[] ActionType = new string[] { "SHOOTER", "LASER", "SHIELD" };
    public int Selection = 0;
    public bool AddAnAction;
    public DrawPhase Draw = DrawPhase.ADDACTION;
    public GameObject EditedObject;
    public bool ShowPhase;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        ShowPhase = GUILayout.Toggle(ShowPhase, "Show/Hide");
        if (ShowPhase)
        {
            SerializedProperty ActionsHolder = serializedObject.FindProperty("ActionHolder");
            EditorGUILayout.PropertyField(ActionsHolder);

            SerializedProperty BaseGameObject = serializedObject.FindProperty("BaseGameObject");
            EditorGUILayout.PropertyField(BaseGameObject);

            SerializedProperty BaseShooter= serializedObject.FindProperty("BaseShooter");
            EditorGUILayout.PropertyField(BaseShooter);

            SerializedProperty BaseShield = serializedObject.FindProperty("BaseShield");
            EditorGUILayout.PropertyField(BaseShield);

            SerializedProperty BaseLaser = serializedObject.FindProperty("BaseLaser");
            EditorGUILayout.PropertyField(BaseLaser);

            SerializedProperty listActionProperty = serializedObject.FindProperty("Phases");
            EditorGUILayout.PropertyField(listActionProperty);

        }

        BossBehavior Boss = target as BossBehavior;



        ActualPhase = GUILayout.Toolbar(ActualPhase, PhaseNames);
        DrawActualPhase(ActualPhase);

        GUILayout.Space(20);
        if (Draw == DrawPhase.ADDACTION)
        {
            if (GUILayout.Button("Add An Action"))
            {
                Draw = DrawPhase.SELECTACTION;
            }
        }

        if (Draw == DrawPhase.SELECTACTION)
        {
            Selection = EditorGUILayout.Popup("Type de l'action", Selection, ActionType);
            if (GUILayout.Button("Select"))
            {
                switch (Selection)
                {
                    case 0:
                        EditedObject = CreateBossShooter();
                        break;
                    case 1:
                        EditedObject = CreateLaser();
                        break;
                    case 2:
                        EditedObject = CreateShield();
                        break;
                    default:
                        break;
                }
                Draw = DrawPhase.CONFIGUREACTION;
            }
        }

        if (Draw == DrawPhase.CONFIGUREACTION)
        {
            switch (Selection)
            {
                case 0:
                    DrawShooter();
                    break;
                case 1:
                    DrawLaser();
                    break;
                case 2:
                    DrawShield();
                    break;
                default:
                    break;
            }
        }

        /*base.OnInspectorGUI();*/
        if (GUI.changed)
        {
            EditorUtility.SetDirty(Boss);
            EditorSceneManager.MarkSceneDirty(Boss.gameObject.scene);
        }

        serializedObject.ApplyModifiedProperties();
    }



    public void DrawActualPhase(int phase)
    {
        BossBehavior Boss = target as BossBehavior;

        SerializedProperty Movement = serializedObject.FindProperty("Phases").GetArrayElementAtIndex(phase).
            FindPropertyRelative("MovementBoss");
        EditorGUILayout.PropertyField(Movement);

        SerializedProperty Random = serializedObject.FindProperty("Phases").GetArrayElementAtIndex(phase).
            FindPropertyRelative("RandomAction");
        EditorGUILayout.PropertyField(Random);

        for (int i = 0; i < Boss.Phases[phase].ListAction.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            SerializedProperty Phases = serializedObject.FindProperty("Phases")
                .GetArrayElementAtIndex(phase).FindPropertyRelative("ListAction").GetArrayElementAtIndex(i);
            EditorGUILayout.PropertyField(Phases);

            if(i!=0)
            {
                if (GUILayout.Button("⇈"))
                {
                    SwapUpAction(i);
                }
            }
            else
            {
                if (GUILayout.Button("-----"))
                {

                }
            }

            if (i != Boss.Phases[phase].ListAction.Count - 1)
            {
                if (GUILayout.Button("⇊"))
                {
                    SwapDownAction(i);
                }
            }
            else
            {
                if (GUILayout.Button("-----"))
                {

                }
            }

            if (GUILayout.Button("X"))
            {
                RemoveAction(i);
            }
            
            EditorGUILayout.EndHorizontal();
        }
    }

    public void SwapUpAction(int id)
    {
        BossBehavior Boss = target as BossBehavior;
        BossAction TempAction = Boss.Phases[ActualPhase].ListAction[id];
        Boss.Phases[ActualPhase].ListAction[id] = Boss.Phases[ActualPhase].ListAction[id-1];
        Boss.Phases[ActualPhase].ListAction[id - 1] = TempAction;
    }

    public void SwapDownAction(int id)
    {
        BossBehavior Boss = target as BossBehavior;
        BossAction TempAction = Boss.Phases[ActualPhase].ListAction[id];
        Boss.Phases[ActualPhase].ListAction[id] = Boss.Phases[ActualPhase].ListAction[id + 1];
        Boss.Phases[ActualPhase].ListAction[id + 1] = TempAction;
    }
    public void RemoveAction(int id)
    {
        BossBehavior Boss = target as BossBehavior;
        DestroyImmediate(Boss.Phases[ActualPhase].ListAction[id].gameObject);
        Boss.Phases[ActualPhase].ListAction.RemoveAt(id);
    }
    public void DrawShooter()
    {
        SerializedProperty ShooterStats = serializedObject.FindProperty("BossBallThrower");
        EditorGUILayout.PropertyField(ShooterStats);
        if (GUILayout.Button("Valid"))
        {
            BossBehavior Boss = target as BossBehavior;
            EditedObject.GetComponent<BossShooter>().Instantier = Boss.BossBallThrower;
            EditedObject.name = EditedObject.GetComponent<BossShooter>().Instantier.BallThrower[0].Name;
            Draw = DrawPhase.ADDACTION;
        }
    }

    public void DrawLaser()
    {
        SerializedProperty LaserStats = serializedObject.FindProperty("LaserInstantier");
        EditorGUILayout.PropertyField(LaserStats);
        if (GUILayout.Button("Valid"))
        {
            BossBehavior Boss = target as BossBehavior;
            EditedObject.GetComponent<BossLaser>().Instantier = Boss.LaserInstantier;
            EditedObject.name = EditedObject.GetComponent<BossLaser>().Instantier.Stats[0].Name;
            Draw = DrawPhase.ADDACTION;
        }
    }

    public void DrawShield()
    {
        SerializedProperty ShieldStats = serializedObject.FindProperty("ShieldStats");
        EditorGUILayout.PropertyField(ShieldStats);
        if (GUILayout.Button("Valid"))
        {
            BossBehavior Boss = target as BossBehavior;
            EditedObject.GetComponent<BossShield>().Instantier = Boss.ShieldStats;
            EditedObject.name = EditedObject.GetComponent<BossShield>().Instantier.Stats.Name;
            Draw = DrawPhase.ADDACTION;
        }
    }

    /*public GameObject CreateObjectWithBossAction<T>(T action) where T:BossAction 
    {
        BossBehavior Parent = target as BossBehavior;
        GameObject Obj = Instantiate(BaseGameObject, Parent.transform);
        Obj.AddComponent(action.GetType());
        return Obj;
    }*/

    public GameObject CreateBossShooter()
    {
        BossBehavior boss = target as BossBehavior;
        GameObject Obj = Instantiate(boss.BaseGameObject, boss.ActionHolder.transform);
        Obj.AddComponent<BossShooter>();
        boss.Phases[ActualPhase].ListAction.Add(Obj.GetComponent<BossShooter>());
        return Obj;
    }

    public GameObject CreateShield()
    {
        BossBehavior boss = target as BossBehavior;
        GameObject Obj = Instantiate(boss.BaseGameObject, boss.ActionHolder.transform);
        Obj.AddComponent<BossShield>();
        boss.Phases[ActualPhase].ListAction.Add(Obj.GetComponent<BossShield>());
        return Obj;
    }

    public GameObject CreateLaser()
    {
        BossBehavior boss = target as BossBehavior;
        GameObject Obj = Instantiate(boss.BaseGameObject, boss.ActionHolder.transform);
        Obj.AddComponent<BossLaser>();
        boss.Phases[ActualPhase].ListAction.Add(Obj.GetComponent<BossLaser>());
        return Obj;
    }


    public enum DrawPhase
    {
        ADDACTION,
        SELECTACTION,
        CONFIGUREACTION,
    }


}
