using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	Map map;

	public int mapWidth = 100;
	public int mapHeight = 100;
	public int roomCount = 10;

	// Use this for initialization
	void Start () {
		// We create the map using the MapGenerator, passing in a width, a height and a number of rooms to generate.
		map = Map.Create(MapGenerator.Generate (mapWidth, mapHeight, roomCount));
		// Initialize the map, and spawn all the tiles.
		map.Initialize ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}