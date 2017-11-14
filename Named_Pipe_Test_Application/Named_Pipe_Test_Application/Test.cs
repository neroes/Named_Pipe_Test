using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Pipes;
using System.IO;
namespace Named_Pipe_Test_Application
{
    class Test
    {
        public NamedPipeServerStream namedPipeServerStream;
        public Test()
        {
            namedPipeServerStream = new NamedPipeServerStream("pizzastream");
            StartPythonParts();
            
            namedPipeServerStream.WaitForConnection();
            StreamWriter writer = new StreamWriter(namedPipeServerStream);
            StreamReader reader = new StreamReader(namedPipeServerStream);
            writer.WriteLine("pizza");
            System.Console.WriteLine(reader.ReadLine());
        }
        public void StartPythonParts()
        {
            /*string fileName = @"C:\Users\soren\Documents\Visual Studio 2017\Projects\Named_Pipe_Test_Application\Python_Pipe\Python_Pipe.py";

            Process p = new Process();
            p.StartInfo = new ProcessStartInfo(@"C:\Program Files\Python36\python.exe", fileName)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            return p.Start();*/
            // full path of python interpreter 
            string python = @"C:\Program Files\Python36\python.exe";

            // python app to call 
            string myPythonApp = @"C:\PythonScripts\Python_Pipe.py"; 

              // dummy parameters to send Python script 
              /*int x = 2;
              int y = 5;*/

              // Create new process start info 
              ProcessStartInfo myProcessStartInfo = new ProcessStartInfo(python);

            // make sure we can read the output from stdout 
            myProcessStartInfo.UseShellExecute = false;
            myProcessStartInfo.RedirectStandardOutput = true;

            // start python app with 3 arguments  
            // 1st arguments is pointer to itself,  
            // 2nd and 3rd are actual arguments we want to send 
            myProcessStartInfo.Arguments = myPythonApp;

            Process myProcess = new Process();
            // assign start information to the process 
            myProcess.StartInfo = myProcessStartInfo;

            Console.WriteLine("Calling Python script");
            // start the process 
            myProcess.Start();
            Console.WriteLine("Script Started");
            StreamReader myStreamReader = myProcess.StandardOutput;
            string myString = myStreamReader.ReadLine();
            Console.WriteLine("Value received from script: " + myString);


            // Read the standard output of the app we called.  
            // in order to avoid deadlock we will read output first 
            // and then wait for process terminate: 
            /*StreamReader myStreamReader = myProcess.StandardOutput;
            string myString = myStreamReader.ReadLine();

            /*if you need to read multiple lines, you might use: 
                string myString = myStreamReader.ReadToEnd() 

            // wait exit signal from the app we called and then close it. 
            myProcess.WaitForExit();
            myProcess.Close();

            // write the output we got from python app 
            Console.WriteLine("Value received from script: " + myString);*/
        }
    }
}
