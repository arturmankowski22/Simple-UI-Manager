using System;
using System.Collections.Generic;

namespace SUIM
{
    public class CyclicOverwriteStack<T> where T : class
    {
        public bool IsEmpty => Peek() == null;
            
        private int _currentIndex; 
        private readonly int _maxSize;
        private readonly List<T> _list;

        public CyclicOverwriteStack(int size)
        {
            if (size <= 0)
                throw new Exception($"Length must be at least 1 (was {size})");
            
            _maxSize = size;
            _list = new List<T>(size);
            for (var i = 0; i < size; i++)
                _list.Add(null);
        }

        public void Push(T item)
        {
            _list[_currentIndex] = item;
            _currentIndex = (_currentIndex + 1) % _maxSize;
        }

        public T Pop()
        {
            if (IsEmpty)
                return null;

            if (_currentIndex == 0)
            {
                if (_list[_maxSize - 1] != null)
                    _currentIndex = _maxSize - 1;
                else
                    return null;
            }
            else
            {
                _currentIndex--;
            }
                
            var lastIndex = _currentIndex % _maxSize;
            var item = _list[lastIndex];
            _list[lastIndex] = null;
            return item;
        }

        public T Peek()
        {
            int lastIndex;
            if (_currentIndex == 0)
            {
                if (_list[_maxSize - 1] != null)
                    lastIndex = _maxSize - 1;
                else
                    return null;
            }
            else
                lastIndex = (_currentIndex - 1) % _maxSize;
                
            return _list[lastIndex];
        }
    }
}