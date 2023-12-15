using System;
namespace AVL_Insert
{
	public class AVLTree
	{
		private Node? rootNode;

		public AVLTree()
		{
			this.rootNode = null;
		}
        public AVLTree(Node root)
        {
            this.rootNode = root;
        }

        public Node? getRootNode()
		{
			return this.rootNode;
		}
		public void insertNode(Node node)
		{
			//Console.WriteLine("inserting node (" + node.getValue() + ")");
			if (rootNode == null) return;

			Node? actualNode = rootNode;
			Node assignToNode = actualNode;

			while(actualNode != null)
			{
				if (node.getValue() == actualNode.getValue()) throw new AVLDuplicateException("There can be no duplications inside an AVL Tree!");

				if(node.getValue() < actualNode.getValue())
				{
					// left part-tree
					assignToNode = actualNode;
					actualNode = actualNode.getPrev();
					continue;
				}
				else
				{
					// right part-tree
					assignToNode = actualNode;
                    actualNode = actualNode.getNext();
                    continue;
                }
			}

			if(node.getValue() < assignToNode.getValue())
			{
				// we have to insert the node to the left side of assignNode
				//Console.WriteLine("inserting as a left child of (" + assignToNode.getValue() + ")");
				assignToNode.setPrev(node);
			}
			else
			{
                // we have to insert the node to the right side of
                //Console.WriteLine("inserting as a right child of (" + assignToNode.getValue() + ")");
                assignToNode.setNext(node);
            }
			while (!isTreeBalanced())
			{
				// we have to rotate until the tree is balanced again
				//Console.WriteLine("-- Tree is not balanced, rotating..");
				rotate(getUnbalancedNode());
			}

            //Console.WriteLine("Tree is balanced again.");
            Console.WriteLine(this);
        }

		public AVLUnbalancedNode getUnbalancedNode()
		{
			//Console.WriteLine("Getting unbalanced node for rotating");
            Node? imbalanceAt = null;
			Node? parent = null;
			bool first = true;

            AVLQueue queue = new AVLQueue();
            queue.add(rootNode);

            // while we have elements inside our queue, we keep going
            while (!queue.isEmpty())
            {
				
                Node processing = queue.pop();
				//Console.WriteLine("- Checking if (" + processing.getValue() + ") is inbalanced..");

                // if a leaf, we skip it
                if (processing.getNext() == null && processing.getPrev() == null) continue;

                if (!isNodeBalanced(processing))
                {
					//Console.WriteLine("***Imbalanced, flagging");
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        parent = imbalanceAt;
                    }
                    imbalanceAt = processing;
					
                }
                queue.add(processing.getPrev());
                queue.add(processing.getNext());
            }
			
			if(imbalanceAt != null)
			{
                //Console.WriteLine("******\nLast found inbalance: (" + imbalanceAt.getValue() + "), balance: " + getBalanceForNode(imbalanceAt));
                if (getBalanceForNode(imbalanceAt) == -2)
                {
                    return new AVLUnbalancedNode(getParentOfNode(getRootNode(), imbalanceAt.getValue()), imbalanceAt, AVLUnbalancedNodeMode.NN, imbalanceAt.getNext(), (getBalanceForNode(imbalanceAt.getNext()) < 0 ? AVLUnbalancedNodeMode.N : AVLUnbalancedNodeMode.P));
                }
                else
                {
                    return new AVLUnbalancedNode(getParentOfNode(getRootNode(), imbalanceAt.getValue()), imbalanceAt, AVLUnbalancedNodeMode.PP, imbalanceAt.getPrev(), (getBalanceForNode(imbalanceAt.getPrev()) < 0 ? AVLUnbalancedNodeMode.N : AVLUnbalancedNodeMode.P));
                }
			}
			else
			{
                //Console.WriteLine("******\nNo inbalance found");
                return new AVLUnbalancedNode();
			}
			
        }

