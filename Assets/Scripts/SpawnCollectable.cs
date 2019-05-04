using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollectable : MonoBehaviour {

    public int xRange = 10;
    public int yRange = 10;
    public int numberOfObjects = 16;

    public GameObject[] objects;

    float worldScreenHeight;
    float worldScreenWidth;

    void Awake()
    {
        worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        xRange = Mathf.FloorToInt(worldScreenWidth) / 2;
        yRange = Mathf.FloorToInt(worldScreenHeight) / 2;

    }

    // Use this for initialization
    void Start () {
        Spawn();
	}
	
	void Spawn()
    {
        for (int i = 0; i < numberOfObjects; ++i)
        {
            Vector2 spawnLocation = new Vector2(Random.Range(xRange, -xRange), Random.Range(yRange - 10, -yRange));

            int objectPick = Random.Range(1, objects.Length);
            Instantiate (objects[objectPick], spawnLocation, Quaternion.identity);
        }
    }
}
