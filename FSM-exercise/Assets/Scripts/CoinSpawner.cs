using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour {

    public GameObject coinPrefab;
    public float secondsBetweenCoins = 2f;

	// Use this for initialization
	void Start () {
        InvokeRepeating("SpawnCoin", 0, secondsBetweenCoins);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpawnCoin()
    {
        float x = Random.Range(-9f, 9f);
        float z = Random.Range(-9f, 9f);
        Instantiate(coinPrefab, new Vector3(x, 0.5f, z), Quaternion.identity);
    }
}