		public bool isTreeBalanced()
		{
			if (rootNode == null) return false;

			AVLQueue queue = new AVLQueue();
			queue.add(rootNode);

            // while we have elements inside our queue, we keep going
            while (!queue.isEmpty())
			{
				
				Node processing = queue.pop();

				// if a leaf, we skip it
				if (processing.getNext() == null && processing.getPrev() == null) continue;

				if (!isNodeBalanced(processing))
				{
					return false;
				}
				queue.add(processing.getPrev());
                queue.add(processing.getNext());
            }
			return true;
			
		}

		public bool isNodeBalanced(Node node)
		{
			int balanceLevel = getBalanceForNode(node);

			//Console.WriteLine("(" + node.getValue() + ") balance level is " + balanceLevel);	
			return Math.Abs(balanceLevel) <= 1;
		}

		public int getBalanceForNode(Node node)
		{
			return getLevelsForNodeDescendents(node.getPrev(), 0) - getLevelsForNodeDescendents(node.getNext(), 0);

        }

		public int getLevelsForNodeDescendents(Node? node, int accumulator)
		{
			
			if (node == null) return 0;
            //Console.WriteLine("Checking levels for " + node.getValue() + "...");
            accumulator++;
            //Console.WriteLine("Current level: " + accumulator);
            if (node.getPrev() == null && node.getNext() == null)
			{
                //Console.WriteLine("Its a leaf, terminating...");
                return accumulator;
			}

			int valueLeftTree = getLevelsForNodeDescendents(node.getPrev(), accumulator);
			int valueRightTree = getLevelsForNodeDescendents(node.getNext(), accumulator);


            return valueLeftTree < valueRightTree ? valueRightTree : valueLeftTree;
		}

		public void rotate(AVLUnbalancedNode node)
		{
			//Console.WriteLine("Rotating (" + node.node + ") since it is inbalanced..");
			if (rootNode == null) return;
			//Console.WriteLine("past through rootcheck");
			//Console.WriteLine("Received packet: " + node);
			bool isRoot = (node.parent == null && node.node != null);

			if(node.nodeMode == AVLUnbalancedNodeMode.NN)
			{
				// (--,*)
				if(node.childMode == AVLUnbalancedNodeMode.N)
				{
                    // (--,-)
                    // node helyere jon a child, majd child bal gyereke lesz a root
                    if (isRoot)
					{
						Node root = this.rootNode;
						this.rootNode = node.child;
						node.child.setPrev(root);
                        node.node.setNext(null);
                    }
					else
					{
						// parent jobb gyereke a node helyett child lesz, majd child bal gyereke a node
						//Console.WriteLine("Rotated as (--,-), so parent is (" + node.parent + "), parent righthand child (" + node.parent.getNext() + "), child left hand (" + node.child.getPrev() + "), child righthand (" + node.child.getNext() + ")");
						if(node.parent.getNext() != null && node.parent.getNext() == node.node)
						{
                            node.parent.setNext(node.child);
						}
						else if(node.parent.getPrev() != null && node.parent.getPrev() == node.node)
						{
                            node.parent.setPrev(node.child);
                        }
						
						node.child.setPrev(node.node);
                        node.node.setNext(null);
                    }

                }
                else if(node.childMode == AVLUnbalancedNodeMode.P)
				{
                    // (--,+)
                    //Console.WriteLine("Rotating as (--,+)");
                    if (isRoot)
                    {
                        Node max = node.child.getPrev();
                        this.rootNode = max;
                        node.node.setNext(max.getPrev());
                        node.child.setPrev(max.getNext());
                        max.setPrev(node.node);
                        max.setNext(node.child);
                    }
                    else
                    {
                        Node max = node.child.getPrev();
                        if (node.parent.getNext() != null && node.parent.getNext() == node.node)
                        {
                            node.parent.setNext(max);
                        }
                        else if (node.parent.getPrev() != null && node.parent.getPrev() == node.node)
                        {
                            node.parent.setPrev(max);
                        }
                        node.node.setNext(max.getPrev());
                        node.child.setPrev(max.getNext());
                        max.setPrev(node.node);
                        max.setNext(node.child);
                    }
                }

			}else if(node.nodeMode == AVLUnbalancedNodeMode.PP)
			{
                // (++,*)
                if (node.childMode == AVLUnbalancedNodeMode.N)
                {
					// (++,-)
					//Console.WriteLine("Rotating as (++,-)");

                    if (isRoot)
                    {
                        Node min = node.child.getNext();
                        this.rootNode = min;
                        node.node.setPrev(min.getNext());
                        node.child.setNext(min.getPrev());
                        min.setNext(node.node);
                        min.setPrev(node.child);
                    }
                    else
                    {
                        Node min = node.child.getNext();
                        if (node.parent.getNext() != null && node.parent.getNext() == node.node)
                        {
                            node.parent.setNext(min);
                        }
                        else if (node.parent.getPrev() != null && node.parent.getPrev() == node.node)
                        {
                            node.parent.setPrev(min);
                        }
                        node.node.setPrev(min.getNext());
                        node.child.setNext(min.getPrev());
                        min.setNext(node.node);
                        min.setPrev(node.child);
                    }
                }
                else if (node.childMode == AVLUnbalancedNodeMode.P)
                {
                    // (++,+)

                    // node helyere jon a child, majd child jobb gyereke lesz a root
                    if (isRoot)
                    {
                        Node root = this.rootNode;
                        this.rootNode = node.child;
                        node.child.setNext(root);
                        node.node.setPrev(null);
                    }
                    else
                    {
                        // parent bal gyereke a node helyett child lesz, majd child jobb gyereke a node
                        //Console.WriteLine("Rotated as (++,+), so parent is (" + node.parent + "), parent lefthand child (" + node.parent.getPrev() + "), child right hand (" + node.child.getNext() + "), child lefthand (" + node.child.getPrev() + ")");
                        if (node.parent.getNext() != null && node.parent.getNext() == node.node)
                        {
                            node.parent.setNext(node.child);
                        }
                        else if (node.parent.getPrev() != null && node.parent.getPrev() == node.node)
                        {
                            node.parent.setPrev(node.child);
                        }
                        node.child.setNext(node.node);
                        node.node.setPrev(null);
                    }
                }
            }
		}
		private String toStr = "";
        public override string ToString()
        {
			toStr = "";
			treeToString(this.getRootNode());
			return "Tree representation: " + toStr;
        }
        private void treeToString(Node root)
        {
            // bases case 
            if (root == null)
                return;

            // push the root data as character 
            toStr += (char)(root.getValue() + '0');

            // if leaf node, then return 
            if (root.getPrev() == null && root.getNext() == null)
                return;

            // for left subtree 
            toStr += ('(');
            treeToString(root.getPrev());
            toStr += (')');

            // only if right child is present to 
            // avoid extra parenthesis 
            if (root.getNext() != null)
            {
                toStr += ('(');
                treeToString(root.getNext());
                toStr += (')');
            }
        }

