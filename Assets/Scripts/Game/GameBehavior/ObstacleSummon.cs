using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSummon : MonoBehaviour
{
    [SerializeField] private Transform[] summonSpots;
    [SerializeField] private Transform[] defaultSummonSpots;
    [SerializeField] private GameObject[] obstaclePrefabs;
    // Start is called before the first frame update
    void Start()
    {
        Summon();
        DefaultSummon();
    }


    private void Summon()
    {
        for(int i=0;i<summonSpots.Length;i++)
        {
            int obstacleRnd = Random.Range(1, 11); //1~10 
            if (obstacleRnd > 7) //30%의 확률로 장애물 생성
            {
                int rnd = Random.Range(0, 3);//0~2
                GameObject go = Instantiate(obstaclePrefabs[rnd], this.transform);
                this.InitSummonPos(rnd, i, go);
            }
        }
    }

    private void DefaultSummon()
    {
        for(int i=0; i < defaultSummonSpots.Length; i++)
        {
            int rnd = Random.Range(0, 3);//0~2
            GameObject go = Instantiate(obstaclePrefabs[rnd], this.transform);
            this.InitDefaultSummonPos(rnd, i, go);
        }
        
    }

    private void InitSummonPos(int num,int i, GameObject go)
    {
        if (num == 0 || num==1) //나무
        {
            go.transform.position = new Vector3(this.summonSpots[i].position.x,
                this.summonSpots[i].position.y, this.summonSpots[i].position.z-0.09f);
        }
        else if(num == 2) //돌 
        {
            go.transform.position = new Vector3(this.summonSpots[i].position.x + 0.35f,
                this.summonSpots[i].position.y + 0.5f, this.summonSpots[i].position.z - 0.02f);
        }
    }

    private void InitDefaultSummonPos(int num, int i, GameObject go)
    {
        if (num == 0 || num == 1)
        {
            go.transform.position = new Vector3(this.defaultSummonSpots[i].position.x,
                this.defaultSummonSpots[i].position.y, this.defaultSummonSpots[i].position.z - 0.09f);
        }
        else if (num == 2) //돌 
        {
            go.transform.position = new Vector3(this.defaultSummonSpots[i].position.x + 0.35f,
                this.defaultSummonSpots[i].position.y + 0.5f, this.defaultSummonSpots[i].position.z - 0.02f);
        }
    }
}
