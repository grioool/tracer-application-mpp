using System.Collections.Concurrent;
using System.Linq;
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
            var threadId = Thread.CurrentThread.ManagedThreadId;         
            ThreadTracer threadTracer;
            _threadTracers.TryGetValue(threadId, out threadTracer);
            return threadTracer;
        }
        
        public void StopTrace()
        {
            var threadTracer = GetCurrentThreadTracer();
            threadTracer.StopTrace();
        }

        public TraceResult GetTraceResult()
        {
            var threadsInfo = _threadTracers.Select(thread => new ThreadInfo(thread.Key, thread.Value.GetThreadMethodList())).ToList();
            return new TraceResult(threadsInfo);
        }
    }
}