		public Node? getParentOfNode(Node startNode, int valueToSearch)
		{
			if (startNode.getValue() == valueToSearch && startNode == this.getRootNode()) return null;
			if (startNode.getPrev() == null && startNode.getNext() == null) return null;
			//Console.WriteLine("---------- Checking if (" + startNode + ") is parent of (" + valueToSearch + ")..");
			
			if((startNode.getNext() != null && startNode.getNext().getValue() == valueToSearch) || (startNode.getPrev() != null && startNode.getPrev().getValue() == valueToSearch))
			{
				//Console.WriteLine("----------- Yes, returning (" + startNode + ")");
				return startNode;
			}
			else
			{
                //Console.WriteLine("----------- Not, trying with children (" + startNode.getPrev() + "), and (" + startNode.getNext() + ")");
                Node leftTry = getParentOfNode(startNode.getPrev(), valueToSearch);
                Node rightTry = getParentOfNode(startNode.getNext(), valueToSearch);

				if (leftTry != null) return leftTry;
				if (rightTry != null) return rightTry;
				//Console.WriteLine("------------ Parent not found, returning null");
				return null;
            }
		}

		public Node getMinFromTree(Node startNode)
		{
			if (startNode.getPrev() == null) return startNode;

			return getMinFromTree(startNode.getPrev());
		}
        public Node getMaxFromTree(Node startNode)
        {
            if (startNode.getNext() == null) return startNode;

            return getMinFromTree(startNode.getNext());
        }
    }
}

