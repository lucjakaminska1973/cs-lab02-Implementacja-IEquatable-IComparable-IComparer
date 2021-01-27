using System;
using ver1;
using zadanie1;

namespace zadanie2
{
    class Program
    {
        static void Main(string[] args)
        {
            var xerox = new Copier();
            xerox.PowerOn();
            IDocument doc1 = new PDFDocument("aaa.pdf");
            xerox.Print(in doc1);

            IDocument doc2;
            xerox.Scan(out doc2);

            xerox.ScanAndPrint();

            MultiFunctionalDevice device = new MultiFunctionalDevice();
            device.PowerOn();

            IDocument doc3;
            device.Fax(out doc3);
            device.ScanAndPrint();
            IDocument doc4;
            device.Scan(out doc4);
            IDocument doc5 = new PDFDocument("bbb.pdf");
            device.Print(doc5);

            IDocument document = new ImageDocument("image.jpg");
            device.Fax(out document);

            Console.WriteLine(xerox.Counter);
            Console.WriteLine(xerox.PrintCounter);
            Console.WriteLine(xerox.ScanCounter);
        }
    }
    public interface IFax : IDevice
    {
        /// <summary>
        /// Dokument jest wysyłany, jeśli urządzenie włączone. W przeciwnym przypadku nic się nie wykonuje
        /// </summary>
        /// <param name="document">obiekt typu IDocument, różny od `null`</param>
        void Fax(out IDocument documentOut);
    }

    public class MultiFunctionalDevice : Copier, IFax
    {
        public int FaxCounter { get; private set; } = 0;

        public void Fax(out IDocument documentOut)
        {
            GetState();
            if (state == IDevice.State.off)
            {
                documentOut = null;

            }
            else
            {
                FaxCounter++;
                documentOut = new ImageDocument("FaxImage" + FaxCounter + ".jpg");
                Console.WriteLine($"{DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss")} Fax: {documentOut.GetFileName()}");
            }
        }
    }
}
