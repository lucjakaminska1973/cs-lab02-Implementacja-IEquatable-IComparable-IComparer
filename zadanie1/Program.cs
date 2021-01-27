using System;
using System.Globalization;
using ver1;

namespace zadanie1
{
    class Program
    {
        static void Main()
        {
            var xerox = new Copier();
            xerox.PowerOn();
            IDocument doc1 = new PDFDocument("aaa.pdf");
            xerox.Print(in doc1);

            IDocument doc2;
            xerox.Scan(out doc2);

            xerox.ScanAndPrint();
            System.Console.WriteLine(xerox.Counter);
            System.Console.WriteLine(xerox.PrintCounter);
            System.Console.WriteLine(xerox.ScanCounter);
        }
    }


    public class Copier : BaseDevice, IPrinter, IScanner
    {
        //int printcounter = 0, scancounter = 0, counter = 0;
        public int PrintCounter { get; private set; }
        public int ScanCounter { get; private set; }



        //public static string ActDateString() => DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");

        public void Print(in IDocument document)
        {
            GetState();
            if (state == IDevice.State.on)
            {
                PrintCounter++;
                Console.WriteLine($"{DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss")} Print: {document.GetFileName()}");
            }

        }

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
        {
            GetState();
            if (state == IDevice.State.off)
            {
                document = null;
            }
            else
            {
                ++ScanCounter;
                switch (formatType)
                {
                    case IDocument.FormatType.JPG:

                        document = new ImageDocument("ImageScan" + ScanCounter + ".jpg");
                        break;
                    case IDocument.FormatType.PDF:
                        document = new PDFDocument("PDFScan" + ScanCounter + ".pdf");
                        break;
                    case IDocument.FormatType.TXT:
                        document = new TextDocument("TextScan" + ScanCounter + ".txt");
                        break;
                    default:
                        document = new ImageDocument("ImageScan" + ScanCounter + ".jpg");
                        break;
                }
                Console.WriteLine($"{DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss")} Scan: {document.GetFileName()}");
            }

        }

        public void ScanAndPrint()
        {
            IDocument document;
            Scan(out document, IDocument.FormatType.JPG);
            Print(document);
        }

    }
}
