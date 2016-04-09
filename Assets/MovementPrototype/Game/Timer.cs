using UnityEngine;
using System.Collections;

public class Timer {

    float elapsedTime = 0;
    float length;

    public bool Run(float duration) {
        bool ended;
        TimerCounter(duration, out ended);
        if (ended) {
            return true;
        }
        return false;
    }

    public void Reset() {
        elapsedTime = 0;
    }

    public float GetTimeIncreasing() {
        return elapsedTime;
    }

    public float GetTimeDecreasing() {
        return length - elapsedTime;
    }

    void TimerCounter(float length, out bool ended) {
        this.length = length;
        if (length > 0) {
            if (elapsedTime < length) {
                elapsedTime += Time.deltaTime;
                ended = false;
            }
            else {
                elapsedTime = 0;
                ended = true;
            }
        }
        else {
            ended = false;
        }
    }
}
