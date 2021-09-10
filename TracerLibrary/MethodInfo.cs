using System.Collections.Generic;

namespace TracerLibrary
{
    
    public class MethodInfo
    {
        public string Name { get; }

        public string ClassName { get; }

        public double ExecutionTime { get; }

        public List<MethodInfo> Methods { get; }

        public MethodInfo(string name, string className, double executionTime, List<MethodInfo> methods)
        {
            Name = name;
            ClassName = className;
            ExecutionTime = executionTime;
            Methods = new List<MethodInfo>(methods);
        }
    }
}