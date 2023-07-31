using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    [SerializeField] private GameObject trigger;
    [SerializeField] private int chamberId;
    [SerializeField] private string sceneName;

    private void Awake()
    {
        trigger.GetComponent<ISuccessTrigger>().OnSuccess += EnterDoor;
    }

    private void OnDestroy()
    {
        trigger.GetComponent<ISuccessTrigger>().OnSuccess -= EnterDoor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            trigger.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            trigger.SetActive(false);
        
    }

    private void EnterDoor()
    {
        SceneManager.GetInstance().ChamberId = chamberId;
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
