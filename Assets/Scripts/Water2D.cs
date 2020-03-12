using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water2D : MonoBehaviour {

	public GameObject prefab;

	float[, ] u;
	float[, ] v;

	GameObject[, ] water;
	int width = 50;
	int height = 150;
	int count = 0;
	float cellSize = .2f;

	// Use this for initialization
	void Start () {

		u = new float[width, height];
		v = new float[width, height];
		water = new GameObject[width, height];

		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				u[x, y] = Mathf.Sin(x * cellSize) + Mathf.Cos(y * cellSize);
				v[x, y] = 0;
				water[x, y] = Instantiate (prefab, new Vector3 (x * cellSize, 0, y * cellSize), Quaternion.identity);
			}
		}

	}

	// Update is called once per frame
	void Update () {

		count++;

		if (count % 1 == 0) {

			for (int x = 0; x < width; x++) {
				for (int y = 0; y < height; y++) {

					if (x == 0 && y == 0) {
						v[x, y] += .99f * (u[x+1, y] + u[x, y+1] - 2*u[x,y]) * Time.deltaTime;
					}
					else if (x == 0 && y == height - 1) {
						v[x, y] += .99f * (u[x+1, y] + u[x, y-1] - 2*u[x,y]) * Time.deltaTime;
					}
					else if (x == width - 1 && y == 0) {
						v[x, y] += .99f * (u[x-1, y] + u[x, y+1] - 2*u[x,y]) * Time.deltaTime;
					}
					else if (x == width - 1 && y == height - 1) {
						v[x, y] += .99f * (u[x-1, y] + u[x, y-1] - 2*u[x,y]) * Time.deltaTime;
					}
					else if (x == 0) {
						v[x, y] += .99f * (u[x+1, y] + u[x, y+1] + u[x, y-1] - 3*u[x,y]) * Time.deltaTime;
					}
					else if (x == width - 1) {
						v[x, y] += .99f * (u[x-1, y] + u[x, y+1] + u[x, y-1] - 3*u[x,y]) * Time.deltaTime;
					}
					else if (y == 0) {
						v[x, y] += .99f * (u[x+1, y] + u[x-1, y] + u[x, y+1] - 3*u[x,y]) * Time.deltaTime;
					}
					else if (y == height - 1) {
						v[x, y] += .99f * (u[x+1, y] + u[x-1, y] + u[x, y-1] - 3*u[x,y]) * Time.deltaTime;
					}
					else {
						v[x, y] += .99f * (u[x+1, y] + u[x-1, y] + u[x, y+1] + u[x, y-1] - 4*u[x,y]) * Time.deltaTime;
					}

					// v[x, y] *= .99f; //Dampener
					u[x, y] += v[x, y] * Time.deltaTime; //Update position based on velocity
					u[x,y] = Mathf.Max(0, u[x,y]); //Prevent water going out the bottom

					float value = u[x, y];
					GameObject w = water[x, y];
					w.transform.localScale = new Vector3 (cellSize, value, cellSize);
					w.transform.position = new Vector3(x * cellSize, value/2, y * cellSize);
					//water[x,y].transform.

				}
			}
		}

	}

	float getU (int x, int y) {

		int i = x;
		int j = y;
		i = Mathf.Max (0, i);
		i = Mathf.Min (width - 1, i);
		j = Mathf.Max (0, j);
		j = Mathf.Min (height - 1, j);

		return u[i, j];

	}

	public void AddForce(){
		u[width/2, height/2] = 0;
	}
}
