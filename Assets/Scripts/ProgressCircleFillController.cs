using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressCircleFillController : MonoBehaviour
{
    private GameObject circle;
    private GameObject fill;
    private GameObject particles;
    public bool Stop { get; set; }

    private Color originalColor;
    void Awake()
    {
        Stop = false;
    }

    private void Start()
    {
        circle = transform.Find("Circle").gameObject;
        fill = circle.transform.Find("Fill").gameObject;
        particles = circle.transform.Find("Fill Particles").gameObject;
        originalColor = fill.GetComponent<Image>().color;
    }

    void Update()
    {
        if (Stop)
            return;

        float progress = GetComponent<HoldInteractable>().Progress();
        if (progress > 0f)
        {
            circle.SetActive(true);
            fill.GetComponent<Image>().fillAmount = progress;
            particles.transform.eulerAngles = new Vector3(0, 0, Mathf.Max(-360, -progress * 360));
        }
        else
            circle.SetActive(false);
    }

    public void SetFillColor(Color c)
    {
        fill.GetComponent<Image>().color = c;
    }

    public void ResetState()
    {
        fill.GetComponent<Image>().color = originalColor;
        Stop = false;
        particles.transform.eulerAngles = Vector3.zero;
        fill.GetComponent<Image>().fillAmount = 0;
        circle.SetActive(false);
        particles.SetActive(true);
        GetComponent<HoldInteractable>().ResetState();
    }

    public void EnableParticles(bool enable)
    {
        particles.SetActive(enable);
    }
}
