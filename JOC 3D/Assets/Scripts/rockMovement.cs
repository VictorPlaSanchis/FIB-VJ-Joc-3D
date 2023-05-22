using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockMovement : MonoBehaviour
{

    public List<Vector3> turns;

    private enum MoveDirection { Left, Right };
    private MoveDirection currentDirection = MoveDirection.Right;

    private float minDistance = 1f;

    public float speed = 2.5f;
    float rotationSpeed = 2f;

    private int currentTurn;

    void fillTurns()
    {
        foreach (GameObject t in GameObject.FindGameObjectsWithTag("Platform")) {
            if(!t.GetComponent<platformBehaviour>()) continue;
            if(t.GetComponent<platformBehaviour>().platformType == platformBehaviour.PlatformType.Turn ||
               t.GetComponent<platformBehaviour>().platformType == platformBehaviour.PlatformType.TurnAndJump) {
                turns.Add(t.transform.position);
            }
        }
    }

    void Start()
    {
        fillTurns();
        currentTurn = 0;
    }
    void Move()
    {

        Vector3 direction = new Vector3(0.0f, 0.0f, 0.0f);
        Quaternion currentRotation = transform.rotation;
        switch (currentDirection)
        {
            case MoveDirection.Right:
                direction = new Vector3(0.0f, 0.0f, -1.0f);
                transform.rotation = Quaternion.Euler(-(rotationSpeed * 360f) * Time.deltaTime, 0f, 0f) * currentRotation;
                break;
            case MoveDirection.Left:
                direction = new Vector3(-1.0f, 0.0f, 0.0f);
                transform.rotation = Quaternion.Euler(0f, 0f, (rotationSpeed * 360f) * Time.deltaTime) * currentRotation;
                break;
            default:
                Debug.Log("Found unknown direction, player direction = 0.");
                break;
        }

        direction.Normalize();

        float realTimeSpeed = speed * Time.deltaTime;
        this.transform.position += direction * realTimeSpeed;
    }

    public void Turn()
    {
        Quaternion currentRotation = transform.rotation;
        this.transform.position = turns[currentTurn] + new Vector3(0.0f, 0.65f, 0.0f);

        if (currentDirection == MoveDirection.Right)
        {
            transform.rotation = Quaternion.Euler(0f, -90, 0f) * currentRotation;
            currentDirection = MoveDirection.Left;
            return;
        }
        if (currentDirection == MoveDirection.Left)
        {
            transform.rotation = Quaternion.Euler(0f, 90, 0f) * currentRotation;
            currentDirection = MoveDirection.Right;
            return;
        }

    }


    void Update()
    {

        if (Vector3.Distance(turns[currentTurn], this.transform.position) < minDistance)
        {
            Turn();
            currentTurn++;
            if(currentTurn >= turns.Count)
            {
                if (currentDirection == MoveDirection.Right) 
                {
                    turns.Add(turns[turns.Count - 1] + new Vector3(0.0f, 0.0f, -1000.0f));
                }
                if (currentDirection == MoveDirection.Left)
                {
                    turns.Add(turns[turns.Count - 1] + new Vector3(-1000.0f, 0.0f, 0.0f));
                }
            }
        }

        Move();

    }
}
