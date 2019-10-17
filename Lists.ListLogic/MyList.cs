using System;
using System.Collections;

namespace Lists.ListLogic
{
    /// <summary>
    /// Die Liste verwaltet beliebige Elemente und implementiert
    /// das IList-Interface und damit auch ICollection und IEnumerable
    /// </summary>
    public class MyList : IList
    {
        Node _head;

        #region IList Members

        /// <summary>
        /// Ein neues Objekt wird in die Liste am Ende
        /// eingefügt. Etwaige Typinkompatiblitäten beim Vergleich werden
        /// nicht behandelt und lösen eine Exception aus.
        /// </summary>
        /// <param name="value">Einzufügender Datensatz</param>
        /// <returns>Index des Werts in der Liste</returns>
        public int Add(object value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            Node newNode = new Node(value);

            if (_head == null)
            {
                _head = newNode;
            }
            else if (_head.Next == null)
            {
                _head.Next = newNode;
            }
            else
            {
                Node run = _head;
                while (run.Next != null)
                {
                    run = run.Next;
                }
                run.Next = newNode;
            }
            return IndexOf(value);
        }

        /// <summary>
        /// Die Liste wird geleert. Die Elemente werden einfach ausgekettet.
        /// Der GC räumt dann schon auf.
        /// </summary>
        public void Clear()
        {
            _head = null;
        }

        /// <summary>
        /// Gibt es den gesuchten DataObject zumindest ein mal?
        /// </summary>
        /// <param name="value">gesuchter DataObject</param>
        /// <returns></returns>
        public bool Contains(object value)
        {
            return IndexOf(value) != -1;
        }

        /// <summary>
        /// Der DataObject wird gesucht und dessen Index wird zurückgegeben.
        /// </summary>
        /// <param name="value">gesuchter DataObject</param>
        /// <returns>Index oder -1, falls der DataObject nicht in der Liste ist</returns>
        public int IndexOf(object value)
        {
            Node run = _head;
            int idx = -1;
            bool isOnPosition = false;

            while (run != null && isOnPosition == false)
            {
                if (run.DataObject.Equals(value))
                {
                    isOnPosition = true;
                }
                run = run.Next;
                idx++;
            }
            if (!isOnPosition)
            {
                idx = -1;
            }
            return idx;
        }

        /// <summary>
        /// DataObject an bestimmter Position in Liste einfügen.
        /// Es ist auch erlaubt, einen DataObject hinter dem letzten Element
        /// (index == count) einzufügen.
        /// </summary>
        /// <param name="index">Einfügeposition</param>
        /// <param name="value">Einzufügender DataObject</param>
        public void Insert(int index, object value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (index >= 0 && index <= Count)
            {
                Node newNode = new Node(value);
                Node run;
                if (index == 0)
                {
                    run = _head;
                    newNode.Next = run;
                    _head = newNode;
                }
                else
                {
                    run = _head;
                    for (int i = 1; i < index; i++)
                    {
                        run = run.Next;
                    }
                    newNode.Next = run.Next;
                    run.Next = newNode;
                }

            }
        }

        /// <summary>
        /// Wird nicht verwendet ==> Immer false
        /// </summary>
        public bool IsFixedSize
        {
            get { return false; }
        }

        /// <summary>
        /// Wird nicht verwendet ==> Immer false
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Ein DataObject wird aus der Liste entfernt, wenn es ihn 
        /// zumindest ein mal gibt.
        /// </summary>
        /// <param name="value">zu entfernender DataObject</param>
        public void Remove(object value)
        {
            if (value == null)
                return;

            if (Contains(value))
            {
                if (_head.DataObject.Equals(value))
                {
                    _head = _head.Next;
                    return;
                }
                Node run = _head;
                while (run.Next != null)
                {
                    if (run.Next.DataObject.Equals(value))
                    {
                        run.Next = run.Next.Next;
                        return;
                    }
                    run = run.Next;
                }
            }
        }

        /// <summary>
        /// Der DataObject an der Position Index wird entfernt.
        /// Gibt es die Position nicht, geschieht nichts.
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            Node current = _head;
            Node previous = null;
            int count = 0;

            while (count < index && current != null)
            {
                previous = current;
                current = current.Next;
                count++;
            }
            if (current != null)
            {
                if (previous == null)
                {
                    _head = current.Next;
                }
                else
                {
                    previous.Next = current.Next;
                }
            }
        }

        /// <summary>
        /// Indexer zum Einfügen und Lesen von Werten an
        /// gesuchten Positionen.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public object this[int index]
        {
            get
            {
                Node run = _head;
                for (int i = 0; i < index && run != null; i++)
                {
                    run = run.Next;
                }
                return run.DataObject;
            }
            set
            {
                Insert(index, value);
                if (index == 0)
                {
                    RemoveAt(1);
                }
                else
                {
                    RemoveAt(index + 1);
                }
            }
        }

        #endregion

        #region ICollection Members

        /// <summary>
        /// Werte in ein bereits angelegtes Array kopieren.
        /// Ist das übergebene Array zu klein, oder stimmt der
        /// Startindex nicht, geschieht nichts.
        /// </summary>
        /// <param name="array">Zielarray, existiert bereits</param>
        /// <param name="index">Startindex</param>
        public void CopyTo(Array array, int index)
        {
            if (index < 0 || index > Count)
                throw new ArgumentOutOfRangeException();

            if (Count - index <= array.Length)
            {
                if (_head != null)
                {
                    Node run = _head;
                    if (index > 0)
                    {
                        for (int i = 0; i < index && run != null; i++)
                        {
                            run = run.Next;
                        }
                    }

                    for (int i = 0; run != null; i++)
                    {
                        array.SetValue(run.DataObject, i);
                        run = run.Next;
                    }
                }
            }
        }

        /// <summary>
        /// Die Anzahl von Elementen in der Liste wird immer 
        /// explizit gezählt und ist nicht redundant gespeichert.
        /// </summary>

        public int Count
        {
            get
            {
                int count = 0;
                Node tmp = _head;

                while (tmp != null)
                {
                    tmp = tmp.Next;
                    count++;
                }
                return count;
            }
        }

        /// <summary>
        /// Multithreading wird nicht verwendet
        /// </summary>
        public bool IsSynchronized
        {
            get { return false; }
        }

        /// <summary>
        /// Multithreading wird nicht verwendet
        /// </summary>
        public object SyncRoot
        {
            get { return null; }
        }

        #endregion

        public IEnumerator GetEnumerator()
        {
            return new ListEnumerator(_head);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="comparer"></param>
        public static void Sort(MyList list, IComparer comparer)
        {
            if (list._head != null)
            {
                int result = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < list.Count - 1; j++)
                    {
                        IComparable object1 = list[j] as IComparable;
                        IComparable object2 = list[j + 1] as IComparable;

                        if (object1 == null)
                            throw new ArgumentNullException(nameof(object1));
                        if (object2 == null)
                            throw new ArgumentNullException(nameof(object2));

                        if (comparer != null)
                        {
                            result = comparer.Compare(object1,object2);
                        }
                        else
                        {
                            result = object1.CompareTo(object2);
                        }
                        if (result > 0)
                        {
                            object tmp = list[j];
                            list[j] = list[j + 1];
                            list[j + 1] = tmp;
                        }
                    }
                }
            }
        }
        public void Sort(MyList list)
        {
            Sort(list,null);
        }
    }

}
