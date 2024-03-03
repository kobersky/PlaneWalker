using System;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem.HID;


/* adds functionality to LevelEditor */
[ExecuteInEditMode]
[CustomEditor(typeof(LevelEditor))]
public class LevelEditorInspector : Editor
{
    private string _saveNewLevelButtonText = "Save New Level";
    private string _resetLevel = "Reset Level";
    private string _messageBox = "Info";

    private string LevelPath => Path.Combine("Assets", "Prefabs", "Levels", $"NewLevel_{DateTime.Now:yyyyMMdd_HHmmss}.prefab");

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelEditor levelEditor = (LevelEditor)target;

        if (GUILayout.Button(_saveNewLevelButtonText))
        {
            levelEditor.SaveNewLevel(LevelPath);
        }

        if (GUILayout.Button(_resetLevel))
        {
            levelEditor.ResetLevel();
        }

        levelEditor.ActionMessage = EditorGUILayout.TextField(_messageBox, levelEditor.ActionMessage);

        //not needed for final version
        /*      string _createBaseSurfaceButtonText = "Create Base Surface";
             if (GUILayout.Button(_createBaseSurfaceButtonText))
             {
                 levelEditor.CreateBaseSurface();
             }*/
    }

    private void OnSceneGUI()
    {
        HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
        LevelEditor levelEditor = (LevelEditor)target;

        Event currentEvent = Event.current;

        if (currentEvent.type == EventType.MouseDown && currentEvent.button == 0)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(currentEvent.mousePosition);
            RaycastHit hit;

            GameObject clickedObject = 
                HandleUtility.PickGameObject(currentEvent.mousePosition,true );

            if (clickedObject != null)
            {
                levelEditor.ExecuteEditorAction(clickedObject);
            }

            currentEvent.Use(); 
        }
    }
}
