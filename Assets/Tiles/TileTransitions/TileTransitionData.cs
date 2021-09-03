using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class TileTransitionData
{
   public int self;

   //<OtherTileId,Bitmask>
   public  Dictionary<int, int> bitmask = new Dictionary<int, int>();
   
   /*
    * 0,1,2
    * 3,  4
    * 5,6,7
    * 
    */
   public TileTransitionData(int[] adjacent,int self)
   {
      
      this.self = self;

      

      if (self != adjacent[3])
      {
         AddBit(adjacent[3],8);
      }

      if (self != adjacent[6])
      {
         AddBit(adjacent[6],4);
      }

      if (self != adjacent[4])
      {
         
         AddBit(adjacent[4],2);
      }

      if (self != adjacent[1])
      {
         AddBit(adjacent[1],1);
      }


      if (bitmask.Count == 0)
      {
         if (self != adjacent[0])
         {
            AddBit(adjacent[0],19);
         }

         if (self != adjacent[2])
         {
            AddBit(adjacent[2],18);
         }

         if (self != adjacent[5])
         {
         
            AddBit(adjacent[5],17);
         }

         if (self != adjacent[7])
         {
            AddBit(adjacent[7],16);
         }
      }

      
      
   }
   private void AddBit(int key, int value)
   {
      if (!bitmask.ContainsKey(key))
      {
         bitmask.Add(key,0);
      }

      bitmask[key] += value;
   }
   
   
}
