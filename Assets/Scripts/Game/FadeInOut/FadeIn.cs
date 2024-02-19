using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    [SerializeField] private Image dim;

    private System.Action onFadeInComplete;
    public System.Action onFadeInComplete1;
    // Start is called before the first frame update
    void Start()
    {
        this.onFadeInComplete = () => {
            Debug.Log("FadeIn complete!");
            //this.StageOneMain.SetActive(true);
            Image image = this.dim.GetComponent<Image>();
            image.enabled = false;
            Destroy(this.gameObject, 1.0f);
        };
        this.StartCoroutine(this.Fadein());
    }

    private IEnumerator Fadein()
    {
        Color color = this.dim.color;

        while (true)
        {
            color.a -= 0.01f;
            this.dim.color = color;

            if (this.dim.color.a <= 0)
            {
                break;
            }
            yield return null;
        }
        this.onFadeInComplete();
        this.onFadeInComplete1();
    }
}
