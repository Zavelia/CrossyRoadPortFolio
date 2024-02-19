using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform coins;
    [SerializeField] private int maxCoins;
    public void Init()
    {
        for(int i=0;i<maxCoins; i++)
        {
            int rndX = Random.Range(-4, 7);
            int rndZ = Random.Range(0, 500);
            GameObject coinGo = Instantiate(coinPrefab, coins);
            coinGo.transform.position = new Vector3(rndX,0.5f,rndZ-0.733f);
        }
    }

}
