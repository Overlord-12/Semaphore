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
        static object locker = new();

        public MyMonitorSemaphore(int count)
        {
            _count = count;
        }

        public void Acquire()
        {
            lock (locker)
            {
                if (_count > 0)
                    _count--;
                else if (_count == 0)
                {
                    Monitor.Wait(locker);
                }
            }

        }

        public int Release(int releaseCount)
        {
            lock (locker)
            {
                int result = _count;
                Monitor.PulseAll(locker);

                _count++;
                return result;
            }
        }

        public bool TryAcquire()
        {
            lock (locker)
            {
                return Monitor.TryEnter(locker);
            }
        }
    }
}
