using UnityEngine;

public class Keystone
{
	public KeyCode Key { get; private set; }
	public Vector3 Position { get; set; }

	public Keystone(KeyCode key)
	{
		Key = key;
	}
}