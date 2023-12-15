using System;
namespace AVL_Insert
{
	public class AVLQueue
	{
		private Queue<Node> queue = new Queue<Node>();
		public AVLQueue()
		{
		}

		public bool isEmpty()
		{
			return queue.Count == 0;
		}

		public void add(Node node)
		{
			if (node == null) return;
			queue.Enqueue(node);
		}

		public Node pop()
		{
			return queue.Dequeue();
			
		}
		public void remove()
		{
			queue.Dequeue();
		}
	}
}

