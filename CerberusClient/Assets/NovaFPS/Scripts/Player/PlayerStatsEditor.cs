#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

[System.Serializable]
[CustomEditor(typeof(PlayerStats))]
public class PlayerStatsEditor : Editor {
    private string[] tabs = { "Player States", "Fall Damage", "UI", "Events" };
    private int currentTab = 0;

    public override void OnInspectorGUI() {
        serializedObject.Update();
        PlayerStats myScript = target as PlayerStats;

        Texture2D myTexture = Resources.Load<Texture2D>("CustomEditor/playerState_CustomEditor") as Texture2D;
        GUILayout.Label(myTexture);

        EditorGUILayout.BeginVertical();
        currentTab = GUILayout.Toolbar(currentTab, tabs);
        EditorGUILayout.Space(10f);
        EditorGUILayout.EndVertical();

        #region variables

        if (currentTab >= 0 || currentTab < tabs.Length) {
            switch (tabs[currentTab]) {
                case "Player States":
                    EditorGUILayout.LabelField("PLAYER STATES", EditorStyles.boldLabel);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("maxHealth"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("maxShield"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("controllable"));
                    break;

                case "Fall Damage":
                    EditorGUILayout.LabelField("FALL DAMAGE", EditorStyles.boldLabel);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("takesFallDamage"));
                    if (myScript.takesFallDamage) {
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("minFallTime"));
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("fallDamageMultiplier"));
                    }
                    break;

                case "UI":
                    EditorGUILayout.LabelField("UI", EditorStyles.boldLabel);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("healthSlider"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("shieldSlider"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("healthStatesEffect"));
                    break;

                case "Events":
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("events"));
                    break;
            }
        }

        #endregion variables

        serializedObject.ApplyModifiedProperties();
    }
}

#endif