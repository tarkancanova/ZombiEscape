using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(2);
            StartCoroutine(nameof(ReloadScene), 1f);
        }
    }

    private IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }
}
