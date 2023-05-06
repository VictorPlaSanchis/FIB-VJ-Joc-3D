using System;
using UnityEngine;

public class camaraMovement : MonoBehaviour
{

	public Transform target;
	public Vector3 offset;
	public Vector3 weigths;

	void Update()
	{
		if (!target) Console.Write("TARGET IS NULL!");
		else {
			this.transform.position = new Vector3(
				(target.position.x * weigths.x) + offset.x,
                (target.position.y * weigths.y) + offset.y,
                (target.position.z * weigths.z) + offset.z
            );
		}
	}

	public void setTarget(Transform target)
	{
		this.target = target;
	}

}
