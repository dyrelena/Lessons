using System;

namespace PrinterMFU
{
    class Program
    {
        static void Main(string[] args)
        {
            Xerox test = new ColorXeroxA4();
            test.Copy("Paper Source - New copy");

            MFU test1 = new MFUColorA4 (new ColorPrinter(), new ScannerA4());
            string document = test1.Scan("text from paper source");
            test1.Print(document);
            test1.Copy("Paper Source - 1 - New copy");

            IPrinter prn = new MFUColorA4(new ColorPrinter());
            prn.Print("!!!!!!!!!!!");

        }
    }

    //решение
    abstract class Xerox
    {
        private protected MyPrinter printer;
        private protected MyScanner scanner;
        public virtual void Copy(string PaperSource)
        {
            printer.Print(scanner.Scan(PaperSource));
        }
    }

    class ColorXeroxA4 : Xerox
    {
        public ColorXeroxA4()
        {
            printer = new ColorPrinter();
            scanner = new ScannerA4();
        }
    }
    class ColorXeroxA3 : Xerox
    {
        public ColorXeroxA3()
        {
            printer = new ColorPrinter();
            scanner = new ScannerA3();
        }
    }

    abstract class MFU : IPrinter, IScanner
    {
        public MyPrinter printer;
        public MyScanner scanner;
      
        public void Print(string someFile)
        {
            this.printer.Print(someFile);
        }
        public string Scan(string PaperSource)
        {
            return this.scanner.Scan(PaperSource);
        }
        public void Copy(string PaperSource)
        {
            this.printer.Print(this.scanner.Scan(PaperSource));
        }
    }

    class MFUColorA4 : MFU
    {
        public MFUColorA4(ColorPrinter colorPrinter, ScannerA4 scannerA4) 
        {
            printer = colorPrinter;
            scanner = scannerA4;
        }
        public MFUColorA4(ColorPrinter colorPrinter)
        {
            printer = colorPrinter;
        }
        public MFUColorA4(ScannerA4 scannerA4)
        {   
            scanner = scannerA4;
        }
    }
    //preconditions

        interface IPrinter
    {
        void Print(string someFile);
    }

    interface IScanner
    {
        string Scan(string PaperSource);
    }
    abstract class MyPrinter : IPrinter
    {
        public readonly bool color;
        public readonly string paperFormat;

        public MyPrinter(bool color, string paperFormat)
        {
            this.color = color;
            this.paperFormat = paperFormat;
        }
        public virtual void Print(string someFile)
        {
            Console.WriteLine($"Print file: {someFile}");
        }
    }

    class ColorPrinter : MyPrinter
    {
        public ColorPrinter() : base(true, "A4") { }
        public override void Print(string someFile)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            base.Print(someFile);
            Console.ResetColor();
        }
    }

    public abstract class MyScanner : IScanner
    {
        public readonly string format;
        public string document;

        public MyScanner(string format) 
        {
            this.format = format;
        }
        public string Scan(string PaperSource)
        {
            document = PaperSource;
            return document;
        }

    }
    class ScannerA4 : MyScanner
    {
        public ScannerA4() : base("A4") { }
    }
    class ScannerA3 : MyScanner
    {
        public ScannerA3() : base("A3") { }
    }

    
}
