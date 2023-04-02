using DG.Tweening;

using UnityEngine;

public static class CanvasGroupExtensionTweening
{
	public static Tween DoFade(this CanvasGroup canvasGroup,float to,float duration,Ease ease= Ease.OutQuad)
	{
		return DOTween.To( ()=>canvasGroup.alpha,(x)=> {  canvasGroup.alpha = x; }, to, duration).SetEase(ease);
	}
}
