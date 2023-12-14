using CommandLine.Text;
using CommandLine;
using Codecrete.SwissQRBill.Generator;
using System.Runtime.CompilerServices;

internal class Program
{
    public class Options
    {
  
        [Option('a',"amount",Required =true,HelpText ="Set amount of Invoice")]
        public decimal Amount { get; set; }

        [Option('i',"iban", Required =true,HelpText ="IBAN of receiver")]
        public string? Iban { get; set; }

        [Option('c',"currency",Required =false,Default ="CHF",HelpText ="Currency of Invoice")]
        public string? Currency { get; set; }

        [Option('l',"language",Required =false,Default = Language.DE,HelpText ="Possible Values DE,FR,IT,EN - case sensitive!")]
        public Language Language { get; set; }

        [Option('f',"format",Required =false,Default = GraphicsFormat.SVG, HelpText="Possible Values SVG, PDF - case sensitive!")]
        public GraphicsFormat Format { get; set; }

        [Option('o',"outputsize",Required =false,Default = OutputSize.QrBillOnly, HelpText="Possible Values QrBillOnly, QrCodeOnly")]
        public OutputSize OutputSizeFormat { get; set; }

        [Option('p',"savepath",Required =true,HelpText ="local path to save file")]
        public string SavePath { get; set; }

        [Option('r',"reference",Required =false,HelpText ="valid reference number")]
        public string Reference { get; set; }

        [Option('m',"message",Required =false, HelpText="message on Invoice")]
        public string Message { get; set; }

        [Option("creditor_name",Required =true)]
        public string CreditorName { get; set; }

        [Option("creditor_address1",Required =false)]
        public string CreditorAdress1 { get;set; }

        [Option("creditor_address2",Required =false)]
        public string CreditorAdress2 { get; set; }

        [Option("creditor_countrycode",Required =true,Default ="CH")]
        public string CreditorCountryCode { get; set; }

        [Option("debitor_name",Required =false)]
        public string DebitorName { get; set; }

        [Option("debitor_address1",Required =false)]
        public string DebitorAddress1 { get; set; }

        [Option("debitor_address2", Required = false)]
        public string DebitorAddress2 { get; set; }

        [Option("debitor_countrycode", Required = false,Default = "CH")]
        public string DebitorCountryCode { get; set; }

    }

    private static void Main(string[] args)
    {
        CommandLine.Parser.Default.ParseArguments<Options>(args)
    .   WithParsed(RunOptions)
        .WithNotParsed(HandleParseError);
    }
    

    static void RunOptions(Options opts)
    {

#pragma warning disable CS8604 // Possible null reference argument.
        Bill bill = new Bill()
        {
            Amount = opts.Amount,
            Account = opts.Iban,
            Currency = opts.Currency,
            Creditor = new Address
            {
                Name = opts.CreditorName,
                AddressLine1 = opts.CreditorAdress1,
                AddressLine2 = opts.CreditorAdress2,
                CountryCode = opts.CreditorCountryCode
            },
            Debtor = new Address
            {
                Name= opts.DebitorName,
                AddressLine1 = opts.DebitorAddress1,
                AddressLine2 = opts.DebitorAddress2,
                CountryCode = opts.DebitorCountryCode

            },

            // more payment data
            Reference = opts.Reference,
            UnstructuredMessage = opts.Message,
     

            Format = new BillFormat
            {
                Language = opts.Language,
                GraphicsFormat = opts.Format,
                OutputSize =  opts.OutputSizeFormat

            }

        };

        ValidationResult validationResult = QRBill.Validate(bill);

        // Generate QR bill
        byte[] svg = QRBill.Generate(bill);

        // Save generated SVG file        
        File.WriteAllBytes(opts.SavePath, svg);
        Console.WriteLine($"QR bill saved at {Path.GetFullPath(opts.SavePath)}");


#pragma warning restore CS8604 // Possible null reference argument.

    }
    static void HandleParseError(IEnumerable<Error> errs)
    {
        //handle errors
    }

  
    
}
