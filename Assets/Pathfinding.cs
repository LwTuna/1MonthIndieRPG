
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Pathfinding
{
        public static List<Vector2> GetLargestRoom(int[,] map,int roomTile)
        {
                return GetAllRooms(map, roomTile).OrderBy(list => list.Count).Last();
        }
        
        public static List<List<Vector2>> GetAllRooms(int[,] map,int roomTile)
        {
                List<List<Vector2>> rooms = new List<List<Vector2>>();

                for (var x = 0; x < map.GetLength(0); x++)
                {
                        for (var y = 0; y< map.GetLength(1); y++)
                        {
                                if (ContainsPosition(new Vector2(x, y), rooms)) continue;
                                if (map[x, y] != roomTile) continue;
                                
                                List<Vector2> room = new List<Vector2>();
                                FloodFill(new Vector2Int(x, y), room, map,roomTile);
                                rooms.Add(room);
                        }     
                }

                return rooms;
        }

        private static void FloodFill(Vector2Int pos,List<Vector2> room, int[,] map,int roomTile)
        {
                
                if(pos.x<0 || pos.x>=map.GetLength(0) ||pos.y<0 || pos.y>=map.GetLength(1 )) return;
                if (map[pos.x, pos.y] != roomTile) return;
                if (room.Contains(pos)) return;
                room.Add(pos);
                        
                FloodFill(new Vector2Int(pos.x,pos.y+1),room,map,roomTile);
                FloodFill(new Vector2Int(pos.x,pos.y-1),room,map,roomTile);
                FloodFill(new Vector2Int(pos.x+1,pos.y),room,map,roomTile);
                FloodFill(new Vector2Int(pos.x-1,pos.y),room,map,roomTile);
        }

        private static bool ContainsPosition(Vector2 pos, List<List<Vector2>> rooms)
        {
                return rooms.Any(room => room.Contains(pos));
        }
}