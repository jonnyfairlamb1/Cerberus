#if UNITY_EDITOR

using UnityEditor.Presets;

#endif

using UnityEngine;
using NovaFPS;

public class CrosshairShape : MonoBehaviour {
#if UNITY_EDITOR
    [HideInInspector] public Preset currentPreset;

    public Preset defaultPreset;
#endif

    [System.Serializable]
    public class Parts {
        public bool topPart, downPart, leftPart, rightPart;
    }

    public Parts parts;

    public string presetName;

#if UNITY_EDITOR

    private void Start() => ResetCrosshair();

    private void ResetCrosshair() => NovaFPSUtilities.ApplyPreset(currentPreset, this);

#endif
}