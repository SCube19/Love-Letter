using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController : MonoBehaviour
{
    [SerializeField] private GameObject unlocker;
    [SerializeField] private GameObject unlockEffects;

    private void Awake()
    {
        unlocker.GetComponent<ISuccessTrigger>().OnSuccess += UnlockHeart;
        transform.Find("Heart").Find("HeartObject").GetComponent<Heart>().Index = SceneManager.GetInstance().ChamberId;
    }

    private void OnDestroy()
    {
        unlocker.GetComponent<ISuccessTrigger>().OnSuccess -= UnlockHeart;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            unlocker.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            unlocker.GetComponent<HoldPromptController>().ResetState();
            unlocker.SetActive(false);
        }
    }

    private void UnlockHeart()
    {
        unlockEffects.SetActive(true);
        unlockEffects.transform.Find("UnlockParticles").GetComponent<ParticleSystem>().Play();
        transform.Find("Beam").gameObject.GetComponent<ParticleSystem>().Stop();
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Animator>().SetTrigger("unlock");
    }
}
