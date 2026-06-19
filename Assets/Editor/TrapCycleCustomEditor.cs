using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TrapCycleController))]
public class TrapCycleCustomEditor : Editor
{
    #region PROPERTIES
        private SerializedProperty trapProperty;
        private SerializedProperty isTimeBasedProperty;
        private SerializedProperty timeBeforeActivationProperty;
        private SerializedProperty timeBeforeDeactivationProperty;
    #endregion
    
    #region FIND PROPERTIES
        void OnEnable()
        {
            trapProperty = serializedObject.FindProperty("trap");
            isTimeBasedProperty = serializedObject.FindProperty("isTimeBased");
            timeBeforeActivationProperty = serializedObject.FindProperty("timeBeforeActivation");
            timeBeforeDeactivationProperty = serializedObject.FindProperty("timeBeforeDeactivation");
        }
    #endregion
    
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        #region SCRIPT REFERENCE
            SerializedProperty scriptReference = serializedObject.FindProperty("m_Script");
            GUI.enabled = false;    
            EditorGUILayout.PropertyField(scriptReference);
            GUI.enabled = true;
        #endregion
        
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Trap Data", GUILayout.Width(Screen.width/5f));
        trapProperty.objectReferenceValue = EditorGUILayout.ObjectField(trapProperty.objectReferenceValue, typeof(AActivableTrap), false, GUILayout.Width(Screen.width/3f));
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.Space();
        
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Is Time Based",  GUILayout.Width(Screen.width/5f));
        isTimeBasedProperty.boolValue = EditorGUILayout.Toggle(isTimeBasedProperty.boolValue, GUILayout.Width(10f));
        EditorGUILayout.EndHorizontal();

        if (isTimeBasedProperty.boolValue)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Before Activation", GUILayout.Width(Screen.width/5f));
            timeBeforeActivationProperty.floatValue = EditorGUILayout.FloatField(timeBeforeActivationProperty.floatValue, GUILayout.Width(Screen.width/15f));
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Before Deactivation", GUILayout.Width(Screen.width/5f));
            timeBeforeDeactivationProperty.floatValue = EditorGUILayout.FloatField(timeBeforeDeactivationProperty.floatValue, GUILayout.Width(Screen.width/15f));
            EditorGUILayout.EndHorizontal();
        }
        serializedObject.ApplyModifiedProperties();
    }
}
