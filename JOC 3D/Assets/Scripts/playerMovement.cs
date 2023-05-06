using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class playerMovement : MonoBehaviour
{

	private bool keyPressed = false;
	private platformBehaviour lastPlatform;

	private enum MoveDirection { Left, Right };
	private MoveDirection currentDirection;
	private Vector3 initPos;
	private int jumps;
	private float minDistance = 1.0f;


    public int maxJumps = 2;
    public float speed = 2.5f;
	public float jumpForce = 1f;


    void Start()
	{
		currentDirection = MoveDirection.Right;
		lastPlatform = null;
	}

	public void scaleSpeed(float scalar) { 
		this.speed = speed * scalar;
	}

    void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (!keyPressed)
			{
				if (Vector3.Distance(this.transform.position, lastPlatform.transform.position) <= minDistance) {
                    switch (lastPlatform.platformType)
                    {
                        case platformBehaviour.PlatformType.Turn:
                            Turn();
                            lastPlatform.alreadyTurned();
                            break;
                        case platformBehaviour.PlatformType.Jump:
                            Jump();
                            break;
                        case platformBehaviour.PlatformType.TurnAndJump:
                            Turn();
                            lastPlatform.alreadyTurned();
                            Jump();
                            break;

                    }
                } 
			}
			keyPressed = true;
		}
		else keyPressed = false;

		Move();

		if(lastPlatform)
		{
			if((this.transform.position.y + 3.0f) < lastPlatform.transform.position.y)
			{
				Die();
			}
		}

	}

	void Move()
	{

		Vector3 direction = new Vector3(0.0f, 0.0f,	0.0f);

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

	void Turn()
	{
		this.transform.position = new Vector3(
			lastPlatform.transform.position.x,
			this.transform.position.y,
			lastPlatform.transform.position.z
		);

        Debug.Log("TURNED");

        if (currentDirection == MoveDirection.Right)
		{
			currentDirection = MoveDirection.Left;
			return;
		}
		if (currentDirection == MoveDirection.Left)
		{
			currentDirection = MoveDirection.Right;
			return;
		}

	}

	void Jump()
	{
		if (jumps >= maxJumps) return;
		Debug.Log("JUMPED");
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        jumps++;

    }

	public void Die()
	{
		GameObject.Find("LevelController").GetComponent<levelController>().killPlayer();
	}

    void OnCollisionEnter(Collision collision)
    {
		lastPlatform = collision.gameObject.GetComponent<platformBehaviour>();
		jumps = 0;
    }

}