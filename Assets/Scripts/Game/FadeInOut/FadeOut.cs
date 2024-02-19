using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    [SerializeField] private Image dim;
    [SerializeField] private Restart restart;

    private System.Action onFadeOutComplete;
    // Start is called before the first frame update
    void Start()
    {
        Image image = this.dim.GetComponent<Image>();
        image.enabled = true;
        this.onFadeOutComplete = () => {
            restart.ReStart();
        };
        this.StartCoroutine(this.Fadeout());
    }

    private IEnumerator Fadeout()
    {
        Color color = this.dim.color;
        //¾îµÎ¾îÁü
        while (true)
        {
            color.a += 0.01f;
            this.dim.color = color;

            if (this.dim.color.a >= 1)
            {
                break;
            }
            yield return null;
        }
        Debug.Log("FadeOut Complete!");
        this.onFadeOutComplete();
    }
}
