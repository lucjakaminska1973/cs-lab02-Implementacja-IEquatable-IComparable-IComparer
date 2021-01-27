using System;
using ver1;
using zadanie3;

namespace zadanie3
{
    class Program
    {
        static void Main(string[] args)
        {
            Printer printer = new Printer();
            printer.PowerOn();

            IDocument doc = new PDFDocument("aaa.pdf");
            printer.Print(doc);
            printer.Print(doc);

            printer.PowerOff();
            printer.Print(new PDFDocument("AAA.pdf"));
            Console.WriteLine("Printer => Number printed documents: " + printer.PrintCounter);
            Console.WriteLine("Printer => Number starts: " + printer.Counter);

            printer.PowerOn();

            Scanner scanner = new Scanner();
            scanner.PowerOn();

            IDocument doc1 = new TextDocument("bbb.pdf");
            scanner.Scan(out doc1, doc1.GetFormatType());
            scanner.Scan(out doc1, doc1.GetFormatType());
            scanner.Scan(out doc1, doc1.GetFormatType());
            printer.Print(doc1);

            Console.WriteLine("Scanner => Number scans: " + scanner.ScanCounter);

            IDocument doc2 = new ImageDocument("ccc.pdf");

            Copier copier = new Copier();

            copier.scanner.PowerOn();
            copier.scanner.Scan(out doc2, doc2.GetFormatType());
            copier.scanner.PowerOff();

            copier.scanner.PowerOn();
            copier.scanner.PowerOff();
            copier.scanner.PowerOn();

            copier.printer.PowerOn();
            copier.printer.PowerOn();

            copier.printer.Print(doc2);

            copier.printer.PowerOn();
            copier.printer.PowerOff();

            copier.scanner.PowerOff();

            Console.WriteLine(copier.scanner.ScanCounter);
            Console.WriteLine(copier.printer.PrintCounter);

            MultidimensionalDevice device = new MultidimensionalDevice();

            IDocument doc3 = new PDFDocument("ddd.pdf");
            device.printer.PowerOn();
            device.printer.Print(doc3);

            device.scanner.PowerOn();
            device.scanner.Scan(out doc3, doc3.GetFormatType());

            device.scanner.PowerOff();
            device.printer.PowerOff();

            device.Fax(out doc3);

            Console.WriteLine(device.printer.Counter);
            Console.WriteLine(device.printer.Counter);

            Console.WriteLine(device.printer.PrintCounter);
            Console.WriteLine(device.scanner.ScanCounter);
        }
    }
}
