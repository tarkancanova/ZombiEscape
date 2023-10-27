using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class EnemyAI : MonoBehaviour
{
    private GameObject _playerObject;
    private Vector3 _playerPosition;
    private GameObject[] _enemyObjects;
    public static float _moveSpeed = 1.5f;
    private Animator _anim;
    private Vector3 _relativePos;
    private Quaternion rotation;
    public static bool gameOver = false;
    HeadRaycastControl headRaycastControl;
    FootRaycastControl footRaycastControl;
    private Rigidbody _rigidbody;
    private Vector3 _jumpVector = new Vector3(0, 0.1f, 0);
    bool didSlideRecently = false;


    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _playerObject = GameObject.FindGameObjectWithTag("Player").gameObject;
        _enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemyObject in _enemyObjects)
        {
            footRaycastControl = enemyObject.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(1).gameObject.GetComponent<FootRaycastControl>();
            headRaycastControl = enemyObject.transform.GetChild(0).GetChild(2).GetChild(0).
                GetChild(0).GetChild(3).GetChild(0).GetChild(3).GetComponent<HeadRaycastControl>();

        }
        _rigidbody = GetComponent<Rigidbody>();
        

    }

    private void Update()
    {

        EnemyRun();

        EnemyJump();

        EnemySlide();

        StartCoroutine(BackToOriginalValues());
    }
    




    private void EnemyRun()
    {
        _playerPosition = _playerObject.transform.position;

        transform.position = Vector3.MoveTowards(transform.position, _playerPosition, _moveSpeed * Time.deltaTime);

        _anim.SetFloat("Blend", 0f);

        _relativePos = _playerPosition - transform.position;

        rotation = Quaternion.LookRotation(_relativePos, Vector3.up);

        transform.rotation = rotation;
    }
    

    private void EnemyJump()
    {
        if (footRaycastControl.EnemyAIJumpCheck())
        {
            _rigidbody.AddForce(_jumpVector, ForceMode.Impulse);
        }
    }

    private void EnemySlide()
    {
        if (headRaycastControl.EnemyAISlideCheck())
        {
            _rigidbody.transform.localScale = transform.localScale * 0.7f;
            _moveSpeed = 5f;
            didSlideRecently = true;
        }
    }

    IEnumerator BackToOriginalValues()
    {
        if (didSlideRecently)
        {
            yield return new WaitForSeconds(0.6f);
            _rigidbody.transform.localScale = Vector3.one;
            _moveSpeed = 1.5f;
        }
    }
}
