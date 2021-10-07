using System.Collections.Generic;

public class Node<T>
{
	public T Data { get; private set; }
	public List<Node<T>> NextNodes { get; private set; }

	public Node(T data)
	{
		Data = data;
		NextNodes = new List<Node<T>>();
	}

	public void AddNextNode(Node<T> node)
	{
		NextNodes.Add(node);
	}

	public void RemoveNextNode(Node<T> node)
	{
		NextNodes.Remove(node);
	}
}