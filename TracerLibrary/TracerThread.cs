using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace TracerLibrary
{
    public class ThreadTracer
    {
        public ThreadTracer()
        {
            _methodTracers = new Stack<MethodTracer>();
            _methodInfoList = new List<MethodInfo>();
        }

        private readonly Stack<MethodTracer> _methodTracers;

        private MethodTracer _currentMethodTracer;

        private readonly List<MethodInfo> _methodInfoList;

        public List<MethodInfo> GetThreadMethodList()
        {
            return _methodInfoList;
        }

        public void StartTrace()
        {
            if (_currentMethodTracer != null)
            {
                _methodTracers.Push(_currentMethodTracer);
            }
            _currentMethodTracer = new MethodTracer();

            _currentMethodTracer.StartTrace();
        }

        public void StopTrace()
        {
            _currentMethodTracer.StopTrace();
            
            StackTrace stackTrace = new StackTrace();
            var methodName = stackTrace.GetFrame(2)?.GetMethod()?.Name;
            var reflectedType = stackTrace.GetFrame(2)?.GetMethod()?.ReflectedType;
            
            if (reflectedType == null) return;
            
            string className = reflectedType.Name;
            double methodExecutionTime = _currentMethodTracer.GetExecutionTime();
            List<MethodInfo> methodInfos = _currentMethodTracer.GetNestedMethods();
            MethodInfo methodInfo = new MethodInfo(methodName, className, methodExecutionTime, methodInfos);
            if (_methodTracers.Count > 0)
            {
                _currentMethodTracer = _methodTracers.Pop();
                _currentMethodTracer.AddNestedMethod(methodInfo);
            }
            else
            {
                _methodInfoList.Add(methodInfo);
            }
        }
    }
}