using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTrace.Languages.Java
{
	internal class LinkedList
	{
		public string Name { get; set; }
		public int Count { get; set; }
		private LinkedListNode head;
		public Kind NodesKind { get; set; }

		public DataType Type => throw new NotImplementedException();

		public void Add(IAtom data)
		{
			if (head == null)
			{
				head = new LinkedListNode();

				head.Value = data;
				head.Next = null;
			}
			else
			{
				LinkedListNode toAdd = new LinkedListNode();
				toAdd.Value = data;

				LinkedListNode current = head;
				while (current.Next != null)
				{
					current = current.Next;
				}

				current.Next = toAdd;
			}
		}
	}

	internal class LinkedListNode
	{
		public IAtom Value { get; set; }
		public LinkedListNode Next { get; set; }	
	}

}
