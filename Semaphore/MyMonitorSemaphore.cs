using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semaphore
{
     public class MyMonitorSemaphore : ISemaphore
    {
        private int _count;
        object locker = new();
        private int totalCount;

        public MyMonitorSemaphore(int count)
        {
            totalCount = count;
            _count = count;
        }

        public void Acquire()
        {
            bool acquiredLock = _count < totalCount;
            Monitor.Enter(locker, ref acquiredLock);

            if (_count > 0)
                _count--;
        }

        public int Release(int releaseCount)
        {
            if (_count == releaseCount)
                return _count;

            Monitor.Exit(locker);
            _count++;
            return _count;
        }

        public bool TryAcquire()
        {
            return _count > 0;
        }
    }
}
