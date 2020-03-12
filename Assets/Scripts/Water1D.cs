using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Water1D : MonoBehaviour {

	public GameObject waterPrefab;
	public GameObject velocityPrefab;

	float[] u;
	float[] v;

	GameObject[] water;
	GameObject[] velocity;
	int width = 25;
	int count = 0;
	float cellSize = .2f;

	float velocityYOffset = 3;

	// Use this for initialization
	void Start () {

		u = new float[width];
		v = new float[width];
		water = new GameObject[width];
		velocity = new GameObject[width];

		for (int x = 0; x < width; x++) {
				u[x] = 1;//Mathf.Sin(x * cellSize);

				v[x] = 0;
				water[x] = Instantiate (waterPrefab, new Vector3 (x * cellSize, 0, 0), Quaternion.identity);
				velocity[x] = Instantiate (velocityPrefab, new Vector3 (x * cellSize, -velocityYOffset, 0), Quaternion.identity);
			
		}
		u[width/2] = 0;

	}

	// Update is called once per frame
	void Update () {

		count++;

		if (count % 1 == 0) {

			for (int x = 0; x < width; x++) {
					
					if (x == 0) {
						v[x] += .99f * (u[x+1] - u[x]) * 2*Time.deltaTime;
					}
					else if (x == width - 1) {
						v[x] += .99f * (u[x-1] - u[x]) * 2*Time.deltaTime;
					}
					else {
						v[x] += .99f * (u[x+1] + u[x-1] - 2*u[x]) * 2*Time.deltaTime; //Acceleration to update velocity
					}
					// v[x] *= .99f; //Dampener
					u[x] += v[x] * 2*Time.deltaTime; //Update position based on velocity

					u[x] = Mathf.Max(0, u[x]); //Prevent water from leaving the bottom

					float value = u[x];
					GameObject w = water[x];
					w.transform.localScale = new Vector3 (cellSize, value, 1);
					w.transform.position = new Vector3(x * cellSize, value/2, 0);
					
					// GameObject vel = velocity[x];
					// value = v[x] * 25;
					// vel.transform.localScale = new Vector3 (cellSize, value, 1);
					// vel.transform.position = new Vector3(x * cellSize, value/2 - velocityYOffset, 0);
					//water[x,y].transform.

				}
		}

	}

	float getU (int x) {

		int i = x;
		i = Mathf.Max (0, i);
		i = Mathf.Min (width - 1, i);

		return u[i];

	}

	public void AddForce(){
		u[width/2] = 0;
	}
}
