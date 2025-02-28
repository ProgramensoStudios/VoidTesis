using System;
using System.Collections;
using UnityEngine;

public class DestroyObj : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        int layer = LayerMask.NameToLayer("Bullet");
        if (other.gameObject.layer == layer)
        {
            gameObject.SetActive(false);
            StartCoroutine(SetUpAgain());
        }
    }

    private IEnumerator SetUpAgain()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(true);
    }
}
