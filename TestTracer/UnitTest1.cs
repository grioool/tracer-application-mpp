using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using TracerLibrary;

namespace TracerLibrary.Tests
{
    public class TracerLibraryTests
    {

        public Tracer Tracer = new Tracer();

        private readonly List<Thread> _threads = new List<Thread>();

        int ThreadsCount = 10;
        int MethodsCount = 10;

        int MillisecondsTimeout = 200;

        private void Method()
        {
            Tracer.StartTrace();
            Thread.Sleep(MillisecondsTimeout);
            Tracer.StopTrace();
        }

        [Test]
        public void ExecutionTime()
        {
            Method();
            TraceResult traceResult = Tracer.GetTraceResult();
            double methodTime = traceResult.ThreadsInfo[0].Methods[0].ExecutionTime;
            double threadTime = traceResult.ThreadsInfo[0].ExecutionTime;
            Assert.IsTrue(methodTime >= MillisecondsTimeout);
            Assert.IsTrue(threadTime >= MillisecondsTimeout);
        }

        [Test]
        public void ThreadCount()
        {
            for (int i = 0; i < ThreadsCount; i++)
            {
                _threads.Add(new Thread(Method));
            }

            foreach (Thread thread in _threads)
            {
                thread.Start();
                thread.Join();
            }

            TraceResult traceResult = Tracer.GetTraceResult();
            Assert.AreEqual(ThreadsCount, traceResult.ThreadsInfo.Count);
        }

        [Test]
        public void MethodCount()
        {
            for (int i = 0; i < MethodsCount; i++)
            {
                Method();
            }

            TraceResult traceResult = Tracer.GetTraceResult();
            Assert.AreEqual(MethodsCount, traceResult.ThreadsInfo[0].Methods.Count);
        }

        [Test]
        public void Name()
        {
            Tracer.StartTrace();
            Tracer.StopTrace();
            TraceResult traceResult = Tracer.GetTraceResult();
            Assert.AreEqual(nameof(Name), traceResult.ThreadsInfo[0].Methods[0].Name);
            Assert.AreEqual(nameof(TracerLibraryTests), traceResult.ThreadsInfo[0].Methods[0].ClassName);
            Assert.AreEqual(Thread.CurrentThread.ManagedThreadId, traceResult.ThreadsInfo[0].Id);
        }
    }
}
