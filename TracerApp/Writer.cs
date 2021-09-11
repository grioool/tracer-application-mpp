using System;
using System.IO;

namespace TracerApp
{
    public class Writer
    {
        public void WriteToFile(string fileName, string traceResult)
        { 
            File.WriteAllText(fileName, traceResult + "");
        }
        
        public void WriteToConsole(string traceResultSerialized)
        {
            Console.WriteLine(traceResultSerialized);
        }
    }
}