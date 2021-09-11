using System.Collections.Generic;
using System.Reflection;

namespace TracerLibrary
{
    public class ThreadInfo
    {
        
        public ThreadInfo() {}
        
        public ThreadInfo(int id, List<MethodInfo> threadMethods)
        {
            Id = id;
            Methods = new List<MethodInfo>();
            Methods = threadMethods;
        }

        public int Id { set; get; }
        public List<MethodInfo> Methods { set; get; }

        private double executionTime;
        
        public double ExecutionTime
        {
            get
            {
                executionTime = SumMethodsExecutionTime(Methods[0]);
                return executionTime;
            }
        }
        
        private double SumMethodsExecutionTime(MethodInfo methodInfo)
        {
            double time = 0;
            time += methodInfo.ExecutionTime;
            foreach (MethodInfo method in methodInfo.Methods)
            {
                if (method.Methods.Count > 0)
                {
                    time += SumMethodsExecutionTime(method);
                }
                time += method.ExecutionTime;
            }
            return time;
        }

    }
}