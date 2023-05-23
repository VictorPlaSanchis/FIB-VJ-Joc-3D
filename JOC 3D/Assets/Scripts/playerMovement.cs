using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class playerMovement : MonoBehaviour
{
	public AudioClip jumpSoundClip;
	private AudioSource audioSource;
	private Animator animator;
	private bool keyPressed = false;
	private platformBehaviour lastPlatform;

	private enum MoveDirection { Left, Right };
	private MoveDirection currentDirection;
	private Vector3 initPos;
	public int jumps;
	private float minDistance = 1.25f;
	private float minDistanceForJump = .64f;


	public int maxJumps = 1;
	public float speed = 2.5f;
	public float jumpForce = 1f;


	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		currentDirection = MoveDirection.Right;
		lastPlatform = null;
		animator = GetComponentInChildren<Animator>();
	}

	public void scaleSpeed(float scalar) { 
		this.speed = speed * scalar;
	}

	private float distanceWithLastPlatform()
	{
		if (lastPlatform == null) return float.PositiveInfinity;
		return Vector3.Distance(
				new Vector3(this.transform.position.x, 0.0f, this.transform.position.z),
				new Vector3(lastPlatform.transform.position.x, 0.0f, lastPlatform.transform.position.z)
		);
	}

	void DetectCurrentPlatform()
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 2.0f))
		{
			lastPlatform = hit.collider.gameObject.GetComponent<platformBehaviour>();
			
		}


        // if is on touch
        if (lastPlatform != null && Mathf.Abs(lastPlatform.transform.position.y - this.transform.position.y) <= minDistanceForJump)
        {
            Debug.DrawLine(this.transform.position, this.transform.position + new Vector3(0, 100.0f, 0), Color.red);
            animator.SetBool("Jump", false);
            jumps = 0;
		}
	}
	void PerformAction()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (!keyPressed)
			{
				switch (lastPlatform.platformType)
				{
					case platformBehaviour.PlatformType.Turn:
						if (distanceWithLastPlatform() <= minDistance)
						{
							Turn();
							lastPlatform.alreadyTurned();
						}
						break;
					case platformBehaviour.PlatformType.Jump:
						Jump();
						break;
					case platformBehaviour.PlatformType.TurnAndJump:
                        if (distanceWithLastPlatform() <= minDistance)
                        {
                            Turn();
                            lastPlatform.alreadyTurned();
                        }
                        Jump();
						break;
				}
			}
			keyPressed = true;
		}
		else keyPressed = false;
	}

	private void DetectIfPlayerHaveFall()
	{
		if (lastPlatform)
		{
			if ((this.transform.position.y + 3.0f) < lastPlatform.transform.position.y)
			{
				Die();
			}
		}
	}

	void Move()
	{

		Vector3 direction = new Vector3(0.0f, 0.0f, 0.0f);

		switch (currentDirection)
		{
			case MoveDirection.Right:
				direction = new Vector3(0.0f, 0.0f, -1.0f);
				break;
			case MoveDirection.Left:
				direction = new Vector3(-1.0f, 0.0f, 0.0f);
				break;
			default:
				Debug.Log("Found unknown direction, player direction = 0.");
				break;
		}

		direction.Normalize();

		float realTimeSpeed = speed * Time.deltaTime;
		this.transform.position += direction * realTimeSpeed;
	}


	void Update()
	{

		DetectCurrentPlatform();

		PerformAction();

		Move();

		DetectIfPlayerHaveFall();

	}

	public void Turn()
	{
		Quaternion currentRotation = transform.rotation;
		this.transform.position = new Vector3(
			lastPlatform.transform.position.x,
			this.transform.position.y,
			lastPlatform.transform.position.z
		);

		GameObject.FindGameObjectWithTag("levelController").GetComponent<levelController>().addScore(1);

		Debug.Log("TURNED");

		if (currentDirection == MoveDirection.Right)
		{
			transform.rotation = Quaternion.Euler(0f, 90, 0f) * currentRotation;
			currentDirection = MoveDirection.Left;
			return;
		}
		if (currentDirection == MoveDirection.Left)
		{
			transform.rotation = Quaternion.Euler(0f, -90, 0f) * currentRotation;
			currentDirection = MoveDirection.Right;
			return;
		}

	}

	public void Jump()
	{
		if (jumps >= maxJumps) return;
		audioSource.PlayOneShot(jumpSoundClip);
		Debug.Log("JUMPED");
		this.GetComponent<Rigidbody>().velocity = Vector3.zero;
		this.GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
		jumps++;
		animator.SetBool("Jump", true);
	}

	public void Die()
	{
		GameObject.FindGameObjectWithTag("levelController").GetComponent<levelController>().killPlayer();
	}

}
