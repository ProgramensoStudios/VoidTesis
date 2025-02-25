using System.Collections;
using UnityEngine;

public class Turbo : MonoBehaviour
{
    public ParticleSystem warp;
    public ParticleSystem warp2;
    public bool isTurbo;
    private Coroutine corTurbo;
    //public AudioSource sfx;

    void Start()
    {
        warp.Stop();
        warp2.Stop();
       // sfx.time = 9f;
    }

    public void TurboPressed()
    {
        if (corTurbo == null)
        {
            corTurbo = StartCoroutine(TurboWait());
           // sfx.Play();
        }
    }
    
    public IEnumerator TurboWait()
    {
        isTurbo = true;
        
        warp.Play();
        warp2.Play();

        yield return new WaitForSeconds(5);
        isTurbo = false;
        
        warp.Stop();
        warp2.Stop();

        corTurbo = null;
    }
    
}