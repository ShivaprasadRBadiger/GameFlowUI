using System.Threading.Tasks;
using GameFlowUI.Core;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(Button))]
public abstract class HomeScreenFooterButtonBase : GFUIWidget
{
	private CanvasGroup _canvasGroup;
	protected Button _button;

	public override void Initialize()
	{
		base.Initialize();
		_canvasGroup = GetComponent<CanvasGroup>();
		_button = GetComponent<Button>();
		_button.onClick.AddListener(OnClickHandler);
	}

	protected abstract void OnClickHandler();

	public async override Task AnimateIn()
	{
		var sequence = DOTween.Sequence();
		sequence.Append(transform.DOScale(Vector3.one, 0.5f));
		sequence.Join(_canvasGroup.DoFade(1f, 0.5f));
		await sequence.AsyncWaitForCompletion();
	}

	public async override Task AnimateOut()
	{
		var sequence = DOTween.Sequence();
		sequence.Append(transform.DOScale(Vector3.one*0.5f, 0.5f));
		sequence.Join(_canvasGroup.DoFade(0, 0.5f));
		await sequence.AsyncWaitForCompletion();
	}

	public override void Hide()
	{
		gameObject.SetActive(false);
	}

	public override void Show()
	{
		gameObject.SetActive(true);
	}
	public override void Dispose()
	{
		base.Dispose();
		_button.onClick.RemoveListener(OnClickHandler);
	}
}
