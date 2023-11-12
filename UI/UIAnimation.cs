using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName = "UIAnimation/FadeINOUT")]
public class UIAnimation : ScriptableObject
{
    public float fadeTime;
    public float duration;
    public RectTransform RectPos;
    public void PanelFadeIn(GameObject obj)
    {
        obj.SetActive(true);
        obj.GetComponent<CanvasGroup>().alpha = 0;
        obj.transform.localPosition = new Vector3(0, -1000, 0f);
        obj.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 84), fadeTime, false).SetEase(Ease.InOutQuint);
        obj.GetComponent<CanvasGroup>().DOFade(1, 1);
    }
    public void Close(GameObject obj)
    {
        obj.SetActive(false);
    }
    public void PanelFadeOut(GameObject obj)
    {

        obj.GetComponent<CanvasGroup>().alpha = 1;
       
        obj.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, -1000), fadeTime, false).SetEase(Ease.InOutQuint);
        obj.GetComponent<CanvasGroup>().DOFade(0, fadeTime);


    }

    public void PanelBigger(GameObject obj)
    {
        obj.SetActive(true);
       
        obj.GetComponent<CanvasGroup>().alpha = 0;
       
        obj.transform.DOScale(1, .5f).SetDelay(.5f).SetEase(Ease.OutBack);
        obj.GetComponent<CanvasGroup>().DOFade(1, fadeTime);
    }
    public void PanelLittle(GameObject obj)
    {
        obj.GetComponent<CanvasGroup>().alpha = 1;
        obj.transform.DOScale(0, .3f).SetEase(Ease.OutBack);
        obj.GetComponent<CanvasGroup>().DOFade(0, fadeTime);

    }
}
