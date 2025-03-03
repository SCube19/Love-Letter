using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect : MonoBehaviour
{
    [SerializeField] private GameObject echoObject;
    [SerializeField] private float echoTime = 0.3f;
    [SerializeField] private float aliveTime = 1f;

    private Coroutine echoCoroutine;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play()
    {
        echoCoroutine = StartCoroutine(Echo());
    }

    public void Stop()
    {
        StopCoroutine(echoCoroutine);
    }

    private IEnumerator Echo()
    {
        Debug.Log("echostart");
        while (true)
        {
            GameObject echo = Instantiate(echoObject, transform);
            echo.transform.parent = null;
            Destroy(echo, aliveTime);
            yield return new WaitForSecondsRealtime(echoTime);
        }
    }
}