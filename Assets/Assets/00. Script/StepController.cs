using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StepController : MonoBehaviour
{
    private AudioSource ad;

    [SerializeField]
    private List<AudioClip> clips = new List<AudioClip>();

    private void Awake()
    {
        ad = GetComponent<AudioSource>();
    }

    #region Player

    public void OnStep()
    {
        OnStepSound();
        StepPoolManager.Instance.GetStep();
    }

    #endregion

    #region Enemy

    public void OnEnemyStep(Transform tr, int index)
    {
        OnStepSound();

        if(index == 0)
            StepPoolManager.Instance.GetEnemyLeftStep(tr);
        else
            StepPoolManager.Instance.GetEnemyRightStep(tr);
    }

    #endregion

    #region Common

    private void OnStepSound()
    {
        int index = Random.Range(0, clips.Count);

        ad.clip = clips[index];
        ad.Play();
    }

    #endregion
}
