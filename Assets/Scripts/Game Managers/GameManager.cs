using CM.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public event GameObjectEvent OnEntityRemoved;

	[SerializeField]
	private List<GameObject> _entities = new List<GameObject>();

	[SerializeField]
	private GameObject[] _itemPrefabs;

	[SerializeField]
	private GameObject[] _enemyPrefabs;

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

	public void RemoveEntity(GameObject entity)
	{
		_entities.Remove(entity);

		OnEntityRemoved?.Invoke(entity);

		Destroy(entity);
	}

	public void RemoveEntity(GameObject entity, float time)
	{
		_entities.Remove(entity);

		OnEntityRemoved?.Invoke(entity);

		Destroy(entity, time);
	}

	public GameObject GetEntity(string tag)
	{
		foreach (GameObject entity in _entities)
		{
			if (entity.tag == tag)
				return entity;
		}

		return null;
	}

	public GameObject GetEntity(KeyCode key)
	{
		foreach (GameObject entity in _entities)
		{
			if (entity.GetComponent<IKeystoneEntity>().Key == key)
				return entity;
		}

		return null;
	}

	public GameObject GetEntity(KeyCode key, string tag)
	{
		foreach (GameObject entity in _entities)
		{
			if (entity.GetComponent<IKeystoneEntity>().Key == key && entity.tag == tag)
				return entity;
		}

		return null;
	}

	public GameObject[] GetEntities(string tag)
	{
		List<GameObject> entities = new List<GameObject>();

		foreach (GameObject entity in _entities)
		{
			if (entity.tag == tag)
				entities.Add(entity);
		}

		return entities.ToArray();
	}

	public GameObject[] GetEntities(KeyCode key)
	{
		List<GameObject> entities = new List<GameObject>();

		foreach (GameObject entity in _entities)
		{
			if (entity.GetComponent<IKeystoneEntity>().Key == key)
				entities.Add(entity);
		}

		return entities.ToArray();
	}

	public GameObject GetItemPrefab(ItemTypes itemType)
    {
		return _itemPrefabs[(int)itemType];
	}

	public GameObject GetEnemyPrefab(EnemyTypes enemyType)
	{
		return _enemyPrefabs[(int)enemyType];
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

	public Keystone[] GetAllNeighbourKeystones(KeyCode key)
	{
		List<Keystone> keystones = new List<Keystone>();
		Node<Keystone> node = GetNode(key);

		if (node == null)
			return null;

		int neighbourCount = 4;

		for (int i = 0; i < neighbourCount; i++)
		{
			if (node.NextNodes[i] == null)
			{
				keystones.Add(null);
				continue;
			}

			keystones.Add(node.NextNodes[i].Data);
		}

		return keystones.ToArray();
	}

	public Keystone[] GetKeystonesByPosition(KeystonePositionsSO keystonePositionsSO, KeyCode key)
	{
		List<Keystone> keystones = new List<Keystone>();
		Node<Keystone> node = GetNode(key);

		if (node == null)
			return null;

		for (int i = 0; i < keystonePositionsSO.positions.Length; i++)
		{
			Node<Keystone> targetNode = node;
			Node<Keystone> nodeToCheck = null;

			int positionX = (int)keystonePositionsSO.positions[i].x;
			int positionY = (int)keystonePositionsSO.positions[i].y;

			// Make sure to add the starting node if X and Y are zero
			if (positionX == 0 && positionY == 0)
			{
				keystones.Add(targetNode.Data);
				continue;
			}

			// Check for left or right node
			Direction horizontalDirection = (positionX < 0) ? Direction.Left : Direction.Right;

			// Check for up or down node
			Direction verticalDirection = (positionY < 0) ? Direction.Up : Direction.Down;

			positionX = Mathf.Abs(positionX);
			positionY = Mathf.Abs(positionY);

			do
			{
				if (positionX > 0)
				{
					nodeToCheck = GetNeighbourNode(targetNode, horizontalDirection);

					if (nodeToCheck != null)
					{
						targetNode = nodeToCheck;
						positionX--;
					}
				}

				if (positionY > 0 && nodeToCheck == null)
				{
					nodeToCheck = GetNeighbourNode(targetNode, verticalDirection);

					if (nodeToCheck != null)
					{
						targetNode = nodeToCheck;
						positionY--;
					}
					else
					{
						// Exit loop if the X or Y couldn't find a new node
						targetNode = null;

						positionX = 0;
						positionY = 0;
					}
				}
				else if (nodeToCheck == null)
				{
					// Exit loop if the X or Y couldn't find a new node
					targetNode = null;

					positionX = 0;
					positionY = 0;
				}

				nodeToCheck = null;
			}
			while (positionX > 0 || positionY > 0);

			/*for (int indexX = 0; indexX < Mathf.Abs(positionX); indexX++)
			{
				targetNode = GetNeighbourNode(targetNode, horizontalDirection);

				if (targetNode == null)
					break;
			}

			if (targetNode == null)
				continue;

			for (int indexY = 0; indexY < Mathf.Abs(positionY); indexY++)
			{
				targetNode = GetNeighbourNode(targetNode, verticalDirection);

				if (targetNode == null)
					break;
			}

			if (targetNode == null)
				continue;*/

			// Make sure that the target node is not the starting node
			if (targetNode != node && targetNode != null)
				keystones.Add(targetNode.Data);
		}

		return keystones.ToArray();
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

	public Node<Keystone> GetNeighbourNode(KeyCode key, Direction direction)
	{
		return GetNode(key).NextNodes[(int)direction];
	}

	public Node<Keystone> GetNeighbourNode(Node<Keystone> node, Direction direction)
	{
		return node.NextNodes[(int)direction];
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

	private Graph<Keystone> GetKeyboardLayout()
	{
		Dictionary<KeyCode, Node<Keystone>> nodeGraph = new Dictionary<KeyCode, Node<Keystone>>();
		Graph<Keystone> keystoneGraph = new Graph<Keystone>();

		// Add every letter as Keystone
		KeyCode keyCode = KeyCode.A;
		
		for (int i = 0; i < 26; i++)
		{
			Keystone keystone = new Keystone(keyCode);

			nodeGraph.Add(keyCode, keystoneGraph.Add(keystone));

			keyCode++;
		}

		// Add every alpha number as Keystone
		keyCode = KeyCode.Alpha0;

		for (int i = 0; i < 10; i++)
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