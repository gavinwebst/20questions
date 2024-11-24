﻿using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;



namespace _20questions
{

    public class TreeNode {
        public string question;
        public TreeNode yesNode;
        public TreeNode noNode;

        //constructor for node 
        public TreeNode(string question) {
            this.question = question;
            this.noNode = null;
            this.yesNode = null;
        }


        //mutator for the nodes
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

        public void setQuestion(string question)
        {
            this.question = question;
        }
        public string getQuestion() { 
            return this.question; 
        }

        //check if there are children nodes for parent node
        public bool isQuestion() {
            if (noNode == null && yesNode == null)
            {
                return false;
            }
            else {
                return true;
            }
        }

        public void query()
        {
            if (!isQuestion())
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
        private void updateTree() {
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

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
