#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

[System.Serializable]
[CustomEditor(typeof(PlayerMovement))]
public class PlayerMovementEditor : Editor {
    private string[] tabs = { "Assignables", "Movement", "Camera", "Sliding", "Jumping", "Aim assist", "Stamina", "Others" };
    private int currentTab = 0;

    public override void OnInspectorGUI() {
        serializedObject.Update();
        PlayerMovement myScript = target as PlayerMovement;

        Texture2D myTexture = Resources.Load<Texture2D>("CustomEditor/playerMovement_CustomEditor") as Texture2D;
        GUILayout.Label(myTexture);

        EditorGUILayout.BeginVertical();
        currentTab = GUILayout.SelectionGrid(currentTab, tabs, 6);
        EditorGUILayout.Space(10f);
        EditorGUILayout.EndVertical();

        #region variables

        if (currentTab >= 0 || currentTab < tabs.Length) {
            switch (tabs[currentTab]) {
                case "Assignables":
                    EditorGUILayout.LabelField("ASSIGNABLES", EditorStyles.boldLabel);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("playerCam"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("orientation"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("UI"));
                    break;

                case "Camera":
                    EditorGUILayout.LabelField("CAMERA", EditorStyles.boldLabel);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("normalFOV"));
                    break;

                case "Movement":
                    EditorGUILayout.LabelField("BASIC MOVEMENT", EditorStyles.boldLabel);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("canRunBackwards"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("canRunWhileShooting"));
                    if (!myScript.canRunBackwards || !myScript.canRunWhileShooting) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("loseSpeedDeceleration"));
                        EditorGUI.indentLevel--;
                    }
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("acceleration"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("runSpeed"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("walkSpeed"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("crouchSpeed"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("crouchTransitionSpeed"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("whatIsGround"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("frictionForceAmount"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("maxSlopeAngle"));
                    break;

                case "Sliding":
                    EditorGUILayout.LabelField("SLIDING", EditorStyles.boldLabel);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("allowSliding"));
                    if (myScript.allowSliding) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("slideForce"));
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("slideCounterMovement"));
                        EditorGUI.indentLevel--;
                    }

                    break;

                case "Jumping":

                    EditorGUILayout.LabelField("JUMPING", EditorStyles.boldLabel);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("jumpForce"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("controlAirborne"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("allowCrouchWhileJumping"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("canJumpWhileCrouching"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("jumpCooldown"));
                    break;

                case "Aim assist":

                    EditorGUILayout.LabelField("AIM ASSIST", EditorStyles.boldLabel);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("applyAimAssist"));
                    if (myScript.applyAimAssist) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumDistanceToAssistAim"));
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("aimAssistSpeed"));
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("aimAssistSensitivity"));
                        EditorGUI.indentLevel--;
                    }

                    break;

                case "Stamina":
                    EditorGUILayout.LabelField("STAMINA", EditorStyles.boldLabel);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("usesStamina"));
                    if (myScript.usesStamina) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("minStaminaRequiredToRun"));
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("maxStamina"));
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("LoseStaminaWalking"));
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("staminaLossOnJump"));
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("staminaLossOnSlide"));
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("staminaSlider"));
                        EditorGUI.indentLevel--;
                    }
                    break;

                case "Others":
                    EditorGUILayout.LabelField("OTHERS", EditorStyles.boldLabel);
                    EditorGUILayout.LabelField("FOOTSTEPS", EditorStyles.boldLabel);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("footstepVolume"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("footsteps"));
                    EditorGUILayout.LabelField("EVENTS", EditorStyles.boldLabel);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("events"));
                    break;
            }
        }

        #endregion variables

        EditorGUILayout.Space(10f);
        serializedObject.ApplyModifiedProperties();
    }
}

#endif