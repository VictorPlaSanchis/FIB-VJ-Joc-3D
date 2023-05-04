using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class sin : MonoBehaviour
{

	public float radius;
	public float speed;
	private float time = 0.0f;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
    {
		this.transform.position = new Vector3(
			Mathf.Sin(time * speed) * radius,
			Mathf.Cos(time * speed) * radius,
			0.0f
		);

		this.time += Time.deltaTime;

	}
}
