using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace TracerLibrary
{
    public class ThreadTracer
    {
        public ThreadTracer()
        {
            MethodTracers = new Stack<MethodTracer>();
            MethodInfoList = new List<MethodInfo>();
        }

        private readonly Stack<MethodTracer> MethodTracers;

        private MethodTracer CurrentMethodTracer;

        private readonly List<MethodInfo> MethodInfoList;

        public List<MethodInfo> GetThreadMethodList()
        {
            return MethodInfoList;
        }

        public void StartTrace()
        {
            if (CurrentMethodTracer != null)
            {
                //CurrentMethodTracer.StopTrace();
                MethodTracers.Push(CurrentMethodTracer);
            }
            CurrentMethodTracer = new MethodTracer();

            CurrentMethodTracer.StartTrace();
        }

        public void StopTrace()
        {
            CurrentMethodTracer.StopTrace();
            StackTrace stackTrace = new StackTrace();
            string methodName = stackTrace.GetFrame(2).GetMethod().Name;
            string className = stackTrace.GetFrame(2).GetMethod().ReflectedType.Name;
            double methodExecutionTime = CurrentMethodTracer.GetExecutionTime();
            List<MethodInfo> methodInfos = CurrentMethodTracer.GetChildMethods();
            MethodInfo methodInfo = new MethodInfo(methodName, className, methodExecutionTime, methodInfos);
            if (MethodTracers.Count > 0)
            {
                CurrentMethodTracer = MethodTracers.Pop();
                CurrentMethodTracer.AddChildMethod(methodInfo);
            }
            else
            {
                MethodInfoList.Add(methodInfo);
                CurrentMethodTracer = null;
            }
        }
    }
}