using static System.Console;
using System.Diagnostics;
using System.Text;

namespace COMP605Component2
{
    internal class dblDictionary
    {
        public Node? Head { get; set; }
        public Node? Tail { get; set; }
        public Node? Current { get; set; }
        public int Counter { get; set; }

        public void LoadFile()
        {

            string directoryPath = @".\WordFiles";
            var files = new DirectoryInfo(directoryPath).GetFiles();
            var sortedFiles = files.OrderBy(file => file.Length).ToList(); //load according to file size, relative reference
            
            WriteLine($"From folder \"{directoryPath}\"");
            int opt = 0; // initialise menu option
            bool isFinished = false; // boolean flag
            while (!isFinished)
            {
                int fCount = 0;
                Clear();
                foreach (var file in sortedFiles)//searches files
                {
                    fCount++;
                    WriteLine($"{fCount}: {file.Name}");
                }
                WriteLine();
                WriteLine($"{fCount+1}: Exit");

                WriteLine("");
                WriteLine("Enter option: ");

                if (!int.TryParse(ReadLine(), out opt))
                {
                    WriteLine("Invalid input. Please enter a number.");
                    continue; // Skip to the next iteration of the loop
                }
                if (opt >= 1 && opt <= fCount)
                {
                    if (Head != null) 
                    {   //List exists, remove list to insert new file
                        Head = null;
                        Tail = null;
                        //head and tail are nulled thus the links between are now dead
                    }
                    int count = 0;
                    FileInfo f = files[opt-1];
                    // Read all lines from the file into an array
                    string[] lines = File.ReadAllLines(f.FullName);
                    WriteLine($"Inserted file: {f}");
                    // Display the lines
                    Stopwatch sw = Stopwatch.StartNew();
                    sw.Start();
                    foreach (string line in lines)
                    {
                        if (ToSearch(line) < 0) //checks for duplicates
                        {
                            if (!line.Contains("#"))//checks for lines that do not have # 
                            { //if no duplicates and no "#" insert into end of list
                                AddToRear(line);
                                count++;
                            }
                        }
                    }
                    sw.Stop();
                    long time = sw.ElapsedMilliseconds;
                    WriteLine($"{count} lines inserted");
                    WriteLine($"Time Taken: {time}ms");
                    ReadKey();
                }
                if (opt == fCount + 1)
                {   //How to invoke the exit option using a boolean flag
                    isFinished = true;
                }
                else
                {
                    WriteLine("Not a Valid option");
                }
            }
                    
        }
        public int ToSearch(String target)
        {
            Node curr = Head;
            int pos = 0;
            if (Head == null) //check for empty list
            {
                return -1; 
            }
            while (curr != null && curr.Word != target) //recursively search for items in O(n) time
            {
                pos++;
                curr = curr.Next;
            }

            // If the word is not present in the list
            if (curr == null || curr.Word != target) //check for end of list
            {
                return -1; 
            }
            return pos; //return where target is in list (starts at 0)

        } 

        #region Insert Methods
        private void InsertAtFront(Node node)
        {
            if (Head == null)
            { //check if list is null
                Head = node;
                Tail = node;
                Current = node;
            }
            else
            { //Attach list to new node
                node.Next = Head;
                Head.Prev = node;

                //Reassign Head to node and keep at top of stack
                Head = node;
                Current = node;
            }       
            Counter++;
        }

        public void AddToFront(String data)
        {//UI Method call
            Node temp = new Node(data);
            InsertAtFront(temp);
            WriteLine($"Attempted adding{temp.ToString()}");
        }

        private void InsertAtRear(Node node)
        { //Similar to InsertAtFront but using Tail node
            if (Head == null)
            { //check if list is null
                Head = node;
                Tail = node;
                Current = node;
            }
            else
            { //Attach list to new node
                Tail.Next = node;
                node.Prev = Tail;

                //Reassign Head to node and keep at top of stack
                Tail = node;
                Current = node;
            }
            Counter++;
        }
        public void AddToRear(String data)
        {//UI Method call
            Node temp = new Node(data);
            InsertAtRear(temp);
        }

        private bool InsertBefore(Node node, Node targetNode)
        {
            bool inserted = false;
            if (Head == null)
            { //list empty
                return inserted;
            }

            if (targetNode.Word == Head.Word)
            { //if head insert as new head
                InsertAtFront(node);
                inserted = true;
            }
            else
            {
                //list not empty
                //set current to find target node
                Current = Head;

                while (Current != null && !inserted)
                {   //search for mode
                    if (Current.Word == targetNode.Word)
                    {   //target node found
                        node.Next = Current;
                        node.Prev = Current.Prev;
                        Current.Prev.Next = node;
                        Current.Prev = node;
                        inserted = true;
                        Counter++;
                    }
                    else
                    {
                        Current = Current.Next;
                    }
                }
            }
            return inserted;
        }
        public string AddBefore(String data, String target)
        {
            Node newNode = new Node(data);
            Node TargetNode = new Node(target);
            if (InsertBefore(newNode, TargetNode))
            {
                return "Target " + TargetNode.ToPrint() + " found, Node: " + newNode.ToPrint() + " inserted";
            }
            else
            {
                return "Target " + TargetNode.ToPrint() + " NOT found, Node: " + newNode.ToPrint() + " NOT inserted";
            }
        }

