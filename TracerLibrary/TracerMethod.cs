using System.Collections.Generic;
using System.Diagnostics;

namespace TracerLibrary
{
    class MethodTracer
    {
        private readonly Stopwatch _stopwatch;
        private readonly List<MethodInfo> _nestedMethods;
        public MethodTracer()
        {
            _stopwatch = new Stopwatch();
            _nestedMethods = new List<MethodInfo>();
        }

        public void StartTrace()
        {
            _stopwatch.Start();
        }
        public void AddNestedMethod(MethodInfo nestedMethod)
        {
            _nestedMethods.Add(nestedMethod);
        }

        public List<MethodInfo> GetNestedMethods()
        {
            return _nestedMethods;
        }

        public double GetExecutionTime()
        {
            return _stopwatch.ElapsedMilliseconds;
        }

        public void StopTrace()
        {
            _stopwatch.Stop();
        }
    }
}