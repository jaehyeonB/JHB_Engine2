using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NoiseVoxelMap : MonoBehaviour
{
    public GameObject grassPrefab;
    public GameObject stonePrefab;
    public GameObject waterPrefab;

    public int waterLine = 0;

    public int width = 20;
    public int depth = 20;
    public int maxHeight = 16;

    [SerializeField] float noiseScale = 20f;

    void Start()
    {
        waterLine = maxHeight - Random.Range(8,16);
        Debug.Log(waterLine);

        float offsetx = Random.Range(-9999f, 9999f);
        float offsetz = Random.Range(-9999f, 9999f);

        for(int x = 0; x < width; x++)
        {
            for(int z = 0; z < depth; z++)
            {
                float nx = (x + offsetx) / noiseScale;
                float nz = (z + offsetz) / noiseScale;

                float noise = Mathf.PerlinNoise(nx, nz);

                int h = Mathf.FloorToInt(noise * maxHeight);

                if (h <= 0) continue;

                for(int y = 0; y <= h; y++)
                {
                    if (y == h)
                        PlaceGrass(x, y, z);
                    else
                        PlaceStone(x, y, z);
                }

                for (int i = h+1; i <= waterLine; i++)
                {
                    PlaceWater(x, i, z);
                    /*if (waterLine > h)
                        PlaceWater(x, waterLine, z);*/
                }
            }
        }
    }

    private void PlaceGrass(int x, int y, int z)
    {
        var go = Instantiate(grassPrefab, new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"B_{x}_{y}_{z}";

        var grassB = go.GetComponent<Block>() ?? go.AddComponent<Block>();
        grassB.type = BlockType.Grass;
        grassB.maxHP = 3;
        grassB.dropCount = 1;
        grassB.mineable = true;
    }

    private void PlaceStone(int x, int y, int z)
    {
        var go = Instantiate(stonePrefab, new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"B_{x}_{y}_{z}";

        var stoneB = go.GetComponent<Block>() ?? go.AddComponent<Block>();
        stoneB.type = BlockType.Stone;
        stoneB.maxHP = 9;
        stoneB.dropCount = 1;
        stoneB.mineable = true;
    }

    private void PlaceWater(int x, int y, int z)
    {
        var go = Instantiate(waterPrefab, new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"B_{x}_{y}_{z}";

        var waterB = go.GetComponent<Block>() ?? go.AddComponent<Block>();
        waterB.type = BlockType.Water;
        waterB.maxHP = 999;
        waterB.dropCount = 0;
        waterB.mineable = false;
    }
    void Update()
    {
        
    }
}
