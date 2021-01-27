using System;
using System.Collections.Generic;
using System.Text;
using ver1;
using zadanie3;

namespace zadanie3
{
    public class Copier
    {
        public Printer printer;
        public Scanner scanner;

        public Copier()
        {
            printer = new Printer();
            scanner = new Scanner();
        }

        public void ScanAndPrint()
        {
            IDocument document;
            scanner.Scan(out document, IDocument.FormatType.JPG);
            printer.Print(in document);
        }
    }
}
