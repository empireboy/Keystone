using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(KeystoneObject))]
public class KeystoneText : MonoBehaviour
{
	[SerializeField]
	private bool _startEmpty = true;

	[SerializeField]
	private string _overrideKey = "";

	[SerializeField]
	private Text _text;

	private KeystoneObject _keystoneObject;

	public void ShowKey()
	{
		_text.text = (string.IsNullOrEmpty(_overrideKey)) ? _keystoneObject.key.ToString() : _overrideKey;
	}

	public void HideKey()
	{
		_text.text = "";
	}

	private void Awake()
	{
		_keystoneObject = GetComponent<KeystoneObject>();

		if (_startEmpty)
			HideKey();
		else
			ShowKey();
	}
}