        private bool InsertAfter(Node node, Node targetNode)
        {

            bool inserted = false;
            if (Head == null)
            { //list empty
                return inserted;
            }

            //list not empty
            //set current to find target node
            Current = Head;

            while (Current != null && !inserted)
            {   //search for mode
                if (Current.Word == targetNode.Word)
                {   //target node found
                    if (Current == Tail)
                    {//Reassign Tail
                        InsertAtRear(node);
                    }
                    else
                    {
                        node.Next = Current.Next;
                        node.Prev = Current;
                        node.Next.Prev = node;
                        Current.Next = node;
                        Current = node;
                    }
                    inserted = true;
                    Counter++;
                }
                else
                {
                    Current = Current.Next; //assign to next node in list
                }
            }
            return inserted;
        }

        public string AddAfter(String data, String target)
        {
            Node newNode = new Node(data);
            Node TargetNode = new Node(target);
            if (InsertAfter(newNode, TargetNode))
            {
                return "Target " + TargetNode.ToPrint() + " found, Node: " + newNode.ToPrint() + " inserted";
            }
            else
            {
                return "Target " + TargetNode.ToPrint() + " NOT found, Node: " + newNode.ToPrint() + " NOT inserted";
            }
        }
        #endregion

        #region Delete Methods
        private Node DeleteAtFront()
        {
            if (Head == null) //Empty list
            {
                return null;
            }
            else
            {
                Node nodeToRemove = Head; //Assign node to delete as head

                Head = Head.Next;
                Head.Prev = null;
                Current = Head;
                Counter--;

                return nodeToRemove;
            }
        }
        public string RemoveFront()
        {//UI method call
            Node nodeToRemove = new Node();
            nodeToRemove = DeleteAtFront();
            if (nodeToRemove != null)
            {
                return "Found, NODE: " + nodeToRemove.ToString() + " removed";
            }
            else
            {
                return "NOT found, or List Empty";
            }
        }

        private Node DeleteAtEnd()
        {
            if (Head == null) //check empty list
            {
                return null;
            }
            else
            {
                Node nodeToRemove = new Node();
                nodeToRemove = Tail;    //assign node to remove as Tail

                Tail = Tail.Prev;
                Tail.Next = null;
                Current = Tail;
                Counter--;

                return nodeToRemove;
            }
        }
        public string RemoveRear()
        {
            Node? nodeToRemove = null;
            nodeToRemove = DeleteAtEnd();
            if (nodeToRemove != null)
            {
                return "NODE " + nodeToRemove.ToString() + " removed";
            }
            else
            {
                return "Deletion failed - List Empty";
            }
        }
        private Node DeleteNode(Node nodeToDelete)
        {
            Node? nodeToRemove = null;
            if (Head == null) //Check empty list and return null
            {
                nodeToRemove = null;
            }
            else if (Head.Word == nodeToDelete.Word) //Delete Head
            {
                nodeToRemove = Head;
                DeleteAtFront();
            }
            else if (Tail.Word == nodeToDelete.Word) //Delete Tail
            {
                nodeToRemove = Tail;
                DeleteAtEnd();
            }
            else    //delete specific node by going through and searching for node
            {
                Current = Head;
                bool deleted = false;
                while (Current != null && !deleted)
                {
                    if (Current.Word == nodeToDelete.Word)
                    {
                        deleted = true;
                        Counter--;
                    }
                    Current = Current.Next;
                }
            }
            return nodeToRemove;
        }
        public string RemoveNode(String data)
        {//UI method call
            Node nodeToRemove = new Node(data);
            Node targetNode = nodeToRemove;
            nodeToRemove = DeleteNode(nodeToRemove);
            if (nodeToRemove != null)
            {
                return "Target " + targetNode.ToString() + " found, NODE: " + nodeToRemove.ToString() + " removed";
            }
            else
            {
                return "Target " + targetNode.ToString() + " NOT found, or List Empty";
            }
        }
        #endregion
        #region Print Methods
        public string ToPrint()
        {
            StringBuilder sb = new StringBuilder();

            if (Head == null)
            {
                sb.Append("Stack is Empty");
                return sb.ToString();
            }
            else
            { 
                Node? temp = null;
                int pos = 1;
                while (temp != null)
                {
                    sb.Append("Node " + pos.ToString() + " --> " + temp.ToPrint() + "\n");
                    temp = temp.Next;
                    pos++;
                }
            }
            return sb.ToString();
        }
        #endregion
    }
}