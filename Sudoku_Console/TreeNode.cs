using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Console
{
    class TreeNode<T>
    {
        List<TreeNode<T>> Children;

        T Item { get; set; }

        public TreeNode(T item)
        {
            Item = item;
            Children = new List<TreeNode<T>>();
        }

        public TreeNode<T> AddChild(T item)
        {
            TreeNode<T> nodeItem = new TreeNode<T>(item);
            Children.Add(nodeItem);
            return nodeItem;
        }

        public override string ToString()
        {
            return "Value: " + Item;
        }

        public void Expand(TreeNode<int> root, int indexOfEmptySlot)
        {
            var list = ImportCSV.PossibilityList(indexOfEmptySlot);
            if (root.Children.Count == 0)
            {
                if (list == null)
                {
                    return;
                }
                for (int i = 0; i < list.Length; i++)
                {
                    ImportCSV.AddValue(indexOfEmptySlot, list[i]);
                    root.AddChild(list[i]);
                    if (ImportCSV.Rule() == true && indexOfEmptySlot <= 23)
                    {
                        ImportCSV.PrintSudoku();
                        return;
                    }
                    foreach (var child in root.Children)
                    {
                        Expand(child, indexOfEmptySlot + 1);
                    }
                }
            }
        } 

        public static void PrintTree(TreeNode<T> root)
        {
            foreach (TreeNode<T> node in root.Children)
            {          
                Console.WriteLine(node);
                PrintTree(node);
            }
        }
    }
}