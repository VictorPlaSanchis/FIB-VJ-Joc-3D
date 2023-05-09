using System;
using UnityEngine;

public class camaraMovement : MonoBehaviour
{

	public Transform target;
	public Vector3 offset;
	public Vector3 weigths;

	public bool isInitialPositionBeingMade = false;

	public float a = 3, b = 5, c = 3; // parametros del movimiento de la camara en la animacion inicial
    private float time;
	public float initialSize;
	public float finalSize;

    private float animationFadeFunction(float x)
	{
		return Mathf.Pow(a, -(Mathf.Pow(x, 2) + c) / b);
	}

	private void Start()
    {
        this.GetComponent<Camera>().orthographicSize = initialSize;
    }

	public void doInitialAnimation() 
	{
		isInitialPositionBeingMade=true;
		time = -b;
    }

	void Update()
	{
		if (isInitialPositionBeingMade) {
			if(time > b)
            {
                isInitialPositionBeingMade = false;
            }
			time += 0.001f;
            this.GetComponent<Camera>().orthographicSize += (animationFadeFunction(time) + b) * Time.deltaTime;
			transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * 3.0f);
		} 
		else {

			if (!target) Console.Write("TARGET IS NULL!");
			else
			{
				this.transform.position = new Vector3(
					(target.position.x * weigths.x) + offset.x,
					(target.position.y * weigths.y) + offset.y,
					(target.position.z * weigths.z) + offset.z
				);
			}

		}
		
	}

	public void setTarget(Transform target)
	{
		this.target = target;
	}

}
