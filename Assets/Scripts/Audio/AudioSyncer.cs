using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSyncer : MonoBehaviour
{
    private float currentAudioValue;
    private float previousAudioValue;
    private float timer;

    public float bias; //Determines what spectrum is going to trigger a beat
    public float timeStep; //Determines minimum interval between each beat
    public float beatTime; //Determines the time that the visualization has for each beat
    public float restTime; //Determines the time that the visualization has for going into rest

    protected bool isBeating; //Determines if the object is currently beating

    public virtual void onBeat() {
        timer = 0;
        isBeating = true;
    }
    public virtual void onUpdate() {
        previousAudioValue = currentAudioValue;
        currentAudioValue = AudioSpectrum.currentSpectrumValue;

        //If during this frame the value went below the bias
        if (previousAudioValue > bias && currentAudioValue <= bias)
        {
            if (timer > timeStep) onBeat();
        }

        //If during this frame the value went above the bias
        if (previousAudioValue<=bias && currentAudioValue > bias)
        {
            if (timer > timeStep) onBeat();
        }
        timer += Time.deltaTime;
    }

    void Update()
    {
        onUpdate();
    }
}
