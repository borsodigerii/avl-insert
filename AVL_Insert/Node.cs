using System;
namespace AVL_Insert
{
	public class Node
	{
		private Node? prev;
		private Node? next;

		private int value;

		public Node(int value)
		{
			this.prev = null;
			this.next = null;
			this.value = value;
		}

		public void setPrev(Node node)
		{
			this.prev = node;
		}

		public void setNext(Node node)
		{
			this.next = node;
		}

		public void setValue(int newValue)
		{
			this.value = newValue;
		}

		public Node? getPrev()
		{
			return this.prev;
		}
        public Node? getNext()
        {
            return this.next;
        }
        public int getValue()
        {
            return this.value;
        }
        public override string ToString()
        {
            return this.getValue().ToString();
        }
    }
}

