using System.Collections.Generic;

namespace TracerLibrary

{
    public class TraceResult
    {
        public TraceResult(List<ThreadInfo> threadsInfo)
        {
            ThreadsInfo = threadsInfo;
        }

        public TraceResult() {}
        public List<ThreadInfo> ThreadsInfo { set;  get; }
    }
}

