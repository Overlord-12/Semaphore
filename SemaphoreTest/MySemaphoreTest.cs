using Semaphore;

namespace SemaphoreTest
{
    [TestClass]
    public class MySemaphoreTest
    {
        private ISemaphore mySemaphore;
        Thread th_1;
        Thread th_2;
        Thread th_3;
        Thread th_4;


        [TestInitialize]
        public void MonitorContext()
        {
            mySemaphore = new MyMonitorSemaphore(4);

            th_1 = new Thread(mySemaphore.Acquire);
            th_2 = new Thread(mySemaphore.Acquire);
            th_3 = new Thread(mySemaphore.Acquire);
            th_4 = new Thread(mySemaphore.Acquire);

            th_1.Priority = ThreadPriority.BelowNormal;
            th_2.Priority = ThreadPriority.BelowNormal;
            th_3.Priority = ThreadPriority.Normal;
            th_4.Priority = ThreadPriority.Lowest;

            th_1.Start();
            th_2.Start();
            th_3.Start();
            th_4.Start();

            th_1.Join();
            th_2.Join();
            th_3.Join();
            th_4.Join();
        }

        [TestMethod]
        public void TestTryMultiAcquire()
        {
            var result = mySemaphore.TryAcquire();

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void TestTrySingleAcquire()
        {
            mySemaphore.Release(4);
            var result = mySemaphore.TryAcquire();

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestTryRelease()
        {
            int result = mySemaphore.Release(3);

            Assert.AreEqual(0, result);
        }
    }
}