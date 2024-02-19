using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainAlert : MonoBehaviour
{
    [SerializeField] private GameObject trainAlert;
    [SerializeField] private float intervelTime;
    [SerializeField] private float elapsedTime = 0.0f;
    [SerializeField] private float elapsedTime2 = 0.0f;

    [SerializeField] private AudioSource trainAlertSound;

    [SerializeField] private bool isCheck = false;

    [SerializeField] Vector3 size;
    [SerializeField] Vector3 center;

    [SerializeField] Transform centerTransform;

    private System.Action onActive;

    private void Start()
    {
        this.onActive = () => {
            if (isCheck) {
                this.trainAlertSound.Play();
            } 
        };
    }
    // Update is called once per frame
    void Update()
    {
        this.PlayerCheck();
        this.elapsedTime += Time.deltaTime;
        if(elapsedTime > intervelTime)
        {
            this.elapsedTime2 += Time.deltaTime;
            if (this.elapsedTime2 < 2.0f)
            {
                this.trainAlert.SetActive(true);
                if(this.elapsedTime2 > 0.0f && this.elapsedTime2 <0.05f)
                {
                    this.onActive();
                }
            }
            else 
            {
                this.trainAlert.SetActive(false);
                this.elapsedTime = 0.0f;
                this.elapsedTime2 = 0.0f;
            }
        }
    }

    private void PlayerCheck()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Player");
        this.center = this.centerTransform.position;

        Collider[] colls = Physics.OverlapBox(center, size, Quaternion.identity, layerMask);
        if (colls.Length > 0) isCheck = true;
        else isCheck = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(this.center, this.size);
    }


}
