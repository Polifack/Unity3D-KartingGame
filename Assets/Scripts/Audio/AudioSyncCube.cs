using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Class that syncs audio with scale

public class AudioSyncCube : AudioSyncer
{
    public float beatHeight;  //Desired object height when the music is beating
    public float restHeight;  //Desired object height when the music is not beating

    private Vector3 beatScale;
    private Vector3 restScale;

    Material cubeMaterial;


    private void Awake()
    {
        Setup();

    }
    public void Setup()
    {
        beatScale = transform.localScale;
        beatScale.y = beatHeight;

        restScale = transform.localScale;
        restScale.y = restHeight;
    }

    private IEnumerator MoveToScale(Vector3 scale)
    {
        Vector3 currentScale = transform.localScale;
        Vector3 initialScale = currentScale;

        float timer = 0f;

        while (currentScale != scale)
        {
            //Lerp into the beat scale
            currentScale = Vector3.Lerp(initialScale, beatScale, timer / beatTime);
            timer += Time.deltaTime;

            transform.localScale = currentScale;
            yield return null;
        }

        isBeating = false;
    }


    public override void onUpdate()
    {
        base.onUpdate();

        //If we are not beating we lerp into the rest scale
        if (!isBeating)
            transform.localScale = Vector3.Lerp(transform.localScale, restScale, restTime * Time.deltaTime);
    }

    public override void onBeat()
    {
        base.onBeat();
        //Stop a scaling corroutine if it is already running and start a new one
        StopCoroutine("MoveToScale");
        StartCoroutine("MoveToScale", beatScale);
    }
}
