using System;

namespace BookManager
{
    class IdGenerator
    {
        private int _nextId = 1;

        public int GetNextId()
        {
            return _nextId++;
        }
    }
}