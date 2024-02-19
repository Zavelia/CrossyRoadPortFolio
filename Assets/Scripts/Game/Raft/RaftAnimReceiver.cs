using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftAnimReceiver : MonoBehaviour
{
    public System.Action onFinished;

    public void OnFinished()
    {
        if (this.onFinished != null)
        {
            this.onFinished();
        }
    }

}
