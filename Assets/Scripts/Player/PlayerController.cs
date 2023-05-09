using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private Animator anim;
	private CharacterController controller;

	public float speed = 7;
	public float turnSpeed = 200;
	public float jumpForce = 10;
	private Vector3 moveDirection = Vector3.zero;
	public float gravity = 20.0f;

	void Start()
	{
		controller = GetComponent<CharacterController>();
		anim = gameObject.GetComponentInChildren<Animator>();
	}

	void Update()
	{
		if (Input.GetKey("w"))
		{
			anim.SetInteger("AnimationPar", 1);
		}
		else
		{
			anim.SetInteger("AnimationPar", 0);
		}

		if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
		{
			moveDirection.y = transform.position.y * jumpForce * gravity * Time.deltaTime;
			anim.SetBool("OnJump", true);
		}

		if (controller.isGrounded)
		{
			moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;
		}


		float turn = Input.GetAxis("Horizontal");
		transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
		controller.Move(moveDirection * Time.deltaTime);
		moveDirection.y -= gravity * Time.deltaTime;
	}
}
