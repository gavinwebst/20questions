using System.Collections.Generic;



namespace _20questions
{

    public class TreeNode {
        public string question;
        public TreeNode yesNode;
        public TreeNode noNode;

        public TreeNode(string question) {
            this.question = question;
            this.noNode = null;
            this.yesNode = null;
        }

        public void setNoNode(TreeNode node) { 
            this.noNode = node;
        }

        public void setYesNode(TreeNode node) {
            this.yesNode = node; 
        }

        public TreeNode getYesNode() { 
            return this.yesNode;
        }

        public TreeNode getNoNode() {
            return this.noNode;
        }


    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
