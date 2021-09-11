using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Xml.Serialization;
using TracerLibrary;


namespace TracerApp
{
    class Program
    {
        public class Foo
        {
            private Bar _bar;
            private ITracer _tracer;

            internal Foo(ITracer tracer)
            {
                _tracer = tracer;
                _bar = new Bar(_tracer);
            }

            public void MyMethod()
            {
                _tracer.StartTrace();
                _bar.InnerMethod();
                _tracer.StopTrace();
            }
        }

        public class Bar
        {
            private ITracer _tracer;

            internal Bar(ITracer tracer)
            {
                _tracer = tracer;
            }

            public void InnerMethod()
            {
                _tracer.StartTrace();

                Thread.Sleep(100);
                _tracer.StopTrace();
            }
        }

        public void M1(object o)
        {
            Tracer tracer = (Tracer) o;
            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            Thread thread = new Thread(program.M1);
            ITracer tracer = new Tracer();
            Foo foo = new Foo(tracer);
            foo.MyMethod();
            thread.Start(tracer);
            thread.Join();
            
            Writer writer = new Writer();
            TraceResult traceResult = tracer.GetTraceResult();
            string resultJson = JsonSerializer.Serialize<TraceResult>(traceResult);
            XmlSerializer formatter = new XmlSerializer(typeof(TraceResult));
            using (FileStream fs = new FileStream("tracer.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, traceResult);
            }
            writer.WriteToFile("tracer.json", resultJson);
            writer.WriteToConsole(resultJson);
        }
    }
}