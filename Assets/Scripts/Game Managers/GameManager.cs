using CM.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private EnemyManager _enemyManager;

	[SerializeField]
	private List<GameObject> _entities = new List<GameObject>();

	private Graph<Keystone> _keystoneGraph;

	public enum Direction
	{
		Left,
		Right,
		Up,
		Down
	}

	public void AddEntity(GameObject entity)
	{
		if (entity == null)
			throw new NullReferenceException("Can't add entity. Entity is null");

		_entities.Add(entity);
	}

	public void AddEntities(GameObject[] entities)
	{
		if (entities.Length <= 0)
			throw new NullReferenceException("Can't add entities. Entities array is empty");

		_entities.AddRange(entities);
	}

	public GameObject GetEntity(string tag)
	{
		RemoveEntitiesIfNull();

		foreach (GameObject entity in _entities)
		{
			if (entity.tag == tag)
				return entity;
		}

		return null;
	}

	public GameObject GetEntity(KeyCode key)
	{
		RemoveEntitiesIfNull();

		foreach (GameObject entity in _entities)
		{
			if (entity.GetComponent<IKeystoneEntity>().Key == key)
				return entity;
		}

		return null;
	}

	public GameObject GetEntity(KeyCode key, string tag)
	{
		RemoveEntitiesIfNull();

		foreach (GameObject entity in _entities)
		{
			if (entity.GetComponent<IKeystoneEntity>().Key == key && entity.tag == tag)
				return entity;
		}

		return null;
	}

	public GameObject[] GetEntities(KeyCode key)
	{
		RemoveEntitiesIfNull();

		List<GameObject> entities = new List<GameObject>();

		foreach (GameObject entity in _entities)
		{
			if (entity.GetComponent<IKeystoneEntity>().Key == key)
				entities.Add(entity);
		}

		return entities.ToArray();
	}

	public Keystone GetNeighbourKeystone(KeyCode key, Direction direction)
	{
		Node<Keystone> node = GetNode(key);

		if (node == null)
			return null;

		if (node.NextNodes[(int)direction] == null)
			return null;

		Keystone keystone = node.NextNodes[(int)direction].Data;

		return keystone;
	}

	public Keystone GetKeystone(KeyCode key)
	{
		foreach (Node<Keystone> node in _keystoneGraph.Nodes)
		{
			Keystone keystone = node.Data;

			if (keystone.Key == key)
				return keystone;
		}

		return null;
	}

	public Node<Keystone> GetNode(KeyCode key)
	{
		foreach (Node<Keystone> node in _keystoneGraph.Nodes)
		{
			Keystone keystone = node.Data;

			if (keystone.Key == key)
				return node;
		}

		return null;
	}

	private void Awake()
	{
		_keystoneGraph = new Graph<Keystone>();
		_keystoneGraph = GetKeyboardLayout();
	}

	private void Update()
	{
		UpdateInput();
	}

	private void OnApplicationFocus(bool focus)
	{
		if (focus)
			return;

		// If the application loses focus we want to release every pressed keystone
		foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
		{
			KeystoneReleasedEvent keystoneReleasedEvent = new KeystoneReleasedEvent(key);

			EventManager.Trigger(keystoneReleasedEvent);
		}
	}

	private void UpdateInput()
	{
		foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
		{
			if (Input.GetKeyUp(key))
			{
				KeystoneReleasedEvent keystoneReleasedEvent = new KeystoneReleasedEvent(key);

				EventManager.Trigger(keystoneReleasedEvent);
			}
			else if (Input.GetKeyDown(key))
			{
				KeystonePressedEvent keystonePressedEvent = new KeystonePressedEvent(key);

				EventManager.Trigger(keystonePressedEvent);
			}
		}
	}

	private void RemoveEntitiesIfNull()
	{
		_entities.RemoveAll(item => item == null);
	}

	private Graph<Keystone> GetKeyboardLayout()
	{
		Dictionary<KeyCode, Node<Keystone>> nodeGraph = new Dictionary<KeyCode, Node<Keystone>>();
		Graph<Keystone> keystoneGraph = new Graph<Keystone>();
		KeyCode keyCode = KeyCode.A;
		
		for (int i = 0; i < 26; i++)
		{
			Keystone keystone = new Keystone(keyCode);

			nodeGraph.Add(keyCode, keystoneGraph.Add(keystone));

			keyCode++;
		}

		AddConnection(nodeGraph, KeyCode.A, KeyCode.None);
		AddConnection(nodeGraph, KeyCode.A, KeyCode.S);
		AddConnection(nodeGraph, KeyCode.A, KeyCode.Q);
		AddConnection(nodeGraph, KeyCode.A, KeyCode.Z);
		AddConnection(nodeGraph, KeyCode.B, KeyCode.V);
		AddConnection(nodeGraph, KeyCode.B, KeyCode.N);
		AddConnection(nodeGraph, KeyCode.B, KeyCode.G);
		AddConnection(nodeGraph, KeyCode.B, KeyCode.None);
		AddConnection(nodeGraph, KeyCode.C, KeyCode.X);
		AddConnection(nodeGraph, KeyCode.C, KeyCode.V);
		AddConnection(nodeGraph, KeyCode.C, KeyCode.D);
		AddConnection(nodeGraph, KeyCode.C, KeyCode.None);
		AddConnection(nodeGraph, KeyCode.D, KeyCode.S);
		AddConnection(nodeGraph, KeyCode.D, KeyCode.F);
		AddConnection(nodeGraph, KeyCode.D, KeyCode.E);
		AddConnection(nodeGraph, KeyCode.D, KeyCode.C);
		AddConnection(nodeGraph, KeyCode.E, KeyCode.W);
		AddConnection(nodeGraph, KeyCode.E, KeyCode.R);
		AddConnection(nodeGraph, KeyCode.E, KeyCode.None);
		AddConnection(nodeGraph, KeyCode.E, KeyCode.D);
		AddConnection(nodeGraph, KeyCode.F, KeyCode.D);
		AddConnection(nodeGraph, KeyCode.F, KeyCode.G);
		AddConnection(nodeGraph, KeyCode.F, KeyCode.R);
		AddConnection(nodeGraph, KeyCode.F, KeyCode.V);
		AddConnection(nodeGraph, KeyCode.G, KeyCode.F);
		AddConnection(nodeGraph, KeyCode.G, KeyCode.H);
		AddConnection(nodeGraph, KeyCode.G, KeyCode.T);
		AddConnection(nodeGraph, KeyCode.G, KeyCode.B);
		AddConnection(nodeGraph, KeyCode.H, KeyCode.G);
		AddConnection(nodeGraph, KeyCode.H, KeyCode.J);
		AddConnection(nodeGraph, KeyCode.H, KeyCode.Y);
		AddConnection(nodeGraph, KeyCode.H, KeyCode.N);
		AddConnection(nodeGraph, KeyCode.I, KeyCode.U);
		AddConnection(nodeGraph, KeyCode.I, KeyCode.O);
		AddConnection(nodeGraph, KeyCode.I, KeyCode.None);
		AddConnection(nodeGraph, KeyCode.I, KeyCode.K);
		AddConnection(nodeGraph, KeyCode.J, KeyCode.H);
		AddConnection(nodeGraph, KeyCode.J, KeyCode.K);
		AddConnection(nodeGraph, KeyCode.J, KeyCode.U);
		AddConnection(nodeGraph, KeyCode.J, KeyCode.M);
		AddConnection(nodeGraph, KeyCode.K, KeyCode.J);
		AddConnection(nodeGraph, KeyCode.K, KeyCode.L);
		AddConnection(nodeGraph, KeyCode.K, KeyCode.I);
		AddConnection(nodeGraph, KeyCode.K, KeyCode.None);
		AddConnection(nodeGraph, KeyCode.L, KeyCode.K);
		AddConnection(nodeGraph, KeyCode.L, KeyCode.None);
		AddConnection(nodeGraph, KeyCode.L, KeyCode.O);
		AddConnection(nodeGraph, KeyCode.L, KeyCode.None);
		AddConnection(nodeGraph, KeyCode.M, KeyCode.N);
		AddConnection(nodeGraph, KeyCode.M, KeyCode.None);
		AddConnection(nodeGraph, KeyCode.M, KeyCode.J);
		AddConnection(nodeGraph, KeyCode.M, KeyCode.None);
		AddConnection(nodeGraph, KeyCode.N, KeyCode.B);
		AddConnection(nodeGraph, KeyCode.N, KeyCode.M);
		AddConnection(nodeGraph, KeyCode.N, KeyCode.H);
		AddConnection(nodeGraph, KeyCode.N, KeyCode.None);
		AddConnection(nodeGraph, KeyCode.O, KeyCode.I);
		AddConnection(nodeGraph, KeyCode.O, KeyCode.P);
		AddConnection(nodeGraph, KeyCode.O, KeyCode.None);
		AddConnection(nodeGraph, KeyCode.O, KeyCode.L);
		AddConnection(nodeGraph, KeyCode.P, KeyCode.O);
		AddConnection(nodeGraph, KeyCode.P, KeyCode.None);
		AddConnection(nodeGraph, KeyCode.P, KeyCode.None);
		AddConnection(nodeGraph, KeyCode.P, KeyCode.None);
		AddConnection(nodeGraph, KeyCode.Q, KeyCode.None);
		AddConnection(nodeGraph, KeyCode.Q, KeyCode.W);
		AddConnection(nodeGraph, KeyCode.Q, KeyCode.None);
		AddConnection(nodeGraph, KeyCode.Q, KeyCode.A);
		AddConnection(nodeGraph, KeyCode.R, KeyCode.E);
		AddConnection(nodeGraph, KeyCode.R, KeyCode.T);
		AddConnection(nodeGraph, KeyCode.R, KeyCode.None);
		AddConnection(nodeGraph, KeyCode.R, KeyCode.F);
		AddConnection(nodeGraph, KeyCode.S, KeyCode.A);
		AddConnection(nodeGraph, KeyCode.S, KeyCode.D);
		AddConnection(nodeGraph, KeyCode.S, KeyCode.W);
		AddConnection(nodeGraph, KeyCode.S, KeyCode.X);
		AddConnection(nodeGraph, KeyCode.T, KeyCode.R);
		AddConnection(nodeGraph, KeyCode.T, KeyCode.Y);
		AddConnection(nodeGraph, KeyCode.T, KeyCode.None);
		AddConnection(nodeGraph, KeyCode.T, KeyCode.G);
		AddConnection(nodeGraph, KeyCode.U, KeyCode.Y);
		AddConnection(nodeGraph, KeyCode.U, KeyCode.I);
		AddConnection(nodeGraph, KeyCode.U, KeyCode.None);
		AddConnection(nodeGraph, KeyCode.U, KeyCode.J);
		AddConnection(nodeGraph, KeyCode.V, KeyCode.C);
		AddConnection(nodeGraph, KeyCode.V, KeyCode.B);
		AddConnection(nodeGraph, KeyCode.V, KeyCode.F);
		AddConnection(nodeGraph, KeyCode.V, KeyCode.None);
		AddConnection(nodeGraph, KeyCode.W, KeyCode.Q);
		AddConnection(nodeGraph, KeyCode.W, KeyCode.E);
		AddConnection(nodeGraph, KeyCode.W, KeyCode.None);
		AddConnection(nodeGraph, KeyCode.W, KeyCode.S);
		AddConnection(nodeGraph, KeyCode.X, KeyCode.Z);
		AddConnection(nodeGraph, KeyCode.X, KeyCode.C);
		AddConnection(nodeGraph, KeyCode.X, KeyCode.S);
		AddConnection(nodeGraph, KeyCode.X, KeyCode.None);
		AddConnection(nodeGraph, KeyCode.Y, KeyCode.T);
		AddConnection(nodeGraph, KeyCode.Y, KeyCode.U);
		AddConnection(nodeGraph, KeyCode.Y, KeyCode.None);
		AddConnection(nodeGraph, KeyCode.Y, KeyCode.H);
		AddConnection(nodeGraph, KeyCode.Z, KeyCode.None);
		AddConnection(nodeGraph, KeyCode.Z, KeyCode.X);
		AddConnection(nodeGraph, KeyCode.Z, KeyCode.A);
		AddConnection(nodeGraph, KeyCode.Z, KeyCode.None);

		return keystoneGraph;
	}

	private void AddConnection(Dictionary<KeyCode, Node<Keystone>> nodeGraph, KeyCode key, KeyCode nextKey)
	{
		Node<Keystone> node = (nextKey != KeyCode.None) ? nodeGraph[nextKey] : null;

		nodeGraph[key].AddNextNode(node);
	}
}