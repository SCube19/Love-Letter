using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(PressInteractable))]
public class PressPromptController : MonoBehaviour
{
    [SerializeField] KeyCode toPress;
    [SerializeField] private AudioSource successSound;

    private 
    void Awake()
    {

        GetComponent<PressInteractable>().SetPress(toPress);
        GetComponent<PressInteractable>().OnSuccess += TriggerSuccessState;
        transform.Find("PressImage").GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = toPress.ToString();
    }

    private void TriggerSuccessState()
    {
        successSound.Play();
        GetComponent<Animator>().SetTrigger("success");
    }
}
