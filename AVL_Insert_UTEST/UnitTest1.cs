namespace AVL_Insert_UTEST;

using AVL_Insert;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void testIsTreeBalanced()
    {
        Node root = new Node(1);

        Node leftChild = new Node(2);
        Node rightChild = new Node(3);
        root.setPrev(leftChild);
        root.setNext(rightChild);

        AVLTree tree = new AVLTree(root);

        Assert.AreEqual(true, tree.isTreeBalanced());
    }

    [TestMethod]
    public void testGetLevelsForNodeDescendents1()
    {
        Node root = new Node(1);

        Node leftChild = new Node(2);
        Node rightChild = new Node(3);
        root.setPrev(leftChild);
        root.setNext(rightChild);

        AVLTree tree = new AVLTree(root);

        Assert.AreEqual(2, tree.getLevelsForNodeDescendents(root, 0));
    }

    [TestMethod]
    public void testGetLevelsForNodeDescendents2()
    {
        Node root = new Node(1);

        Node leftChild = new Node(2);
        Node left2Child = new Node(3);
        leftChild.setPrev(left2Child);
        root.setNext(leftChild);

        AVLTree tree = new AVLTree(root);

        Assert.AreEqual(3, tree.getLevelsForNodeDescendents(root, 0));
    }
    [TestMethod]
    public void testIsTreeBalanced2()
    {
        Node root = new Node(1);

        Node leftChild = new Node(2);
        Node left2Child = new Node(3);
        leftChild.setPrev(left2Child);
        root.setNext(leftChild);

        AVLTree tree = new AVLTree(root);

        Assert.AreEqual(false, tree.isTreeBalanced());
    }
    [TestMethod]
    public void testIsTreeBalanced3()
    {
        Node root = new Node(1);

        Node leftChild = new Node(2);
        Node rightChild = new Node(3);
        root.setPrev(leftChild);
        root.setNext(rightChild);

        AVLTree tree = new AVLTree(root);

        Assert.AreEqual(true, tree.isTreeBalanced());
    }

    [TestMethod]
    public void testGetLevelsForNodeDescendents3()
    {
        Node root = new Node(1);

        Node leftChild = new Node(2);
        Node left2Child = new Node(3);
        Node rightChild = new Node(4);

        leftChild.setPrev(left2Child);
        root.setPrev(leftChild);
        root.setNext(rightChild);


        AVLTree tree = new AVLTree(root);

        Assert.AreEqual(3, tree.getLevelsForNodeDescendents(root, 0));
    }

    [TestMethod]
    public void testGetUnbalancedNode()
    {
        Node root = new Node(4);
        Node leftC = new Node(3);
        Node leftC2 = new Node(2);
        Node leftc3 = new Node(1);
        root.setPrev(leftC);
        leftC.setPrev(leftC2);
        leftC2.setPrev(leftc3);


        AVLTree tree = new AVLTree(root);
        
        Assert.AreEqual(leftC, tree.getUnbalancedNode().node);
        Assert.AreEqual(root, tree.getUnbalancedNode().parent);
        Assert.AreEqual(leftC2, tree.getUnbalancedNode().child);
    }

    [TestMethod]
    public void testInsertNode()
    {
        Node root = new Node(3);
        Node leftc = new Node(2);
        Node rightc = new Node(4);
        root.setPrev(leftc);
        root.setNext(rightc);

        Node leftc2 = new Node(1);
        Node leftc3 = new Node(0);
        leftc.setPrev(leftc2);
        //leftc2.setPrev(leftc3);

        AVLTree tree = new AVLTree(root);
        tree.insertNode(leftc3);

        Assert.AreEqual(leftc2, root.getPrev());
        Assert.AreEqual(rightc, root.getNext());
        Assert.AreEqual(leftc, leftc2.getNext());
        Assert.AreEqual(leftc3, leftc2.getPrev());
    }

    [TestMethod]
    public void testRotationPPN()
    {
        Node root = new Node(5);
        Node n1 = new Node(1);
        Node n2 = new Node(6);
        Node n3 = new Node(0);
        Node n4 = new Node(2);
        Node n5 = new Node(3);
        AVLTree tree = new AVLTree(root);
        tree.insertNode(n1);
        tree.insertNode(n2); 
        tree.insertNode(n3);
        tree.insertNode(n4);
        tree.insertNode(n5);
        Assert.AreEqual("Tree representation: 2(1(0))(5(3)(6))", tree.ToString());
    }

    [TestMethod]
    public void testRotationNNP()
    {
        Node root = new Node(1);
        Node n1 = new Node(0);
        Node n2 = new Node(6);
        Node n3 = new Node(7);
        Node n4 = new Node(4);
        Node n5 = new Node(3);

        AVLTree tree = new AVLTree(root);
        tree.insertNode(n1);
        tree.insertNode(n2);
        tree.insertNode(n3);
        tree.insertNode(n4);
        tree.insertNode(n5);
        
        Assert.AreEqual("Tree representation: 4(1(0)(3))(6()(7))", tree.ToString());
    }

    [TestMethod]
    public void testRotationNNN()
    {
        Node root = new Node(1);
        Node n1 = new Node(2);
        Node n2 = new Node(3);
        Node n3 = new Node(4);
        AVLTree tree = new AVLTree(root);
        tree.insertNode(n1);
        tree.insertNode(n2);
        tree.insertNode(n3);
        Assert.AreEqual("Tree representation: 2(1)(3()(4))", tree.ToString());
    }

    [TestMethod]
    public void testRotationPPP()
    {
        Node root = new Node(4);
        Node n1 = new Node(3);
        Node n2 = new Node(2);
        Node n3 = new Node(1);

        AVLTree tree = new AVLTree(root);
        tree.insertNode(n1);
        tree.insertNode(n2);
        tree.insertNode(n3);
        Assert.AreEqual("Tree representation: 3(2(1))(4)", tree.ToString());
    }

}
