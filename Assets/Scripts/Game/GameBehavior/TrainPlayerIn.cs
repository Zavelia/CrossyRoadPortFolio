using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainPlayerIn : MonoBehaviour
{
    [SerializeField] private bool isCheck = false;

    [SerializeField] Vector3 size;
    [SerializeField] Vector3 center;

    [SerializeField] AudioSource trainSound;

    // Update is called once per frame
    void Update()
    {
        this.PlayerCheck();

        if (isCheck)
        {
            this.trainSound.mute = false;
        }
        else this.trainSound.mute = true;
    }

    private void PlayerCheck()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Player");
        this.center = this.transform.position;

        Collider[] colls = Physics.OverlapBox(center, size, Quaternion.identity, layerMask);
        if (colls.Length > 0) isCheck = true;
        else isCheck = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.center, this.size);
    }
}
