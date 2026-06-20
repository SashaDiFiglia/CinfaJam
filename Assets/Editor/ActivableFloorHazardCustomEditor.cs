using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ActivableFloorHazard))]

public class ActivableFloorHazardCustomEditor : Editor
{
    #region PROPERTIES
        private SerializedProperty damageProperty;
        private SerializedProperty dealsDamageOverTimeProperty;
        private SerializedProperty delayBetweenHitsProperty;
    #endregion
    
    #region FIND PROPERTIES
        void OnEnable()
        {
            damageProperty = serializedObject.FindProperty("damage");
            dealsDamageOverTimeProperty = serializedObject.FindProperty("dealsDamageOverTime");
            delayBetweenHitsProperty = serializedObject.FindProperty("delayBetweenHits");
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

        GUILayout.Label("Floor Hazard Data", GUILayout.Width(Screen.width/5f));

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Damage", GUILayout.Width(Screen.width/5f));
        damageProperty.floatValue = EditorGUILayout.FloatField(damageProperty.floatValue, GUILayout.Width(Screen.width/10f));
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Damage Over Time", GUILayout.Width(Screen.width/5f));
        dealsDamageOverTimeProperty.boolValue = EditorGUILayout.Toggle(dealsDamageOverTimeProperty.boolValue, GUILayout.Width(10f));
        EditorGUILayout.EndHorizontal();     
        
        if (dealsDamageOverTimeProperty.boolValue)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Delay Between Hits", GUILayout.Width(Screen.width/5f));
            delayBetweenHitsProperty.floatValue = EditorGUILayout.FloatField(delayBetweenHitsProperty.floatValue, GUILayout.Width(Screen.width/10f));
            EditorGUILayout.EndHorizontal();
        }
        
        serializedObject.ApplyModifiedProperties();
    }
}
