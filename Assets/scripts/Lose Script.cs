using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScript : MonoBehaviour
{

    private GameObject _playerObject;

    private void Awake()
    {
        _playerObject = GameObject.Find("Player");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject == _playerObject)
        {
            Destroy(collision.gameObject);
            SceneManager.LoadScene(1);
            StartCoroutine(ReloadScene());


        }

    }

    private IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }


}
