using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class MidPointDisplacement : ScriptableWizard
{
    // how much randomness do we want to add to the new generated values, the higher the more the terrain will look jagged
    [Range(0, 1)] public float maxRandomAddition = 0.1f;
    // as the algorithm progresses we want to reduce randomness to create a more smooth surface, the higher this value is the more steep (and diverse) the terrain will be
    [Range(0, 1)] public float randomValueReduction = 0.5f;

    //how big we want the terrain to be (resolution wise, you can always stretch it in unity)
    [Range(1, 15)] public int terrainSize = 10;

    //seed so we can reproduce results, if you tick the useRandomSeed variable it will just pick a random one
    public int seed;
    public bool useRandomSeed = false;

    //controls to decide minimum and maximum height of the heightmap, remember that 0 is the minimum and 1 is the maximum 
    [Range(0, 1)] public float minHeight = 0f;
    [Range(0, 1)] public float maxHeight = 0.1f;

    //This adds a menu item to launch our script
    [MenuItem("Terrain/Mid-Point Displacement")]
    public static void CreateWizard(MenuCommand command)
    {
        ScriptableWizard.DisplayWizard("Generate Terrain", typeof(MidPointDisplacement));
    }

    //When you click on "Create" in the wizard window the GenerateTerrain function is called (provided you have selected a GameObject with a terrain component)
    void OnWizardCreate()
    {
        GameObject G = Selection.activeGameObject;
        if (G != null && G.GetComponent<Terrain>())
        {
            GenerateTerrain(G.GetComponent<Terrain>());
        }
        else
        {
            Debug.LogError("You de-selected the object with the terrain");
        }
    }

    // every time you change a value in the wizard the OnWizardUpdate function is called. It will check if you have selected a GameObject with a terrain, if not it will disable the "create" button and give an error message
    void OnWizardUpdate()
    {
        GameObject G = Selection.activeGameObject;
        if (G == null || !G.GetComponent<Terrain>())
        {
            errorString = "You must select a GameObject with a terrain! (change a value in this window to refresh)";
            isValid = false;
        }
        else
        {
            errorString = "";
            isValid = true;
        }
    }

    //Our Generate Terrain function
    public void GenerateTerrain(Terrain t)
    {
        //if we are not using a random seed, set the specified one
        if (!useRandomSeed)
            Random.InitState(seed);

        //Mid-point Displacement needs the array size to be a 2^n + 1
        int side = (int)Mathf.Pow(2, terrainSize) + 1;
        //once we know how long is a side, let's set up the terrain resolution to that
        t.terrainData.heightmapResolution = side;

        // we create the hts matrix, where we will store our new heighmap values. This matrix will have size "side x side"
        float[,] hts = new float[t.terrainData.heightmapWidth, t.terrainData.heightmapHeight];

        //Since we are going to just work in one dimension, let's make an array to use for that
        float[] oneDArray = new float[side];
        //now we initialize the first and last value of the array to a random value
        oneDArray[0] = Random.Range(0, 1);
        oneDArray[side-1] = Random.Range(0, 1);

        //----------------------------------------------------
        //IMPLEMENT MID-POINT DISPLACEMENT algorithm here, on the "oneDArray" array
        //------------------------------------------------------


        // perlin noise alike
        for (int i = 1; i < side - 1; i++)
        {
            float prevPoint = oneDArray[i-1];
            oneDArray[i] = Random.Range(prevPoint - 0.1f, prevPoint + 0.1f);
        }


        // midpoint
        for(int s = side-1; s > 1; s = s/2)
        {
            for(seg = 0; seg < side-1; seg+=s)
            {
                float midpoint = (oneDArray[s] + oneDArray[s + seg+(seg/2)]) / 2
                oneDArray[s] = Random.Range(midpoint*-1f, midpoint*1f) * s;
            }
        }

        /*
        int subdivision = 5;
        float start = oneDArray[0];
        float end = oneDArray[side-1];
        int midindex = int(side / 2) + 1;

        for (int i = 1; i < side - 1; i++)
        {   
            float midpoint = oneDArray[midindex];
            oneDArray[midindex] = Random.Range()
        }

        float CalcMidpoint(float start, float end,) {
            return (start + end) / 2 + Random.Range(0f, 1f);
        }
        */

        //copy the 1D array on all the y coordinates of the heightmap
        for (int y = 0; y < t.terrainData.heightmapHeight; y++)
        {
            for (int x = 0; x < t.terrainData.heightmapWidth; x++)
            {
                hts[x, y] = oneDArray[x];
            }
        }

        //at this point we have a map, but it might include values that are less than 0 or more than 1
        //that is not acceptable so we want to normalize it between 0,1
        //but normalizing it between 0 and 1 means that the maximum value of the heightmap WILL be 1 and the lowest value WILL be 0
        //that will give us a very bumpy map, so we specify a minimum and maximum height, and we normalize the heightmap between those two values
        hts = NormalizeHeightmap(hts, minHeight, maxHeight);

        //finally we pass the heightmap (hts) to the terrain to generate the new terrain
        t.terrainData.SetHeights(0, 0, hts);
    }



    float[,] NormalizeHeightmap(float[,] map, float minValue, float maxValue)
    {
        //normalize between maxHeight and minHeight

        float max = float.NegativeInfinity;
        float min = float.PositiveInfinity;
        //first we go through the matrix and we find the max and min values
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                if (map[x, y] > max)
                {
                    max = map[x, y];
                }
                if (map[x, y] < min)
                {
                    min = map[x, y];
                }
            }
        }

        //once we have max and min we can calculate the range
        float range = max - min;
        //finally we go through the matrix again and normalize the values
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                //normalize from 0 to 1
                float zeroToOneNormalization = (map[x, y] - min) / range;
                //then transform the value to be between minValue and maxValue
                map[x, y] = zeroToOneNormalization * (maxValue - minValue) + minValue;
            }
        }

        return map;
    }
}