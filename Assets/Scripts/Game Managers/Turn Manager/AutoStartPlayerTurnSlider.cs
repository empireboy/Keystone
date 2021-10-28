using CM.Events;
using UnityEngine;
using UnityEngine.UI;

public class AutoStartPlayerTurnSlider : MonoBehaviour
{
	[SerializeField]
	private Slider _slider;

	private float _automationTime;
	private float _currentAutomationTime;

	private void Awake()
	{
		EventManager.AddListener<TurnEndEvent>(OnTurnEnd);

		FindObjectOfType<AutoStartPlayerTurn>().OnStartAutomation += OnStartAutomation;

		_slider.gameObject.SetActive(false);
	}

	private void Update()
	{
		if (!_slider.gameObject.activeInHierarchy)
			return;

		_currentAutomationTime -= Time.deltaTime;
		_slider.value = Mathf.Lerp(0, 1, _currentAutomationTime / _automationTime);
	}

	private void OnStartAutomation(float time)
	{
		_automationTime = time;
		_slider.value = 1;
		_currentAutomationTime = _automationTime;

		_slider.gameObject.SetActive(true);
	}

	private void OnTurnEnd(object eventData)
	{
		TurnEndEvent turnEndEvent = eventData as TurnEndEvent;

		if (turnEndEvent.Turn != TurnManager.TurnStates.PlayerTurn)
			return;

		_slider.gameObject.SetActive(false);
	}
}