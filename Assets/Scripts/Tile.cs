using System;
using UnityEngine;

public class Tile
{

	// x and y position of the tile.
	private Vector2 position;
	private TileType tileType;

	// We will store all the tile types here, and then add them to the prefab dictionary in the map.
	public enum TileType {
		FLOOR,
		WALL
	}

	// We need an x and y position for where we need to instantiate the tile, and a tile type.
	public Tile (int x, int y, TileType tileType)
	{
		this.position = new Vector2 (x, y);
		this.tileType = tileType;
	}

	public Vector2 Position {
		get {
			return position;
		}
	}


	// For use in the instantiation loop.
	public TileType GetTileType() {
		return this.tileType;
	}

}