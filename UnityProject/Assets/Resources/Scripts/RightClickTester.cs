﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class RightClickTester : MonoBehaviour 
{

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(1))
		{
			Plane plane = new Plane(Vector3.up, Vector3.zero);
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			float distance = 0f;
			if (!plane.Raycast(ray, out distance))
				return;
			Vector3 targetPos = ray.origin + ray.direction * distance;
			Do(targetPos); 
		}
	}

	void OnGUI()
	{
	}

	public void Do(Vector3 pos)
	{

	}
}
