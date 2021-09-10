using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace TracerLibrary
{
    public class Tracer : ITracer
    {
        private readonly ConcurrentDictionary<int, ThreadTracer> _threadTracers;
        
        public Tracer()
        {
            _threadTracers = new ConcurrentDictionary<int, ThreadTracer>();
        }

        public void StartTrace()
        {
            ThreadTracer threadTracer = GetCurrentThreadTracer();
            
            if (threadTracer == null)
            {
                int currentThreadId = Thread.CurrentThread.ManagedThreadId;
                threadTracer = new ThreadTracer();       
                _threadTracers.TryAdd(currentThreadId, threadTracer);
                
            }
            threadTracer.StartTrace();
        }
        
        private ThreadTracer GetCurrentThreadTracer()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;         
            ThreadTracer threadTracer;
            _threadTracers.TryGetValue(threadId, out threadTracer);
            return threadTracer;
        }
        
        public void StopTrace()
        {
            ThreadTracer threadTracer = GetCurrentThreadTracer();
            threadTracer.StopTrace();
        }

        public TraceResult GetTraceResult()
        {
            var threadsInfo = new List<ThreadInfo>();
            foreach (var thread in _threadTracers)
            {
                threadsInfo.Add(new ThreadInfo(thread.Key, thread.Value.GetThreadMethodList()));
            }
            return new TraceResult(threadsInfo);
        }
    }
}