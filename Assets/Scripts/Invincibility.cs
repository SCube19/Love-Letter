using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : MonoBehaviour
{
    [SerializeField] private float invincibilityTime = 0.3f;
    [SerializeField] private float hitMaterialTime = 0.1f;
    [SerializeField] private Material invincibilityMaterial;
    [SerializeField] private Material invincibilityHitMaterial;
    //for futuere me to solve
    [SerializeField] private Hitbox hitProvider;

    private Material originalMaterial;

    public bool IsInvincible { get; private set; }
    void Start()
    {
        originalMaterial = GetComponent<SpriteRenderer>().material;
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
        GetComponent<SpriteRenderer>().material = invincibilityMaterial;
        yield return new WaitForSeconds(invincibilityTime);
        GetComponent<SpriteRenderer>().material = originalMaterial;
        IsInvincible = false;
    }
}