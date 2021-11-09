using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BossBehavior))]
public class BossBehaviorEditor : Editor
{
    public int ActualPhase = 0;
    public string[] PhaseName = new string[]{"Phase 1","Phase 2"};
    public string[] BossActionType = new string[] {"SHOOTER", "LASER", "MOVEMENT"};
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
            SerializedProperty BaseGameObject = serializedObject.FindProperty("BaseGameObject");
            EditorGUILayout.PropertyField(BaseGameObject);

            SerializedProperty listActionProperty = serializedObject.FindProperty("Phases");
            EditorGUILayout.PropertyField(listActionProperty);

        }

        BossBehavior Boss = target as BossBehavior;

        

        ActualPhase = GUILayout.Toolbar(ActualPhase, PhaseName);
        DrawActualPhase(ActualPhase);
        if (Draw == DrawPhase.ADDACTION)
        {
            if (GUILayout.Button("Add An Action"))
            {
                Draw = DrawPhase.SELECTACTION;
            }
        }

        if (Draw == DrawPhase.SELECTACTION)
        {
            Selection = EditorGUILayout.Popup("Type de l'action", Selection, BossActionType);
            if (GUILayout.Button("Select"))
            {
                switch (Selection)
                {
                    case 0:
                        EditedObject = CreateBossShooter();
                        break;
                    case 1:
                        DrawLaser();
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
                default:
                    break;
            }
        }
        
        /*base.OnInspectorGUI();*/


        serializedObject.ApplyModifiedProperties();
    }


    public void DrawActualPhase(int phase)
    {
        BossBehavior Boss = target as BossBehavior;

        for (int i = 0; i < Boss.Phases[phase].ListAction.Count; i++)
        {
            SerializedProperty Phases = serializedObject.FindProperty("Phases")
                .GetArrayElementAtIndex(phase).FindPropertyRelative("ListAction").GetArrayElementAtIndex(i);
            EditorGUILayout.PropertyField(Phases);
        }
    }
    public void DrawShooter()
    {
        SerializedProperty ShooterStats = serializedObject.FindProperty("BossBallThrower");
        EditorGUILayout.PropertyField(ShooterStats);
        if (GUILayout.Button("Valid"))
        {
            BossBehavior Boss = target as BossBehavior;
            EditedObject.GetComponent<BossShooter>().Instantier = Boss.BossBallThrower;
            EditedObject.name = EditedObject.GetComponent<BossShooter>().Instantier.BallThrower.Name;
            Draw = DrawPhase.ADDACTION;
        }
        GUILayout.Label("Draw Shooter");
    }

    public void DrawLaser()
    {

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
        BossBehavior Parent = target as BossBehavior;
        GameObject Obj = Instantiate(Parent.BaseGameObject, Parent.transform);
        Obj.AddComponent<BossShooter>();
        Parent.Phases[ActualPhase].ListAction.Add(Obj.GetComponent<BossShooter>());
        return Obj;
    }


    public enum DrawPhase
    {
        ADDACTION,
        SELECTACTION,
        CONFIGUREACTION,
    }


}
