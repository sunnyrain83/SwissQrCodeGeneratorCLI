Command Line interface for creating Swiss QR Invoices

Draft - not all properties are supported yet.

Based on work from 
- https://github.com/manuelbl/SwissQRBill.NET
- https://github.com/commandlineparser/commandline

Valid call:

SwissQrCodeGeneratorCLI.exe --amount 390 --iban CH4431999123000889012 --currency CHF --language DE --format SVG --outputsize QrBillOnly --message "Rechnung für xy" --reference "210000000003139471430009017" --creditor_name "Creditor Name" creditor_address1 "Bahnhofstrasse 20" --creditor_address2 "8002 Zürich" --creditor_countrycode "CH" --savepath "C:\temp\qrcode.svg"
