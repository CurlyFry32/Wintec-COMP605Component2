namespace COMP605Component2
{

    internal class Node
    {
        // Data item to store
        public String Word { get; set; }
        public Node? Next { get; set; }
        public Node? Prev { get; set; }

        // Null Constructor
        public Node()
        {
            Word = "";
            Next = null;
            Prev = null;
        }

        // Second Constructor
        public Node(String word)
        {
            Word = word;
            Next = null;
            Prev = null;
        }

        public string ToPrint()
        {
            return this.Word.ToString();
        }

    }
}
