using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRaycastControl : MonoBehaviour
{
    Rigidbody ancestorRigidbody = null;
    private GameObject _playerObject;
    private Vector3 _jumpForce;
    RaycastHit hit;
    Vector3 originalScale;
    Transform thisParent;
    private double timeOfAction;
    private Vector3 _backForce;
    private Vector3 _fwdForce;
    public static List<Vector3> directionVectors;


    private void Awake()
    {
        _playerObject = GameObject.Find("Player");
        thisParent = transform;
        while (thisParent != null && ancestorRigidbody == null)
        {
            ancestorRigidbody = thisParent.GetComponent<Rigidbody>();
            thisParent = thisParent.parent;
        }

        originalScale = Vector3.one;
        _backForce = new Vector3(0, 0, -1);
        _fwdForce = new Vector3(0, 0, 1);

        directionVectors = new List<Vector3> { Vector3.back, Vector3.forward, Vector3.left, Vector3.right, new Vector3(1, 0, 1), 
            new Vector3(-1, 0, -1), new Vector3(1, 0, -1), new Vector3(-1, 0, 1) };
    }



    private IEnumerator BackToOriginalSize()
    {

        yield return new WaitForSeconds(0.8f);
        ancestorRigidbody.transform.localScale = Vector3.one;
        EnemyAI._moveSpeed = 1.5f;

    }


    public bool EnemyAISlideCheck()
    {
        foreach (Vector3 directionVector in directionVectors)
        {

             if (Physics.Raycast(origin: transform.position, direction: directionVector, out hit, maxDistance: 0.8f))
             {
                 if (hit.transform.CompareTag("HigherObstacle"))
                 {
                    return true;
                 }
             }

        }

        return false;
    }
}
