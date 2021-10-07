using CM.Events;
using UnityEngine;
using UnityEngine.Events;

public class DefaultHealth : MonoBehaviour, IDamageable
{
	public event FloatEvent OnHealthChanged;
	public event DamageEvent OnTakeDamage;

	public float Health
	{
		get
		{
			return _health;
		}
		protected set
		{
			_health = value;

			OnHealthChanged?.Invoke(_health);
			HealthChangedEvent.Invoke();
		}
	}

	public float MaxHealth
	{
		get
		{
			return _maxHealth;
		}
	}

	public bool destroyOnDeath = true;

	[SerializeField]
	private float _health = 100;

	[SerializeField]
	private float _maxHealth = 100;

	public UnityEvent HealthChangedEvent;
	public UnityEvent TakeDamageEvent;
	public UnityEvent KillEvent;

	public void TakeDamage(int damage)
	{
		Health = Mathf.Max(Health - damage, 0);

		if (Health <= 0)
			Kill();

		OnTakeDamage?.Invoke(Health, damage);
		TakeDamageEvent.Invoke();
	}

	public void Kill()
	{
		KillEvent.Invoke();

		if (destroyOnDeath)
			Destroy(gameObject);
	}

	private void OnValidate()
	{
		_maxHealth = Mathf.Max(_maxHealth, 0);
		_health = Mathf.Clamp(_health, 0, _maxHealth);
	}
}