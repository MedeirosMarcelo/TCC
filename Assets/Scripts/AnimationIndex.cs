using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using System.Linq;

[RequireComponent(typeof(Animator))]
public class AnimationIndex : MonoBehaviour {
    public Dictionary<string, float> clipDuration = new Dictionary<string, float>();
    public List<string> clipNames = new List<string>();
    public List<float> clipDurations = new List<float>();

    public void UpdateAnimationInfo()
    {
        Animator animator = GetComponent<Animator>();
        clipDuration = new Dictionary<string, float>();
        AnimatorController rac = animator.runtimeAnimatorController as AnimatorController;
        foreach (ChildAnimatorState state in rac.layers[0].stateMachine.states)
        {
            string stateName = state.state.name;
            string motionName = state.state.motion.name;
            float duration = state.state.motion.averageDuration;
            if (clipDuration.ContainsKey(stateName))
            {
                Debug.Log("Duplicated animation state: " + stateName);
                continue;
            }
            clipDuration.Add(stateName, duration);
            clipNames.Add(stateName);
            clipDurations.Add(duration);
        }
    }

    public void ClearAnimationInfo()
    {
        clipDuration.Clear();
        clipNames.Clear();
        clipDuration.Clear();
    }
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