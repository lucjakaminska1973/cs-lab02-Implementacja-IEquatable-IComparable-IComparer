using System;
using System.Collections.Generic;
using System.Text;
using ver1;

namespace zadanie3
{
    public class MultidimensionalDevice : IFax
    {
        public Printer printer;
        public Scanner scanner;

        public MultidimensionalDevice()
        {
            printer = new Printer();
            scanner = new Scanner();
        }

        public void Fax(out IDocument document)
        {
            scanner.PowerOn();
            scanner.Scan(out document, IDocument.FormatType.JPG);

            scanner.PowerOff();

            printer.PowerOn();
            printer.Print(document);

            printer.PowerOff();
        }
    }
}
