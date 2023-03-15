using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : MonoBehaviour
{
    [SerializeField] private float invincibilityTime = 0.3f;

    public bool IsInvincible { get; private set; }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void MakeInvincible()
    {
        StartCoroutine(InvincibilityCoroutine());
    }

    private IEnumerator InvincibilityCoroutine()
    {
        IsInvincible = true;
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(invincibilityTime);
        GetComponent<SpriteRenderer>().color = Color.white;
        IsInvincible = false;
    }
}