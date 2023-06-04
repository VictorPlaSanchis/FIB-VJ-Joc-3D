using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

        foreach (GameObject container in GameObject.FindGameObjectsWithTag("platformContainer")) {
            foreach (Transform plat in container.transform)
            {

                if (!plat.GetComponent<platformBehaviour>()) continue;
                if (plat.GetComponent<platformBehaviour>().platformType == platformBehaviour.PlatformType.Turn ||
                   plat.GetComponent<platformBehaviour>().platformType == platformBehaviour.PlatformType.TurnAndJump)
                {
                    turns.Add(plat.transform.position);
                }
            }
        }

        foreach (GameObject plat in GameObject.FindGameObjectsWithTag("Platform"))
        {
            if (!plat.GetComponent<platformBehaviour>()) continue;
            if (plat.GetComponent<platformBehaviour>().platformType == platformBehaviour.PlatformType.Turn ||
                plat.GetComponent<platformBehaviour>().platformType == platformBehaviour.PlatformType.TurnAndJump)
            {
                turns.Add(plat.transform.position);
            } 
        }

        turns.Sort(delegate (Vector3 a, Vector3 b)
        {
            if (a.z > b.z) return -1;
            else if (a.z < b.z) return 1;
            else
            {
                if (a.x >= b.x) return -1;
                else return 1;
            }
        });

        for (int i = 0; i < turns.Count-1; i++)
        {
            if (turns[i] == turns[i + 1]) turns.RemoveAt(i);
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

        for (int i = 0; i < turns.Count - 1; i++)
        {
            Debug.DrawLine(turns[i] + new Vector3(0,1,0), turns[i + 1] + new Vector3(0, 1, 0));
        }


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
