using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Random = System.Random;


public class IslandGenerator : MonoBehaviour
{
    public Tilemap walkableTilemap,solidTilemap;

    public TileBase[] tiles;

    private void Start()
    {
        var chunkData = CreateChunkData(1024, 1024,8,0.35f);
        for (int x = 0; x < chunkData.GetLength(0); x++)
        {
            for (int y = 0; y < chunkData.GetLength(1); y++)
            {
                (chunkData[x,y] == 1 ? solidTilemap : (chunkData[x,y] == 2 ||chunkData[x,y] == 3) ? walkableTilemap : solidTilemap).SetTile(new Vector3Int(x,y,0), GetTile(chunkData[x,y]));
            }
        }
        
    }

    private TileBase GetTile(int i)
    {
        return tiles[i];
    }


    private float[,] MaskIsland(float[,] noise)
    {
        var island_size = noise.GetLength(0);
        for (int x = 0; x < noise.GetLength(0); x++)
        {
            for (int y = 0; y < noise.GetLength(1); y++)
            {
                
                float distance_x = Math.Abs(x - island_size * 0.5f);
                float distance_y = Math.Abs(y - island_size * 0.5f);
                var distance = Math.Sqrt(distance_x*distance_x + distance_y*distance_y); 

                float max_width = island_size * 0.5f - 20.0f;
                float delta = (float)(distance / max_width);
                float gradient = delta * delta;

                noise[x,y] *= Math.Max(0.0f, 1.0f - gradient);
                
            }

        }

        return noise;
    }

    private int[,] CreateChunkData(int w, int h,int octaveCount,float persistance)
    {
        var noise = MaskIsland(GeneratePerlinNoise(GenerateWithNoise(w, h), octaveCount, persistance));
        
        var tiles = new int[w, h];
        for (int x = 0; x < noise.GetLength(0); x++)
        {
            for (int y = 0; y < noise.GetLength(1); y++)
            {
                var n = noise[x, y];
                if (n < 0.2f) tiles[x, y] = 1;
                else if (n > 0.2f && n < 0.3f) tiles[x, y] = 2;
                else if (n > 0.3f && n < 0.6f) tiles[x, y] = 3;
                else if (n > 0.6f && n <= 1f) tiles[x, y] = 4;
                else tiles[x, y] = 0;
            }
        }


        return tiles;
    }


    private float[,] GenerateWithNoise(int w, int h)
    {
        Random random = new Random();
        float[,] noise = new float[w, h];
        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                noise[x, y] = (float) (random.NextDouble() % 1);
            }
        }

        return noise;
    }

    private float[,] GenerateSmoothNoise(float[,] baseNoise, int octave)
    {
        var w = baseNoise.GetLength(0);
        var h = baseNoise.GetLength(1);

        var smoothNoise = new float[w, h];
        var samplePeriod = 1 << octave;
        var sampleFrequency = 1f / samplePeriod;

        for (var x = 0; x < w; x++)
        {
            var sampleI0 = (x / samplePeriod) * samplePeriod;
            var sampleI1 = (sampleI0 + samplePeriod) % w; //wrap around
            var horizontalBlend = (x - sampleI0) * sampleFrequency;
            for (var y = 0; y < h; y++)
            {
                var sampleJ0 = (y / samplePeriod) * samplePeriod;
                var sampleJ1 = (sampleJ0 + samplePeriod) % h; //wrap around
                var verticalBlend = (y - sampleJ0) * sampleFrequency;

                var top = Interpolate(baseNoise[sampleI0, sampleJ0],
                    baseNoise[sampleI1, sampleJ0], horizontalBlend);

                var bottom = Interpolate(baseNoise[sampleI0, sampleJ1],
                    baseNoise[sampleI1, sampleJ1], horizontalBlend);

                smoothNoise[x, y] = Interpolate(top, bottom, verticalBlend);
            }
        }

        return smoothNoise;
    }

    private float Interpolate(float x0, float x1, float alpha)
    {
        return x0 * (1 - alpha) + alpha * x1;
    }

    private float[,] GeneratePerlinNoise(float[,] baseNoise, int octaveCount, float persistance)
    {
        var width = baseNoise.GetLength(0);
        var height = baseNoise.GetLength(1);

        var smoothNoise = new float[octaveCount, width, height]; 


        for (var i = 0; i < octaveCount; i++)
        {
            var smooth = GenerateSmoothNoise(baseNoise, i);
            for (var x = 0; x < smoothNoise.GetLength(1); x++)
            {
                for (var y = 0; y < smoothNoise.GetLength(2); y++)
                {
                    smoothNoise[i, x, y] = smooth[x, y];
                }
            }
        }

        var perlinNoise = new float[width, height];
        var amplitude = 1.0f;
        var totalAmplitude = 0.0f;

        for (var octave = octaveCount - 1; octave >= 0; octave--)
        {
            amplitude *= persistance;
            totalAmplitude += amplitude;

            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    perlinNoise[i, j] += smoothNoise[octave, i, j] * amplitude;
                }
            }
        }

        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                perlinNoise[i, j] /= totalAmplitude;
            }
        }

        return perlinNoise;
    }
}