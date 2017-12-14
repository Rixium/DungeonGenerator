using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{

	// A list of all of our tiles.
	Tile[,] tiles;

	// Easier to access the required prefabs using the TileType.
	Dictionary<Tile.TileType, GameObject> gameObjects = new Dictionary<Tile.TileType, GameObject>();

	public Map ()
	{
		
	}

	public static Map Create(Tile[,] tiles) {
		// Create a new map, with the tiles and return it.
		Map map = new Map ();
		map.tiles = tiles;
		return map;
	}

	public Vector2 Center {
		get {
			return tiles [50, 50].Position;
		}
	}

	public void Initialize() {
		// Load the tile resource ready for instantiation.
		gameObjects.Add(Tile.TileType.FLOOR, Resources.Load ("Tiles/tilePrefab", typeof(GameObject)) as GameObject);
		gameObjects.Add(Tile.TileType.WALL, Resources.Load ("Tiles/tilePrefab1", typeof(GameObject)) as GameObject);
		// Loop through the tiles and instantiate, at their position.
		foreach (Tile t in tiles) {
			Instantiate (gameObjects[t.GetTileType()], t.Position, Quaternion.identity);
		}
	}

	void Start() {
	
	}

	void Update() {
	}

}