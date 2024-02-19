using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] groundPrefabs;
    [SerializeField] private float elapsedTime = 0.0f;
    [SerializeField] private float intervalTime;
    [SerializeField] private Transform grounds;

    private int posZ = 3;

    public int PosZ { get => posZ;}

    public void CreateGround()
    {
        int rnd = 0;
        this.elapsedTime += Time.deltaTime;
        if(this.elapsedTime > intervalTime)
        {
            int rndGround = Random.Range(1,101); //1~100
            if (rndGround > 0 && rndGround < 93) //1~92 
            {
                rnd = Random.Range(0, groundPrefabs.Length - 2); //0~groundPrefabs.Length-1
            }
            else if(rndGround >=93 && rndGround<96)// 93~95 3%
            {
                Debug.Log("Water");
                rnd = groundPrefabs.Length - 2;
            }
            else
            {
                // 프리팹 마지막 요소가 TrainGround, 즉 TrainGround가 5프로의 확률로 나오도록 조정
                Debug.Log("Train");
                rnd = groundPrefabs.Length - 1; 
            }
            GameObject groundGo = Instantiate(groundPrefabs[rnd],grounds);
            this.SetGroundPosition(groundGo);
            this.posZ++;
            this.elapsedTime = 0.0f;
        }
    }

    private void SetGroundPosition(GameObject go)
    {
        if (go.CompareTag("Ground"))
        {
            go.transform.position = new Vector3(0, 0, posZ);
        }
        else if (go.CompareTag("TrainGround"))
        {
            go.transform.position = new Vector3(0, 0, posZ+0.08f);
        }
        else if (go.CompareTag("CarGround"))
        {
            go.transform.position = new Vector3(0, 0, posZ-0.04f);
        }
        else if (go.CompareTag("TreeGround"))
        {
            go.transform.position = new Vector3(0, 0, posZ + 0.15f);
        }
        else if (go.CompareTag("WaterGround"))
        {
            go.transform.position = new Vector3(0, -0.3f, posZ);
        }
    }
}
