using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HoldPromptController : MonoBehaviour
{
    [SerializeField] KeyCode toHold;
    [SerializeField] private AudioSource successSound;

    private GameObject progressCircle;
    private GameObject successParticles;
    void Awake()
    {
        progressCircle = transform.Find("ProgressCircle").gameObject;
        progressCircle.GetComponent<HoldInteractable>().SetHold(toHold);
        progressCircle.GetComponent<HoldInteractable>().OnSuccess += TriggerSuccessState;
        successParticles = transform.Find("SuccessParticles").gameObject;
        transform.Find("HoldImage").GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = toHold.ToString();
    }

    private void OnDestroy()
    {
        transform.Find("ProgressCircle").GetComponent<HoldInteractable>().OnSuccess -= TriggerSuccessState;
    }

    private void TriggerSuccessState()
    {
        progressCircle.GetComponent<HoldInteractable>().enabled = false;
        progressCircle.GetComponent<ProgressCircleFillController>().EnableParticles(false);
        progressCircle.GetComponent<ProgressCircleFillController>().Stop = true;
        progressCircle.GetComponent<ProgressCircleFillController>().SetFillColor(new Color(255, 103, 0));
        successParticles.GetComponent<ParticleSystem>().Play();
        successSound.Play();
        GetComponent<Animator>().SetTrigger("success");
    }

    public void ResetState()
    {
        progressCircle.GetComponent<HoldInteractable>().enabled = true;
        progressCircle.GetComponent<ProgressCircleFillController>().ResetState();
    }
}
