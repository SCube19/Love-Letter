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
        particles = circle.transform.Find("Fill Particles")?.gameObject;
        originalColor = fill.GetComponent<Image>().color;
    }

    void Update()
    {
        if (Stop)
            return;

        float progress = GetComponent<IProgressProvider>().Progress();
        if (progress > 0f)
        {
            circle.SetActive(true);
            fill.GetComponent<Image>().fillAmount = progress;
            if (particles)
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
        if (particles)
        {
            particles.transform.eulerAngles = Vector3.zero;
            particles.SetActive(true);
        }
        fill.GetComponent<Image>().fillAmount = 0;
        circle.SetActive(false);
        GetComponent<IProgressProvider>().ResetState();
    }

    public void EnableParticles(bool enable)
    {
        if (particles)
            particles.SetActive(enable);
    }
}
