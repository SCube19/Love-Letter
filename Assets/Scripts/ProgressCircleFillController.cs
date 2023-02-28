using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressCircleFillController : MonoBehaviour
{
    private GameObject background;
    private GameObject fill;

    private bool disableUpdate = false;

    void Awake()
    {
        GetComponent<HoldInteractable>().OnHoldSuccess += TriggerEndAnimation;
    }

    private void Start()
    {
        background = transform.Find("Background").gameObject;
        fill = transform.Find("Fill").gameObject;
    }

    private void OnDestroy()
    {
        GetComponent<HoldInteractable>().OnHoldSuccess -= TriggerEndAnimation;
    }
    void Update()
    {
        float progress = GetComponent<HoldInteractable>().Progress();
        if (progress < 0.0001f)
        {
            background.SetActive(false);
            fill.SetActive(false);
            return;
        }
        else
        {
            background.SetActive(true);
            fill.SetActive(true);
            fill.GetComponent<Image>().fillAmount = progress;
        }
    }
    private void TriggerEndAnimation()
    {
        disableUpdate = true;
        transform.localScale = Vector3.one * 2;
    }

}
