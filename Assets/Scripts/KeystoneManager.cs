using CM.Events;
using System.Collections;
using UnityEngine;

public class KeystoneManager : MonoBehaviour
{
	public KeystoneObject[] keystoneObjects;

	private GameManager _gameManager;

	private void Awake()
	{
		_gameManager = FindObjectOfType<GameManager>();
	}

	private void Start()
	{
		EventManager.AddListener<KeystonePressedEvent>(OnKeystonePressed);
		EventManager.AddListener<KeystoneReleasedEvent>(OnKeystoneReleased);

		//StartCoroutine(WaveStartRoutine(KeyCode.A));
	}

	private IEnumerator WaveStartRoutine(KeyCode key)
	{
		yield return new WaitForSeconds(0.5f);

		StartCoroutine(WavePress(key));

		yield return new WaitForSeconds(0.5f);

		StartCoroutine(WaveRelease(key));

		yield return new WaitForSeconds(1f);

		StopAllCoroutines();

		StartCoroutine(WaveStartRoutine(key));
	}

	private IEnumerator WavePress(KeyCode key)
	{
		KeystonePressedEvent keystonePressedEvent = new KeystonePressedEvent(key);
		EventManager.Trigger(keystonePressedEvent);

		for (int i = 0; i < 4; i++)
		{
			Keystone keystone = _gameManager.GetNeighbourKeystone(key, (GameManager.Direction)i);

			if (keystone == null)
				continue;

			yield return new WaitForSeconds(0.04f);

			bool startNextCoroutine = true;

			foreach (KeystoneObject keystoneObject in keystoneObjects)
			{
				if (keystoneObject.key != keystone.Key)
					continue;

				if (keystoneObject.IsPressed)
					startNextCoroutine = false;
			}

			if (!startNextCoroutine)
				continue;

			StartCoroutine(WavePress(keystone.Key));

			keystonePressedEvent = new KeystonePressedEvent(keystone.Key);
			EventManager.Trigger(keystonePressedEvent);
		}
	}

	private IEnumerator WaveRelease(KeyCode key)
	{
		KeystoneReleasedEvent keystoneReleasedEvent = new KeystoneReleasedEvent(key);
		EventManager.Trigger(keystoneReleasedEvent);

		for (int i = 0; i < 4; i++)
		{
			Keystone keystone = _gameManager.GetNeighbourKeystone(key, (GameManager.Direction)i);

			if (keystone == null)
				continue;

			yield return new WaitForSeconds(0.04f);

			bool startNextCoroutine = true;

			foreach (KeystoneObject keystoneObject in keystoneObjects)
			{
				if (keystoneObject.key != keystone.Key)
					continue;

				if (!keystoneObject.IsPressed)
					startNextCoroutine = false;
			}

			if (!startNextCoroutine)
				continue;

			StartCoroutine(WaveRelease(keystone.Key));

			keystoneReleasedEvent = new KeystoneReleasedEvent(keystone.Key);
			EventManager.Trigger(keystoneReleasedEvent);
		}
	}

	private void OnKeystonePressed(object eventData)
	{
		KeystonePressedEvent keystonePressedEvent = eventData as KeystonePressedEvent;

		foreach (KeystoneObject keystoneObject in keystoneObjects)
		{
			if (keystoneObject.key == keystonePressedEvent.Key)
				keystoneObject.Press();
		}
	}

	private void OnKeystoneReleased(object eventData)
	{
		KeystoneReleasedEvent keystoneReleasedEvent = eventData as KeystoneReleasedEvent;

		foreach (KeystoneObject keystoneObject in keystoneObjects)
		{
			if (keystoneObject.key == keystoneReleasedEvent.Key)
				keystoneObject.Release();
		}
	}
}