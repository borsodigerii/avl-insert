using System;
namespace AVL_Insert
{
	public class AVLDuplicateException : Exception
	{
		public AVLDuplicateException()
		{
		}
		public AVLDuplicateException(String msg) : base(msg) { }
	}
}

