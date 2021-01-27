using System;
using System.Collections.Generic;
using System.Text;
using ver1;

namespace zadanie3
{
    public class Scanner : IScanner
    {
        protected IDevice.State state = IDevice.State.off;
        public int Counter { get; set; }
        public int ScanCounter { get; set; }

        public IDevice.State GetState()
        {
            return state;
        }

        public void PowerOff()
        {
            if (state == IDevice.State.on)
            {
                state = IDevice.State.off;
            }
        }

        public void PowerOn()
        {
            if (state == IDevice.State.off)
            {
                state = IDevice.State.on;
                Counter++;
            }
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
        {
            if (state == IDevice.State.on)
            {
                switch (formatType)
                {
                    case IDocument.FormatType.PDF:
                        document = new PDFDocument("PDFScan" + ScanCounter + ".pdf");
                        break;
                    case IDocument.FormatType.JPG:
                        document = new ImageDocument("ImageScan" + ScanCounter + ".jpg");
                        break;
                    case IDocument.FormatType.TXT:
                        document = new TextDocument("TextScan" + ScanCounter + ".txt");
                        break;
                    default:
                        document = null;
                        break;
                }

                if (document != null)
                {
                    Console.WriteLine("{0} Scan: {1}",
                        DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss"),
                        document.GetFileName());
                    ScanCounter++;
                }
            }
            else
            {
                document = null;
            }
        }
    }
}
