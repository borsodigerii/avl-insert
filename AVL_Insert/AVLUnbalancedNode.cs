using System;
namespace AVL_Insert
{
	public struct AVLUnbalancedNode
	{
        public Node? parent;
        public Node node;
        public AVLUnbalancedNodeMode nodeMode;
        public Node child;
        public AVLUnbalancedNodeMode childMode;

        public AVLUnbalancedNode(Node? parent, Node node, AVLUnbalancedNodeMode nodeMode, Node child, AVLUnbalancedNodeMode childMode)
        {
            this.parent = parent;
            this.node = node;
            this.nodeMode = nodeMode;
            this.child = child;
            this.childMode = childMode;
        }
        public override string ToString()
        {
            return "{parent: (" + parent + "); node: { (" + node + "); nodeMode: [" + nodeMode + "] }; child: { (" + child + "); nodeMode: [" + childMode + "] }";
        }
    }
    
}

