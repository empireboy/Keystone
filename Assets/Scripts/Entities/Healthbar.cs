using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
	[SerializeField]
	private Slider _slider;

	private float _maxHealth;

	private void Start()
	{
		DefaultHealth defaultHealth = transform.root.GetComponent<DefaultHealth>();

		defaultHealth.OnTakeDamage += OnTakeDamage;
		_maxHealth = defaultHealth.MaxHealth;
	}

	private void OnTakeDamage(float health, float damage)
	{
		_slider.value = health / _maxHealth;
	}
}