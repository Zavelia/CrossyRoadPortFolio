using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGroundAtCamView : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private List<GameObject> findGroundList = null;

    public List<GameObject> FindGroundList { get => findGroundList; set => findGroundList = value; }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < FindGroundList.Count; i++)
        {
            if (this.findGroundList[i] != null)
            {
                Vector3 viewPos = cam.WorldToViewportPoint(findGroundList[i].transform.position);
                if (viewPos.y < -1f)
                {
                    this.findGroundList.Remove(findGroundList[i]);
                    //Destroy(findGroundList[i]);
                    findGroundList[i].gameObject.SetActive(false);
                }
            }
        }
    }
}
