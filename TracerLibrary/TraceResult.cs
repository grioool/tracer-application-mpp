using System.Collections.Generic;

namespace TracerLibrary

{
    public class TraceResult
    {
        public TraceResult(List<ThreadInfo> threadsInfo)
        {
            ThreadsInfo = threadsInfo;
        }

        public List<ThreadInfo> ThreadsInfo { get; }
        
    }
}

