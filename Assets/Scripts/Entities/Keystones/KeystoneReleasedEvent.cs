using System;
using UnityEngine;

public class KeystoneReleasedEvent : EventArgs
{
	public KeyCode Key { get; private set; }

	public KeystoneReleasedEvent(KeyCode key)
	{
		Key = key;
	}
}