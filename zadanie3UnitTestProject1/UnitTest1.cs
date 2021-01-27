using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using zadanie3;
using ver1;

namespace zadanie3UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
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


        [TestClass]
        public class UnitTest
        {
            [TestMethod]
            public void Copier_Scanner_GetState_StateOff()
            {
                var copier = new Copier();
                copier.scanner.PowerOff();

                Assert.AreEqual(IDevice.State.off, copier.scanner.GetState());
            }

            [TestMethod]
            public void Copier_Scanner_GetState_StateOn()
            {
                var copier = new Copier();
                copier.scanner.PowerOn();

                Assert.AreEqual(IDevice.State.on, copier.scanner.GetState());
            }


            // weryfikacja, czy po wywo³aniu metody `Print` i w³¹czonej kopiarce w napisie pojawia siê s³owo `Print`
            // wymagane przekierowanie konsoli do strumienia StringWriter
            [TestMethod]
            public void Copier_Printer_Print_DeviceOn()
            {
                var copier = new Copier();
                copier.printer.PowerOn();

                var currentConsoleOut = Console.Out;
                currentConsoleOut.Flush();
                using (var consoleOutput = new ConsoleRedirectionToStringWriter())
                {
                    IDocument doc1 = new PDFDocument("aaa.pdf");
                    copier.printer.Print(in doc1);
                    Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
                }
                Assert.AreEqual(currentConsoleOut, Console.Out);
            }

            // weryfikacja, czy po wywo³aniu metody `Print` i wy³¹czonej kopiarce w napisie NIE pojawia siê s³owo `Print`
            // wymagane przekierowanie konsoli do strumienia StringWriter
            [TestMethod]
            public void Copier_Printer_Print_DeviceOff()
            {
                var copier = new Copier();
                copier.printer.PowerOff();

                var currentConsoleOut = Console.Out;
                currentConsoleOut.Flush();
                using (var consoleOutput = new ConsoleRedirectionToStringWriter())
                {
                    IDocument doc1 = new PDFDocument("aaa.pdf");
                    copier.printer.Print(in doc1);
                    Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
                }
                Assert.AreEqual(currentConsoleOut, Console.Out);
            }

            // weryfikacja, czy po wywo³aniu metody `Scan` i wy³¹czonej kopiarce w napisie NIE pojawia siê s³owo `Scan`
            // wymagane przekierowanie konsoli do strumienia StringWriter
            [TestMethod]
            public void Copier_Scan_DeviceOff()
            {
                var copier = new Copier();
                copier.scanner.PowerOff();

                var currentConsoleOut = Console.Out;
                currentConsoleOut.Flush();
                using (var consoleOutput = new ConsoleRedirectionToStringWriter())
                {
                    IDocument doc1;
                    copier.scanner.Scan(out doc1);
                    Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                }
                Assert.AreEqual(currentConsoleOut, Console.Out);
            }

            // weryfikacja, czy po wywo³aniu metody `Scan` i wy³¹czonej kopiarce w napisie pojawia siê s³owo `Scan`
            // wymagane przekierowanie konsoli do strumienia StringWriter
            [TestMethod]
            public void Copier_Scan_DeviceOn()
            {
                var copier = new Copier();
                copier.scanner.PowerOn();

                var currentConsoleOut = Console.Out;
                currentConsoleOut.Flush();
                using (var consoleOutput = new ConsoleRedirectionToStringWriter())
                {
                    IDocument doc1;
                    copier.scanner.Scan(out doc1);
                    Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                }
                Assert.AreEqual(currentConsoleOut, Console.Out);
            }

            // weryfikacja, czy wywo³anie metody `Scan` z parametrem okreœlaj¹cym format dokumentu
            // zawiera odpowiednie rozszerzenie (`.jpg`, `.txt`, `.pdf`)
            [TestMethod]
            public void Copier_Scan_FormatTypeDocument()
            {
                var copier = new Copier();
                copier.scanner.PowerOn();

                var currentConsoleOut = Console.Out;
                currentConsoleOut.Flush();
                using (var consoleOutput = new ConsoleRedirectionToStringWriter())
                {
                    IDocument doc1;
                    copier.scanner.Scan(out doc1, formatType: IDocument.FormatType.JPG);
                    Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                    Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));

                    copier.scanner.Scan(out doc1, formatType: IDocument.FormatType.TXT);
                    Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                    Assert.IsTrue(consoleOutput.GetOutput().Contains(".txt"));

                    copier.scanner.Scan(out doc1, formatType: IDocument.FormatType.PDF);
                    Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                    Assert.IsTrue(consoleOutput.GetOutput().Contains(".pdf"));
                }
                Assert.AreEqual(currentConsoleOut, Console.Out);
            }


            // weryfikacja, czy po wywo³aniu metody `ScanAndPrint` i wy³¹czonej kopiarce w napisie pojawiaj¹ siê s³owa `Print`
            // oraz `Scan`
            // wymagane przekierowanie konsoli do strumienia StringWriter
            [TestMethod]
            public void Copier_ScanAndPrint_DeviceOn()
            {
                var copier = new Copier();
                copier.printer.PowerOn();
                copier.scanner.PowerOn();

                var currentConsoleOut = Console.Out;
                currentConsoleOut.Flush();
                using (var consoleOutput = new ConsoleRedirectionToStringWriter())
                {
                    copier.ScanAndPrint();
                    Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                    Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
                }
                Assert.AreEqual(currentConsoleOut, Console.Out);
            }

            // weryfikacja, czy po wywo³aniu metody `ScanAndPrint` i wy³¹czonej kopiarce w napisie NIE pojawia siê s³owo `Print`
            // ani s³owo `Scan`
            // wymagane przekierowanie konsoli do strumienia StringWriter
            [TestMethod]
            public void Copier_ScanAndPrint_DeviceOff()
            {
                var copier = new Copier();
                copier.scanner.PowerOff();
                copier.printer.PowerOff();

                var currentConsoleOut = Console.Out;
                currentConsoleOut.Flush();
                using (var consoleOutput = new ConsoleRedirectionToStringWriter())
                {
                    copier.ScanAndPrint();
                    Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                    Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
                }
                Assert.AreEqual(currentConsoleOut, Console.Out);
            }

            [TestMethod]
            public void Copier_PrintCounter()
            {
                var copier = new Copier();
                copier.printer.PowerOn();

                IDocument doc1 = new PDFDocument("aaa.pdf");
                copier.printer.Print(in doc1);
                IDocument doc2 = new TextDocument("aaa.txt");
                copier.printer.Print(in doc2);
                IDocument doc3 = new ImageDocument("aaa.jpg");
                copier.printer.Print(in doc3);

                copier.printer.PowerOff();
                copier.printer.Print(in doc3);
                copier.scanner.Scan(out doc1);
                copier.scanner.PowerOn();

                copier.ScanAndPrint();
                copier.ScanAndPrint();

                Assert.AreEqual(3, copier.printer.PrintCounter);
            }

            [TestMethod]
            public void Copier_ScanCounter()
            {
                var copier = new Copier();
                copier.scanner.PowerOn();

                IDocument doc1;
                copier.scanner.Scan(out doc1);
                IDocument doc2;
                copier.scanner.Scan(out doc2);

                IDocument doc3 = new ImageDocument("aaa.jpg");
                copier.printer.Print(in doc3);

                copier.scanner.PowerOff();
                copier.printer.Print(in doc3);
                copier.scanner.Scan(out doc1);
                copier.printer.PowerOn();

                copier.scanner.PowerOn();

                copier.ScanAndPrint();
                copier.ScanAndPrint();

                Assert.AreEqual(4, copier.scanner.ScanCounter);
            }

            [TestMethod]
            public void Copier_PowerOnCounter()
            {
                var copier = new Copier();
                copier.scanner.PowerOn();
                copier.printer.PowerOn();
                copier.printer.PowerOn();

                IDocument doc1;
                copier.scanner.Scan(out doc1);
                IDocument doc2;
                copier.scanner.Scan(out doc2);

                copier.scanner.PowerOff();
                copier.scanner.PowerOff();
                copier.printer.PowerOff();
                copier.printer.PowerOn();

                IDocument doc3 = new ImageDocument("aaa.jpg");
                copier.printer.Print(in doc3);

                copier.printer.PowerOff();
                copier.printer.Print(in doc3);
                copier.scanner.Scan(out doc1);
                copier.scanner.PowerOn();

                copier.ScanAndPrint();
                copier.ScanAndPrint();

                Assert.AreEqual(2, copier.printer.Counter);
            }

            #region MY TESTS

            [TestMethod]
            public void Scanner_GetState_StateOff()
            {
                Scanner scanner = new Scanner();
                scanner.PowerOff();

                Assert.AreEqual(IDevice.State.off, scanner.GetState());
            }

            [TestMethod]
            public void Scanner_GetState_StateOn()
            {
                Scanner scanner = new Scanner();
                scanner.PowerOn();

                Assert.AreEqual(IDevice.State.on, scanner.GetState());
            }

            [TestMethod]
            public void Printer_GetState_StateOff()
            {
                Printer printer = new Printer();
                printer.PowerOff();

                Assert.AreEqual(IDevice.State.off, printer.GetState());
            }

            [TestMethod]
            public void Printer_GetState_StateOn()
            {
                Printer printer = new Printer();
                printer.PowerOn();

                Assert.AreEqual(IDevice.State.on, printer.GetState());
            }

            [TestMethod]
            public void Scanner_Scan_DeviceOn()
            {
                Scanner scanner = new Scanner();
                scanner.PowerOn();

                var currentConsoleOut = Console.Out;
                currentConsoleOut.Flush();
                using (var consoleOutput = new ConsoleRedirectionToStringWriter())
                {
                    IDocument doc1 = new PDFDocument("aaa.pdf");
                    scanner.Scan(out doc1);
                    Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                }
                Assert.AreEqual(currentConsoleOut, Console.Out);
            }

            [TestMethod]
            public void Scanner_Scan_DeviceOff()
            {
                Scanner scanner = new Scanner();
                scanner.PowerOff();

                var currentConsoleOut = Console.Out;
                currentConsoleOut.Flush();
                using (var consoleOutput = new ConsoleRedirectionToStringWriter())
                {
                    IDocument doc1 = new PDFDocument("aaa.pdf");
                    scanner.Scan(out doc1);
                    Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                }
                Assert.AreEqual(currentConsoleOut, Console.Out);
            }

            [TestMethod]
            public void Printer_Print_DeviceOff()
            {
                Printer printer = new Printer();
                printer.PowerOff();

                var currentConsoleOut = Console.Out;
                currentConsoleOut.Flush();
                using (var consoleOutput = new ConsoleRedirectionToStringWriter())
                {
                    IDocument doc1 = new PDFDocument("aaa.pdf");
                    printer.Print(doc1);
                    Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
                }
                Assert.AreEqual(currentConsoleOut, Console.Out);
            }

            [TestMethod]
            public void Printer_Print_DeviceOn()
            {
                Printer printer = new Printer();
                printer.PowerOn();

                var currentConsoleOut = Console.Out;
                currentConsoleOut.Flush();
                using (var consoleOutput = new ConsoleRedirectionToStringWriter())
                {
                    IDocument doc1 = new PDFDocument("aaa.pdf");
                    printer.Print(doc1);
                    Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
                }
                Assert.AreEqual(currentConsoleOut, Console.Out);
            }

            [TestMethod]
            public void Scanner_Scan_FormatTypeDocument()
            {
                Scanner scanner = new Scanner();
                scanner.PowerOn();

                var currentConsoleOut = Console.Out;
                currentConsoleOut.Flush();
                using (var consoleOutput = new ConsoleRedirectionToStringWriter())
                {
                    IDocument doc1;
                    scanner.Scan(out doc1, formatType: IDocument.FormatType.JPG);
                    Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                    Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));

                    scanner.Scan(out doc1, formatType: IDocument.FormatType.TXT);
                    Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                    Assert.IsTrue(consoleOutput.GetOutput().Contains(".txt"));

                    scanner.Scan(out doc1, formatType: IDocument.FormatType.PDF);
                    Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                    Assert.IsTrue(consoleOutput.GetOutput().Contains(".pdf"));
                }
                Assert.AreEqual(currentConsoleOut, Console.Out);
            }

            [TestMethod]
            public void Printer_PrintCounter()
            {
                Printer printer = new Printer();
                printer.PowerOn();

                IDocument doc1 = new PDFDocument("aaa.pdf");
                printer.Print(in doc1);
                IDocument doc2 = new TextDocument("aaa.txt");
                printer.Print(in doc2);
                IDocument doc3 = new ImageDocument("aaa.jpg");
                printer.Print(in doc3);

                printer.PowerOff();
                printer.Print(in doc3);

                Assert.AreEqual(3, printer.PrintCounter);
            }

            [TestMethod]
            public void Scanner_ScanCounter()
            {
                Scanner scanner = new Scanner();
                scanner.PowerOn();

                IDocument doc1;
                scanner.Scan(out doc1);
                IDocument doc2;
                scanner.Scan(out doc2);

                IDocument doc3 = new ImageDocument("aaa.jpg");

                scanner.Scan(out doc1);
                scanner.PowerOn();

                Assert.AreEqual(3, scanner.ScanCounter);
            }

            [TestMethod]
            public void MultidimensionalDevice_Fax()
            {
                MultidimensionalDevice device = new MultidimensionalDevice();

                var currentConsoleOut = Console.Out;
                currentConsoleOut.Flush();
                using (var consoleOutput = new ConsoleRedirectionToStringWriter())
                {
                    IDocument doc1 = new PDFDocument("aaa.pdf");
                    device.Fax(out doc1);
                    Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                    Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
                }
                Assert.AreEqual(currentConsoleOut, Console.Out);
            }

            [TestMethod]
            public void MultidimensionalDevice_ScanCounter()
            {
                MultidimensionalDevice device = new MultidimensionalDevice();

                IDocument doc = new PDFDocument("bbb.pdf");

                device.Fax(out doc);
                device.Fax(out doc);

                Assert.AreEqual(2, device.scanner.ScanCounter);
            }

            [TestMethod]
            public void MultidimensionalDevice_PrintCounter()
            {
                MultidimensionalDevice device = new MultidimensionalDevice();

                IDocument doc = new PDFDocument("bbb.pdf");

                device.Fax(out doc);
                device.Fax(out doc);
                device.Fax(out doc);

                Assert.AreEqual(3, device.printer.PrintCounter);
            }

            #endregion
        }
    }
}
