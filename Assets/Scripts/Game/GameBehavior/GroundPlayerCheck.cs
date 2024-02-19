using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPlayerCheck : MonoBehaviour
{
    [SerializeField]private bool isCheck = false;

    public bool IsCheck { get => isCheck; set => isCheck = value; }

    private CheckGroundAtCamView check;
    private void Start()
    {
        this.check = GameObject.FindAnyObjectByType<CheckGroundAtCamView>();
        this.check.FindGroundList.Add(this.gameObject.transform.parent.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<Player>().MoveDir==Vector3.forward)
        {
            isCheck = true;
        }
    }
}
