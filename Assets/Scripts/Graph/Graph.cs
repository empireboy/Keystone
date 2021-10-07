using System.Collections.Generic;

public class Graph<T>
{
	public List<Node<T>> Nodes { get; private set; }

	public Graph()
	{
		Nodes = new List<Node<T>>();
	}

	public Node<T> Add(T data)
	{
		if (data == null)
			return null;

		if (Nodes == null)
			Nodes = new List<Node<T>>();

		Node<T> node = new Node<T>(data);

		Nodes.Add(node);

		return node;
	}
}