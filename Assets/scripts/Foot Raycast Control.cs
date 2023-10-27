using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootRaycastControl : MonoBehaviour
{

    Rigidbody ancestorRigidbody = null;
    private GameObject _playerObject;
    private Vector3 _jumpForce;
    RaycastHit hit;
    private List<Vector3> directions = new List<Vector3>() { Vector3.forward, Vector3.back }; 
    float maxDistance = 0.8f;

    private void Awake()
    {
        _playerObject = GameObject.Find("Player");
        Transform thisParent = transform;
        while (thisParent != null && ancestorRigidbody == null)
        {
            ancestorRigidbody = thisParent.GetComponent<Rigidbody>();
            thisParent = thisParent.parent;
        }
    }


    public bool EnemyAIJumpCheck()
    {
        foreach (Vector3 direction in directions)
        {
            if (Physics.Raycast(transform.position, direction, out hit, maxDistance))
            {
                if (hit.collider.CompareTag("Obstacle"))
                {
                    return true; // Return true if there's an obstacle with the "Obstacle" tag in any direction
                }
            }
        }

        return false; // Return false if no obstacles with the "Obstacle" tag were found in any direction
    }
}
