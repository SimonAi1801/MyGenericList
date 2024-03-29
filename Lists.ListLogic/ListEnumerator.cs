﻿using System.Collections;
using System;
using System.Collections.Generic;

namespace Lists.ListLogic
{
    internal class ListEnumerator<T> : IEnumerator<T>
    {
        private Node<T> _head;
        private Node<T> _currentNode;
        private int _pos = 0;

        public ListEnumerator(Node<T> head)
        {
            _head = head;
            Reset();
        }

        public object Current
        {
            get
            {
                if (_pos == -1 || _currentNode == null)
                {
                    throw new InvalidOperationException();
                }
                return _currentNode.DataObject;
            }
        }

        T IEnumerator<T>.Current
        {
            get
            {
                if (_pos == -1 || _currentNode == null)
                {
                    throw new InvalidOperationException();
                }
                return _currentNode.DataObject;
            }
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (_currentNode != null && _pos >= 0)
            {
                _currentNode = _currentNode.Next;
                _pos++;
            }
            if (_pos == -1)
            {
                _pos++;
                _currentNode = _head;
            }
            return _currentNode != null;
        }

        public void Reset()
        {
            _pos = -1;
        }
    }
}