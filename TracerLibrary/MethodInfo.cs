using System.Collections.Generic;

namespace TracerLibrary
{
    
    public class MethodInfo
    {
        public string Name { set;  get; }

        public string ClassName { set;  get; }

        public double ExecutionTime { set;  get; }

        public List<MethodInfo> Methods { set;  get; }
        
        protected MethodInfo() {}

        public MethodInfo(string name, string className, double executionTime, List<MethodInfo> methods)
        {
            Name = name;
            ClassName = className;
            ExecutionTime = executionTime;
            Methods = new List<MethodInfo>(methods);
        }
    }
}