using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BulletSO))]

public class ActivableBulletSpawnerCustomInspector : Editor
{
    #region PROPERTIES

    private SerializedProperty bulletPrefabProperty;
    private SerializedProperty bulletSpeedProperty;

    private SerializedProperty canExplodeProperty;
    private SerializedProperty explosionRadiusProperty;

    private SerializedProperty canHitEnemiesProperty;
    private SerializedProperty bulletDamageProperty;
    private SerializedProperty bulletLifetimeProperty;

    #endregion

    #region FIND PROPERTIES

    private void OnEnable()
    {
        bulletPrefabProperty = serializedObject.FindProperty("bulletPrefab");
        bulletSpeedProperty = serializedObject.FindProperty("bulletSpeed");
        canExplodeProperty = serializedObject.FindProperty("canExplode");
        explosionRadiusProperty = serializedObject.FindProperty("explosionRadius");
        canHitEnemiesProperty = serializedObject.FindProperty("canHitEnemies");
        bulletDamageProperty = serializedObject.FindProperty("bulletDamage");
        bulletLifetimeProperty = serializedObject.FindProperty("bulletLifetime");
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
        
        GUILayout.Label("Bullet data", EditorStyles.boldLabel, GUILayout.Width(Screen.width/5f));
       
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Prefab", GUILayout.Width(Screen.width/5f));
        bulletPrefabProperty.objectReferenceValue = EditorGUILayout.ObjectField(bulletPrefabProperty.objectReferenceValue,typeof(GameObject), allowSceneObjects: false, GUILayout.Width(Screen.width/7f));
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Speed", GUILayout.Width(Screen.width/5f));
        bulletSpeedProperty.floatValue = EditorGUILayout.FloatField(bulletSpeedProperty.floatValue, GUILayout.Width(Screen.width/7f));
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Damage", GUILayout.Width(Screen.width/5f));
        bulletDamageProperty.floatValue = EditorGUILayout.FloatField(bulletDamageProperty.floatValue, GUILayout.Width(Screen.width/7f));
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Lifetime", GUILayout.Width(Screen.width/5f));
        bulletLifetimeProperty.floatValue = EditorGUILayout.FloatField(bulletLifetimeProperty.floatValue, GUILayout.Width(Screen.width/7f));
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.Space();
        
        GUILayout.Label("Bullet settings", EditorStyles.boldLabel, GUILayout.Width(Screen.width/5f));

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Can hit enemies", GUILayout.Width(Screen.width/5f));
        canHitEnemiesProperty.boolValue = EditorGUILayout.Toggle(canHitEnemiesProperty.boolValue, GUILayout.Width(10f));
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Can explode", GUILayout.Width(Screen.width/5f));
        canExplodeProperty.boolValue = EditorGUILayout.Toggle(canExplodeProperty.boolValue, GUILayout.Width(10f));
        EditorGUILayout.EndHorizontal();

        if (canExplodeProperty.boolValue)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Explosion radius", GUILayout.Width(Screen.width/5f));
            explosionRadiusProperty.floatValue = EditorGUILayout.FloatField(explosionRadiusProperty.floatValue, GUILayout.Width(Screen.width/7f));
            EditorGUILayout.EndHorizontal();
        }
        serializedObject.ApplyModifiedProperties();
    }
}
