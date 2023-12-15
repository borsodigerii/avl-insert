namespace AVL_Insert;
class Program
{
    static void Main(string[] args)
    {
        Node root = new Node(5);
        Node leftc = new Node(1);
        Node rightc = new Node(6);
        Node leftc3 = new Node(0);
        Node leftc4 = new Node(2);
        Node leftc5 = new Node(3);



        AVLTree tree = new AVLTree(root);
        Console.WriteLine(tree);
        tree.insertNode(leftc);
        tree.insertNode(rightc);
        tree.insertNode(leftc3);
        tree.insertNode(leftc4);
        tree.insertNode(leftc5);
    }
}

