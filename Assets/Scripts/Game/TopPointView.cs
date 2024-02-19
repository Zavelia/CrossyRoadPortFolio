using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TopPointView : MonoBehaviour
{
    private GameMain gameMain;
    [SerializeField] private TMP_Text topText;
    // Start is called before the first frame update
    void Start()
    {
        this.gameMain = GameObject.FindAnyObjectByType<GameMain>();
        if (this.InitPosZ(this.gameObject) == gameMain.TopPoint)
        {
            this.topText.gameObject.SetActive(true);
            this.topText.text = string.Format("TOP:{0}",this.gameMain.TopPoint);
        }
    }

    private float InitPosZ(GameObject go)
    {
        float posZ = 0;
        if (go.CompareTag("Ground"))
        {
            posZ = this.gameObject.transform.position.z;
        }
        else if (go.CompareTag("TrainGround"))
        {
            posZ = this.gameObject.transform.position.z-0.08f;
        }
        else if (go.CompareTag("CarGround"))
        {
            posZ = this.gameObject.transform.position.z+0.04f;
        }
        else if (go.CompareTag("TreeGround"))
        {
            posZ = this.gameObject.transform.position.z-0.15f;
        }
        else if (go.CompareTag("WaterGround"))
        {
            posZ = this.gameObject.transform.position.z;
        }
        return posZ;
    }

}
