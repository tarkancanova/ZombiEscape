using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    private CharacterController controller;
    private Animator _animator;
    [SerializeField] GameObject player;
    [SerializeField] Camera cam;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    private InputManager _inputManager;
    private Vector3 playerVelocityT;
    private double playerVelocitySum;
    private bool notSliding = true;


    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        _inputManager = InputManager.Instance;
        _animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        PlayerMove();
    }


    private IEnumerator BackToOriginalSize()
    {
        yield return new WaitForSeconds(1f);
        playerSpeed = 18f;
        transform.localScale = Vector3.one;
        notSliding = true;
    }


    private void PlayerMove()
    {
        playerSpeed = 3f;
        controller.transform.rotation = new Quaternion(0, cam.transform.rotation.y, 0, cam.transform.rotation.w);
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        if (_inputManager != null) // Add a null check here
        {
            Vector2 movement = _inputManager.GetPlayerMovement();
            Vector3 move = new(movement.x, 0f, movement.y);
            var lastMove = cam.transform.forward * move.z + cam.transform.right * move.x;
            lastMove.y = 0f;
            controller.Move(playerSpeed * Time.deltaTime * lastMove);

            //if (move != Vector3.zero)
            //{
            //    gameObject.transform.forward = move;
            //}

            // Changes the height position of the player..
            if (_inputManager.PlayerJumped() && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }
        }



        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        playerVelocityT = playerVelocity * Time.deltaTime;
        playerVelocitySum = Mathf.Abs(playerVelocityT.x) + Mathf.Abs(playerVelocityT.y) + Mathf.Abs(playerVelocityT.z);

        SetAnimatorValues();

        if (notSliding && Input.GetKey(KeyCode.LeftShift))
        {
            transform.localScale = Vector3.one * 0.5f;
            playerSpeed = playerSpeed * 2;
            notSliding = false;
            StartCoroutine(nameof(BackToOriginalSize));

        }
    }

    private void SetAnimatorValues()
    {
        if (_inputManager.GetPlayerMovement().x == 0 && _inputManager.GetPlayerMovement().y == 0)
        {
            _animator.SetFloat("Run", 0f);
        }

        else
        {
            _animator.SetFloat("Run", 1f);
        }
    }
}


