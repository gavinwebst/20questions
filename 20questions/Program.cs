using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using System;
using System.IO;


namespace _20questions
{

    public class TreeNode
    {
        public string question;
        public TreeNode yesNode;
        public TreeNode noNode;

        //constructor for node 
        public TreeNode(string question)
        {
            this.question = question;
            this.noNode = null;
            this.yesNode = null;
        }


        //mutator for the nodes
        public void setNoNode(TreeNode node)
        {
            this.noNode = node;
        }

        public void setYesNode(TreeNode node)
        {
            this.yesNode = node;
        }

        public TreeNode getYesNode()
        {
            return this.yesNode;
        }

        public TreeNode getNoNode()
        {
            return this.noNode;
        }

        public void setQuestion(string question)
        {
            this.question = question;
        }
        public string getQuestion()
        {
            return this.question;
        }

        //check if there are children nodes for parent node
        public bool isQuestion()
        {
            if (noNode == null && yesNode == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //This function is how the user will naviagte through the tree 
        public void query()
        {
            if (this.isQuestion())
            {
                Console.WriteLine(this.question);
                Console.Write("Enter 'y' for yes and 'n' for no: ");
                char input = getYesOrNo(); // y or no
                if (input == 'y')
                {
                    yesNode.query();
                }
                else
                {
                    noNode.query();
                }
            }
            else
            {
                this.onQueryObject();
            }
        }

        //this function is used when you have reached the end of the tree 
        public void onQueryObject()
        {
            Console.WriteLine("Are you thinking of a(n) " + this.question + "?");
            Console.Write("Enter 'y' for yes and 'n' for no: ");
            char input = getYesOrNo(); //y or n
            if (input == 'y')
            {
                Console.Write("The Computer Wins\n");
            }
            else
            {
                updateTree();
            }
        }

        //if the user wins the program will ask them what they were think of and ask to 
        private void updateTree()
        {
            Console.Write("You win! What were you thinking of?");
            string userInput = Console.ReadLine();
            Console.Write("Please enter a question to distinguish a(n) "
                + this.question + " from " + userInput + ": ");
            string userQuestion = Console.ReadLine();
            Console.Write("If you were thinking of a(n) " + userInput
                + ", what would the answer to that question be?");
            char input = getYesOrNo(); //y or n
            if (input == 'y')
            {
                this.noNode = new TreeNode(this.question);
                this.yesNode = new TreeNode(userInput);
            }
            else
            {
                this.yesNode = new TreeNode(this.question);
                this.noNode = new TreeNode(userInput);
            }
            Console.Write("Thank you my knowledge has been increased");
            this.setQuestion(userQuestion);
        }


        //this get the user input for questions they have been asked 
        private char getYesOrNo()
        {
            char inputCharacter = ' ';
            while (inputCharacter != 'y' && inputCharacter != 'n')
            {
                inputCharacter = Console.ReadLine().ElementAt(0);
                inputCharacter = char.ToLower(inputCharacter);
            }
            return inputCharacter;
        }
    }

    class BTTree
    {
        TreeNode rootNode;
        public BTTree(string question, string yesGuess, string noGuess)
        {
            rootNode = new TreeNode(question);
            rootNode.setYesNode(new TreeNode(yesGuess));
            rootNode.setNoNode(new TreeNode(noGuess));
        }

        public void query()
        {
            rootNode.query();
        }

        public TreeNode getRootNode() {
            return rootNode;
        }
    }

    internal class Program
    {
        /* we tried it a mess good luck have fun ;) */
        public static BTTree tree; 

        static void Main(string[] args)
        {
            string username = "treePlayer";
            string userUnique = username + ".txt";

            // Attempt to load existing knowledge
            if (File.Exists(userUnique))
            {
                Console.WriteLine("Loading existing knowledge...");
                tree = LoadTree(userUnique);
            }
            else
            {
                Console.WriteLine("No previous knowledge found!");
                startNewGame();
            }

            Console.WriteLine("\nStarting the Game");
            do
            {
                tree.query();
            } while (playAgain());

            // Save the updated tree
            SaveTree(tree, userUnique);
            Console.WriteLine("Knowledge saved. Thank you for playing!");
        }

        static bool playAgain()
        {
            Console.Write("\nPlay again? (y/n): ");
            char inputCharacter = ' ';
            while (true)
            {
                string input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    inputCharacter = char.ToLower(input[0]);
                    if (inputCharacter == 'y' || inputCharacter == 'n')
                        return inputCharacter == 'y';
                }
                Console.WriteLine("Invalid input. Please enter 'y' or 'n': ");
            }
        }

        static void startNewGame()
        {
            Console.WriteLine("Starting a new game.\n");
            Console.WriteLine("Enter a qyes or no question: ");
            string question = Console.ReadLine();
            Console.Write("Enter a guess if the response is Yes: ");
            string yesGuess = Console.ReadLine();
            Console.Write("Enter a guess if the response is No: ");
            string noGuess = Console.ReadLine();
            tree = new BTTree(question, yesGuess, noGuess);
        }

        static void SaveTree(BTTree tree, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                SaveNode(tree.rootNode, writer);
            }
        }

        static void SaveNode(TreeNode node, StreamWriter writer)
        {
            if (node == null) return;

            // Write the current node's question
            writer.WriteLine(node.question);
            // Write indicators for child nodes
            writer.WriteLine(node.yesNode != null ? "Y" : "N");
            writer.WriteLine(node.noNode != null ? "Y" : "N");

            // Recursively save child nodes
            SaveNode(node.yesNode, writer);
            SaveNode(node.noNode, writer);
        }

        static BTTree LoadTree(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                TreeNode root = LoadNode(reader);
                return new BTTree(root);
            }
        }

        static TreeNode LoadNode(StreamReader reader)
        {
            // Read the current node's question
            string question = reader.ReadLine();
            string hasYes = reader.ReadLine();
            string hasNo = reader.ReadLine();

            TreeNode node = new TreeNode(question);
            if (hasYes == "Y") node.yesNode = LoadNode(reader);
            if (hasNo == "Y") node.noNode = LoadNode(reader);

            return node;
        }
    }
}
