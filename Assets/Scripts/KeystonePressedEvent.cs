using System;
using UnityEngine;

public class KeystonePressedEvent : EventArgs
{
	public KeyCode Key { get; private set; }

	public KeystonePressedEvent(KeyCode key)
	{
		Key = key;
	}
}