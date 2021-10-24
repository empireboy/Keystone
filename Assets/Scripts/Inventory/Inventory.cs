using System.Collections.Generic;
using UnityEngine;

public abstract class Inventory<T> : MonoBehaviour
{
	protected List<T> items = new List<T>();

	public abstract void Add(T item);
	public abstract void Remove(T item);
}