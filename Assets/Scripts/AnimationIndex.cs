using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using System.Linq;

[RequireComponent(typeof(Animator))]
public class AnimationIndex : MonoBehaviour {
    public List<string> clipNames = new List<string>();
    public List<float> clipDurations = new List<float>();

    public Dictionary<string, float> ClipDuration { get; private set; }

    void Start()
    {
        ClipDuration = new Dictionary<string, float>();
        for (int i = 0; i < clipNames.Count; i++)
        {
            string stateName = clipNames[i];
            float duration = clipDurations[i];
            if (ClipDuration.ContainsKey(stateName))
            {
                Debug.LogError("Duplicated animation state: " + stateName);
                continue;
            }
            ClipDuration.Add(stateName, duration);
        }
    }

#if UNITY_EDITOR
    public void UpdateAnimationInfo()
    {
        Animator animator = GetComponent<Animator>();
        ClipDuration = new Dictionary<string, float>();
        AnimatorController rac = animator.runtimeAnimatorController as AnimatorController;
        foreach (ChildAnimatorState state in rac.layers[0].stateMachine.states)
        {
            string stateName = state.state.name;
            float duration = 0;
            if (state.state.motion == null)
            {
                Debug.LogError("No animation for state: " + stateName);
            }
            else
            {
                duration = state.state.motion.averageDuration;
            }
            clipNames.Add(stateName);
            clipDurations.Add(duration);
        }
    }

    public void ClearAnimationInfo()
    {
        clipNames.Clear();
        clipDurations.Clear();
    }
#endif
}

[CustomEditor(typeof(AnimationIndex))]
public class AnimationIndexEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Bake"))
        {
            AnimationIndex index = this.target as AnimationIndex;
            index.UpdateAnimationInfo();
        }
        if (GUILayout.Button("Clear"))
        {
            AnimationIndex index = this.target as AnimationIndex;
            index.ClearAnimationInfo();
        }
    }
}