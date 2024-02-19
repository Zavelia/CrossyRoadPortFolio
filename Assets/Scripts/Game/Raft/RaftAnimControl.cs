using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftAnimControl : MonoBehaviour
{
    private Animator anim;
    private RaftAnimReceiver receiver;
    [SerializeField] private bool isCheck = false;
    [SerializeField] RaftAnimControl[] raftAnimControls;

    public bool IsCheck { get => isCheck; set => isCheck = value; }

    // Start is called before the first frame update
    void Start()
    {
        this.anim = GetComponent<Animator>();
        this.receiver =GetComponent<RaftAnimReceiver>();
        this.receiver.onFinished = () =>
        {
            this.anim.SetTrigger("Over");
        };
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.anim.SetTrigger("PlayerIn");
            for(int i=0; i< raftAnimControls.Length; i++)
            {
                raftAnimControls[i].IsCheck = true;
            }
        }
    }
}
