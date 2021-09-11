using System.Collections.Generic;
using System.Reflection;

namespace TracerLibrary
{
    public class ThreadInfo
    {
        public ThreadInfo(int id, List<MethodInfo> threadMethods)
        {
            Id = id;
            Methods = new List<MethodInfo>();
            Methods = threadMethods;
        }
        public ThreadInfo() {}
        
        public int Id { set; get; }
        public List<MethodInfo> Methods { set; get; }

        private double executionTime;
        public double ExecutionTime
        {
            get
            {
                executionTime = GetGeneralExecutionTime(Methods[0]);
                return executionTime;
            }
        }
        
        private double GetGeneralExecutionTime(MethodInfo methodInfo)
        {
            double time = 0;
            time += methodInfo.ExecutionTime;
            foreach (MethodInfo method in methodInfo.Methods)
            {
                if (method.Methods.Count > 0)
                {
                    time += GetGeneralExecutionTime(method);
                }
                time += method.ExecutionTime;
            }
            return time;
        }

    }
}