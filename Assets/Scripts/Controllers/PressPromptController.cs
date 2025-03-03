using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(PressInteractable))]
public class PressPromptController : MonoBehaviour, ISuccessTrigger
{
    [SerializeField] private KeyCode toPress;
    [SerializeField] private AudioSource successSound;  

    public event Action OnSuccess;

    void Awake()
    {
        toPress = toPress != KeyCode.None ? toPress : ControlsManager.GetInstance().ControlMap[ControlsManager.Controls.Interact];
        GetComponent<PressInteractable>().SetPress(toPress);
        GetComponent<PressInteractable>().OnSuccess += TriggerSuccessState;
        transform.Find("PressImage").GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = toPress.ToString();
    }

    private void OnDestroy()
    {
        GetComponent<PressInteractable>().OnSuccess -= TriggerSuccessState;
    }

    private void TriggerSuccessState()
    {
        OnSuccess?.Invoke();
        if (successSound != null)
            successSound.Play();
        GetComponent<Animator>().SetTrigger("success");
    }

    public void ResetState() { }
}
