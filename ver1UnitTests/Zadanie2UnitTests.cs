using Microsoft.VisualStudio.TestTools.UnitTesting;
using ver1;
using zadanie1;
using zadanie2;
using System.IO;
using System;

namespace ver1UnitTests
{
    [TestClass]
    public class UnitTest2
    {
        #region MY TESTS

        public class ConsoleRedirectionToStringWriter : IDisposable
        {
            private StringWriter stringWriter;
            private TextWriter originalOutput;

            public ConsoleRedirectionToStringWriter()
            {
                stringWriter = new StringWriter();
                originalOutput = Console.Out;
                Console.SetOut(stringWriter);
            }

            public string GetOutput()
            {
                return stringWriter.ToString();
            }

            public void Dispose()
            {
                Console.SetOut(originalOutput);
                stringWriter.Dispose();
            }
        }

        [TestMethod]

        public void Multifunctional_Fax_DeviceOn()
        {
            var multi = new MultiFunctionalDevice();
            multi.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                multi.Fax(out doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Fax"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }
        [TestMethod]
        public void Multifunctional_Fax_DeviceOff()
        {
            var multi = new MultiFunctionalDevice();
            multi.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                multi.Fax(out doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Fax"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        #endregion
    }
}
