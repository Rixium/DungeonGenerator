using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator {

	static int maxWidth = 30, minWidth = 15;
	static int maxHeight = 30, minHeight = 15;

	// Use this for initialization and map generation.
	public static Tile[,] Generate (int mapWidth, int mapHeight, int roomCount) {
		// Return a created map.
		return CreateMap (mapWidth, mapHeight, roomCount);
	}

	// Creates the map.
	static Tile[,] CreateMap(int mapWidth, int mapHeight, int roomCount) {
		// Make a tile array the size of the passed in width and height.
		Tile[,] tiles = new Tile[mapWidth, mapHeight];

		// Iterate through and generate an empty tile for each position in the array.
		for (int i = 0; i < mapWidth; i++) {
			for (int j = 0; j < mapHeight; j++) {
				tiles [i, j] = new Tile (i, j, Tile.TileType.WALL);	
			}
		}

		// We need an ArrayList for this so we can remove rectangles more easily later.
		ArrayList rooms = new ArrayList ();

		// Generate all the rooms in the map.
		rooms = generateRooms (roomCount, mapWidth, mapHeight);

		// Iterate through each rectangle, their position to their right position, and create a black tile at each position within the rectangle.
		foreach (Rect r in rooms) {
			for (int i = (int)r.x; i < (int)r.xMax; i++) {
				for (int j = (int)r.y; j < (int)r.yMax; j++) {
					tiles [i, j] = new Tile (i, j, Tile.TileType.FLOOR);
				}
			}
		}

		linkRooms (tiles, rooms);

		// finally return all the generated tiles.
		return tiles;
	}

	static void linkRooms(Tile[,] tiles, ArrayList rooms) {
		for (int i = 0; i < rooms.Count - 1; i += 2) {
			for (int j = i + 1; j < rooms.Count; j++) {
				Rect r1 = (Rect)rooms [i];
				Rect r2 = (Rect)rooms [j];

				int lastX = 0;

				int endX = buildHorizontal (r1, r2, tiles);
				buildVertical (r1, r2, tiles, endX);
			}
		}
	}

	static void buildVertical(Rect r1, Rect r2, Tile[,] tiles, int x) {
		if (r2.center.y < r1.center.y) {
			for (int y = (int)r1.center.y; y > (int)r2.center.y; y--) {
				tiles [x, y] = new Tile (x, y, Tile.TileType.FLOOR);
			}
		} else if (r2.center.y > r1.center.y) {
			for (int y = (int)r1.center.y; y < (int)r2.center.y; y++) {
				tiles [x, y] = new Tile (x, y, Tile.TileType.FLOOR);
			}
		}
	}

	static int buildHorizontal(Rect r1, Rect r2, Tile[,] tiles) {
		int lastX = 0;
		if (r2.center.x > r1.center.x) {
			for (int x = (int)r1.center.x; x < (int)r2.center.x; x++) {
				tiles [x, (int)r1.center.y] = new Tile (x, (int)r1.center.y, Tile.TileType.FLOOR);
				lastX = x;
			}

		} else if (r2.center.x < r1.center.x) {
			for (int x = (int)r1.center.x; x > (int)r2.center.x; x--) {
				tiles [x, (int)r1.center.y] = new Tile (x, (int)r1.center.y, Tile.TileType.FLOOR);
				lastX = x;
			}
		}
		return lastX; 
	}

	static ArrayList generateRooms(int roomCount, int mapWidth, int mapHeight) {
		// Room list for us to store rectangles in.
		ArrayList rooms = new ArrayList ();

		// Generate rectangles for the rooms.
		for (int i = 0; i < roomCount; i++) {
			// We generate a random room here, at a random x position 0 - mapWidth, and y 0 - mapHeight.
			rooms.Add(GenerateRoom (mapWidth, mapHeight));
		}
		// Check rooms that are colliding and delete them.
		rooms = DeleteCollidingRooms (rooms);
		// Finally return all the rooms we have left.
		return rooms;
	}

	static ArrayList DeleteCollidingRooms(ArrayList rooms) {
		// Iterate through the list.
		for (int i = 0; i < rooms.Count - 1; i++) {
			// Check each room again.
			for (int j = 0; j < rooms.Count - 1; j++) {
				// We don't need to check if we're colliding with ourself.
				if (i == j) {
					continue;
				}
				// Temporary storage rects for our room rectangles.
				Rect iRect = (Rect) rooms[i];
				Rect jRect = (Rect) rooms[j];

				// Check if the rectangles overlap, and if they do, then remove them from the arraylist.
				if (iRect.Overlaps(jRect)) {
					rooms.RemoveAt (i);
				}
			}
		}

		// finally return all rooms that don't overlap.
		return rooms;
	}

	static Rect GenerateRoom(int mapWidth, int mapHeight) {
		// Random width and height for the room.
		int width = Random.Range (minWidth, maxWidth);
		int height = Random.Range (minHeight, maxHeight);

		// Random x and y, within the range 0 and the generated width and height.
		int x = Random.Range (0, mapWidth - width);
		int y = Random.Range (0, mapHeight - height);

		// Return our lovely new rectangle.
		return new Rect (x, y, width, height);
	}
}
