using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            TreeNode<int> root = new TreeNode<int>(0);
            root.Expand(root, 0);
            //TreeNode<int>.PrintTree(root);
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
