using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PATH : MonoBehaviour
{
	public Transform target;

	public GameObject point;
	public bool UseTrail = true;
	public Transform Trail;//
	private Vector3 lastpoint;
	// Use this for initialization
	void Start()
	{
		lastpoint = target.position;
	}

	// Update is called once per frame
	void Update()
	{
		if (UseTrail)
		{
			Trail.transform.position = target.position - Vector3.forward * 2; ;
		}
	}
	public void CreatePoint(Vector3 rotation)
	{
		if (UseTrail) return;	
		if (Vector3.Distance(lastpoint, target.position) < 0.5)
		{
			return;
		}
		GameObject pointt = Instantiate(point);
		point.transform.position = target.position;
		point.transform.eulerAngles = rotation;
		lastpoint = target.position;
	}
}
