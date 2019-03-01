using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : Agent {

    public int score = 0;

    protected override void FiniteStateMachine()
    {
        throw new System.NotImplementedException();
    }

    Transform GetClosestCoin()
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        float minDistance = float.PositiveInfinity;
        GameObject closestCoin = null;
        foreach (GameObject coin in coins)
        {
            float distance = Vector3.Distance(coin.transform.position, transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestCoin = coin;
            }
        }
        return closestCoin.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            score++;
            Debug.Log("Got a coin!");
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Boo")
        {
            Debug.Log("Mamma mia! [score:" + score + "]");
            Destroy(gameObject);
        }
    }
}
