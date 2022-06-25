using Semaphore;

namespace SemaphoreTest
{
    [TestClass]
    public class MySemaphoreTest
    {
        [TestMethod]
        public void TestTrySingleAcquire()
        {
            MySemaphore mySemaphore = new MySemaphore(1);
            var result = mySemaphore.TryAcquire();

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestTryMultiAcquire()
        {
            MySemaphore mySemaphore = new MySemaphore(1);
            bool result = true;

            Thread myThread = new Thread(mySemaphore.Acquire);
            Thread myThread2 = new Thread(() => { result = mySemaphore.TryAcquire(); });
            myThread.Start();
            myThread2.Start();
            myThread2.Join();

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void TestTryRelease()
        {
            MySemaphore mySemaphore = new MySemaphore(2);
            int result = 0;

            Thread myThread = new Thread(()=> {
                mySemaphore.Acquire();
                result = mySemaphore.Release(1); 
            });
            myThread.Start();
            myThread.Join();

            Assert.AreEqual(1, result);
        }
    }
}