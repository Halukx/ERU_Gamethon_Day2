using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    CharacterController characterController;
    Vector3 direction;
    bool move;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        Movement();
        RotateWithView();
    }
    void Movement() 
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        direction = new Vector3(horizontal, 0, vertical);
        direction = Camera.main.transform.TransformDirection(direction);
        direction.y = 0;
        direction.Normalize();
        characterController.Move(direction * 5 * Time.deltaTime);
        move = direction.x != 0 || direction.z != 0;
    }
    void RotateWithView()
    {
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 15f * Time.deltaTime);
        }
    }
    private void OnAnimatorMove()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetBool("isWalking", move);
    }
}
