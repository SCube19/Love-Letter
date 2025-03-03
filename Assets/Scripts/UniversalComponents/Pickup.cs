using UnityEngine;

//TODO: Dependency Injection
public abstract class Pickup : MonoBehaviour 
{
    [SerializeField] private AudioSource sound;
    [SerializeField] private AudioSource alternativeSound;

    public abstract void Affect(GameObject player);
    public void PlaySound()
    {
        if (Random.Range(0, 1) == 0 || alternativeSound == null)
            sound.Play();
        else
            alternativeSound.Play();
    }
}
