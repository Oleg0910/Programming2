using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace talon_summer_2023
{
    public class SingleLinkedList<T>
    {
        public class Node
        {
            internal T data;
            internal Node next;
            public Node(T d)
            {
                data = d;
                next = null;
            }
        }

        public Node head;

        public bool IsEmpty()
        {
            return head == null;
        }
        public void InsertToHead(T data)
        {
            var node = new Node(data);
            node.next = head;
            head = node;
        }

        public void InsertToTail(T data)
        {
            var node = new Node(data);
            if (head == null)
            {
                head = new Node(data);
                return;
            }
            var temp = head;
            while (temp.next is not null)
            {
                temp = temp.next;
            }
            temp.next = node;
        }
        public void InsertToTail(SingleLinkedList<T> data)
        {
            if (head == null)
            {
                head = data.head;
                return;
            }
            var temp = head;
            while (temp.next != null)
            {
                temp = temp.next;
            }
            temp.next = data.head;
        }
        // function to reverse the list
        public void ReverseList()
        {
            Node prev = null, current = head, next = null;
            while (current != null)
            {
                next = current.next;
                current.next = prev;
                prev = current;
                current = next;
            }
            head = prev;
        }

        public SingleLinkedList<T> ReversedList()
        {
            var new_list = new SingleLinkedList<T>();
            var node = head;
            while (node != null)
            {
                new_list.InsertToHead(node.data);
                node = node.next;
            }

            return new_list;
        }
        // function to print the list data
        public void PrintList()
        {
            Node current = head;
            while (current != null)
            {
                Console.Write(current.data + " ");
                current = current.next;
            }
            Console.WriteLine();
        }

        public string PrintedList()
        {
            string res = "";
            Node current = head;
            while (current != null)
            {
                res += current.data + " ";
                current = current.next;
            }
            return res;
        }
    }
}
