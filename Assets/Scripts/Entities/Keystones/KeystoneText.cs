using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(KeystoneObject))]
public class KeystoneText : MonoBehaviour
{
	[SerializeField]
	private bool _startEmpty = true;

	[SerializeField]
	private Text _text;

	private KeystoneObject _keystoneObject;

	public void ShowKey()
	{
		_text.text = _keystoneObject.key.ToString();
	}

	public void HideKey()
	{
		_text.text = "";
	}

	private void Awake()
	{
		_keystoneObject = GetComponent<KeystoneObject>();
	}

	private void Start()
	{
		if (_startEmpty)
			HideKey();
		else
			ShowKey();
	}
}