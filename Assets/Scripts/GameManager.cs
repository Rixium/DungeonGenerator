using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	Map map;

	public Camera mainCamera;

	public int mapWidth = 100;
	public int mapHeight = 100;
	public int roomCount = 10;

	// Use this for initialization
	void Start () {
		// We create the map using the MapGenerator, passing in a width, a height and a number of rooms to generate.
		map = Map.Create(MapGenerator.Generate (mapWidth, mapHeight, roomCount));
		// Initialize the map, and spawn all the tiles.
		map.Initialize ();

		mainCamera.transform.position = new Vector3 (map.Center.x, map.Center.y, mainCamera.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.A)) {
			mainCamera.transform.position = new Vector3 (mainCamera.transform.position.x - 10 * Time.deltaTime, mainCamera.transform.position.y, mainCamera.transform.position.z);
		} else if (Input.GetKey (KeyCode.D)) {
			mainCamera.transform.position = new Vector3 (mainCamera.transform.position.x + 10 * Time.deltaTime, mainCamera.transform.position.y, mainCamera.transform.position.z);
		}

		if (Input.GetKey (KeyCode.W)) {
			mainCamera.transform.position = new Vector3 (mainCamera.transform.position.x, mainCamera.transform.position.y + 10 * Time.deltaTime, mainCamera.transform.position.z);
		} else if (Input.GetKey (KeyCode.S)) {
			mainCamera.transform.position = new Vector3 (mainCamera.transform.position.x, mainCamera.transform.position.y - 10 * Time.deltaTime, mainCamera.transform.position.z);
		}
	}
}