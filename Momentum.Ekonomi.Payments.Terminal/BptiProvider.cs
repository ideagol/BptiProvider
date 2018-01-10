using BPTI;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace Momentum.Ekonomi.Payments.Terminal.Bpti
{
    public class BptiProvider : ICoBpTiX2Events, ITermimal
    {
        IConnectionPointContainer icpc;
        IConnectionPoint icp;

        public BptiProvider()
        {
            API = new CoBptiX3Class();
            CustomMerchantReceiptRows = new List<string>();
            CustomCustomerReceiptRows = new List<string>();
            Initialize();
        }

        public void Initialize()
        {           
            icpc = (IConnectionPointContainer)API;           
            Guid IID_ICoBpTiEvents = typeof(ICoBpTiX2Events).GUID;
            icpc.FindConnectionPoint(ref IID_ICoBpTiEvents, out icp);
            int localCookie;
            icp.Advise(this, out localCookie);
            ConnectionCookie = localCookie;
        }

        public void ReInitialize()
        {
            UnInitialize();
            API = new CoBptiX3Class();
            Initialize();
        }
        public void UnInitialize()
        {
            icp.Unadvise(ConnectionCookie);            
        }

        #region Variables
        public bool TransactionStarted { get; private set; }
        public bool SignatureNeeded { get; private set; }
        public bool ConnectionInitiated { get; private set; }
        public int ConnectionCookie { get;  set; }
        public int PendingTransactionType { get; private set; }
        public int CurrentTransactionType { get; private set; }
        public bool Opened { get; private set; }
        public CoBptiX3Class API { get; set; }


        public List<string> CustomMerchantReceiptRows { get; set; }
        public List<string> CustomCustomerReceiptRows { get; set; }
        public string TransactionNumber { get; set; }
        public bool TransactionAccepted { get; private set; }

        #endregion

        #region BpTiConstants
        //==============================
        //Constanst for start method
        public enum TransactionTypes
        {
            LPP_PURCHASE = 4352,
            LPP_REFUND = 4353,
            LPP_REVERSAL = 4354,
            LPP_CLOSEBATCH = 4358,
            LPP_SIGNATURE = 4000
        };
        enum ResultDataTypes
        {
            rdCUSTOMERRECEIPT = 1,
            rdMERCHANTRECEIPT,
            rdCLOSEBATCHRESULT,
            rdTRMCONFIG,
            rdCURRENTBATCH,
            rdTRANSLOGDETAILED,
            rdTRANSLOGTOTALS,
            rdUNSENTTRANS,
            rdUNDEFINED = -1
        };

        enum ReceiptItems
        {
            riName = 1,
            riAddress = 2,
            riPostAddress = 3,
            riPhone = 4,
            riOrgNbr = 5,
            riBank = 6,
            riMerchantNbr = 7,
            riTrmId = 8,
            riTime = 9,
            riAmount = 10,
            riTotal = 11,
            riVAT = 12,
            riCashBack = 13,
            riCardNo = 14,
            riCardNoMasked = 15,
            riExpDate = 16,
            riCardProduct = 17,
            riAccountType = 18,
            riAuthInfo = 19,
            riReferenceNo = 20,
            riAcceptanceText = 21,
            riIdLine = 22,
            riSignatureLine = 23,
            riRefundAcceptanceText = 24,
            riCashierSignatureText = 25,
            riCashierNameText = 26,
            riTxnType = 27,
            riSaveReceipt = 28,
            riCustomerEx = 29,
            riPinUsed = 30,
            riEnd = -1,
        };
        enum ResultDataValues { rdEnd = -1 };

       
        #endregion
        
        #region BPTIEvents



        public void terminalDspEvent(ref string row1, ref string row2, ref string row3, ref string row4)
        {
            RaiseTerminalDisplayEvent(new string[] { row1, row2, row3, row4 });
        }

        public void infoEvent(ref string text)
        {
            EventHandler displayEventhandler = TerminalInformationEvent;

            DisplayEventArgs arg = new DisplayEventArgs();
            arg.Items.Add(text);
            if (displayEventhandler != null)
            {
                displayEventhandler(this, arg);
            }
        }

        public void exceptionEvent(ref string text, int code)
        {
            RaiseTerminalExceptionEvent(new string[] { text, code.ToString()});
        }

        public void txnResultEvent(int txnType, int resultCode, ref string text, ref string clearingCompany)
        {

            if (txnType == (int)TransactionTypes.LPP_PURCHASE || txnType == (int)TransactionTypes.LPP_REFUND || txnType == (int)TransactionTypes.LPP_REVERSAL || txnType == (int)TransactionTypes.LPP_SIGNATURE)
            {
                if (resultCode != 0)
                {
                    TransactionAccepted = false;
                }
                else
                {
                    TransactionAccepted = true;
                }

                API.merchantReceipt();
            }
            else if (txnType == (int)TransactionTypes.LPP_CLOSEBATCH)
            {
                API.batchReport();
            }

            RaiseTerminalInformationEvent(new string[] { text });
        }
        
        public void referralEvent(ref string text)
        {
            RaiseTerminalInformationEvent(new string[] { text });
        }

        public void lppCmdFailedEvent(int cmd, int code, ref string text)
        {
            RaiseTerminalInformationEvent(new string[] { cmd.ToString(),  code.ToString(), text});
        }

        public void cardDataEvent(ref string text, ref string cardNo, ref string expDate, ref string track2)
        {
            RaiseTerminalInformationEvent(new string[] { text, cardNo, expDate, track2 });
        }

        public void resultDataEvent(int resultType, int item, ref string description, ref string Value)
        {

            if (item == 20 && TransactionNumber == string.Empty)
            {
                TransactionNumber = Value;
            }
            
            switch ((ResultDataTypes)resultType)
            {
                case ResultDataTypes.rdCUSTOMERRECEIPT:                   
                case ResultDataTypes.rdMERCHANTRECEIPT:
                    receiptData(resultType, item, description, Value);
                    break;
                case ResultDataTypes.rdCLOSEBATCHRESULT:
                case ResultDataTypes.rdCURRENTBATCH:
                    closeBatchData(item, description, Value);
                    break;
                case ResultDataTypes.rdTRMCONFIG:
                case ResultDataTypes.rdTRANSLOGDETAILED:
                case ResultDataTypes.rdTRANSLOGTOTALS:
                case ResultDataTypes.rdUNSENTTRANS:
                    //trmConfig(item, description, value);
                    break;
            }
        
        }

        private void receiptData(int resultType, int item, string text, string data)
        {
            string receiptLine;
            StringBuilder receipt = new StringBuilder();
            
            bool written;
            written = false;

            if (text == "")
                receiptLine = data;  // Default use text as is
            else
            {
                // May need formatting depending on paperwith and so on.
                receiptLine = text + data;
            }

            // Certain items should maybe be bold and may need extra space or linefeeds.
            // Just make sure they come in the order the event occures, the rest is design.
            switch ((ReceiptItems)item)
            {
                case ReceiptItems.riTxnType:
                    receipt.AppendLine(" ");
                    receipt.AppendLine(data);
                    receipt.AppendLine(" ");
                    written = true;
                    break;
                case ReceiptItems.riIdLine:
                    receipt.AppendLine(" ");
                    receipt.AppendLine(data + "........................");
                    receipt.AppendLine(" ");
                    written = true;
                    break;
                case ReceiptItems.riSignatureLine:
                    receipt.AppendLine(" ");
                    receipt.AppendLine(data + "........................");
                    receipt.AppendLine(" ");
                    written = true;
                    break;

                case ReceiptItems.riCashierSignatureText:
                    receipt.AppendLine(" ");
                    receipt.AppendLine("........................");
                    break;
                case ReceiptItems.riCashierNameText:
                    receipt.AppendLine(" ");
                    receipt.AppendLine("........................");
                    break;
            }
            
            if (written == false)
                receipt.AppendLine(receiptLine);


            switch ((ResultDataTypes)resultType)
            {
                case ResultDataTypes.rdMERCHANTRECEIPT:
                    if (item == (int)ReceiptItems.riEnd)
                    {
                        receipt.AppendLine("---- end of receipt ----");
                        API.customerReceipt();
                    }
                    break;

                case ResultDataTypes.rdCUSTOMERRECEIPT:
                    if (item == (int)ReceiptItems.riEnd)
                    {
                        receipt.AppendLine(" ---- end of receipt ----");
                        API.endTransaction();                        
                    }
                    break;
            }

            RaiseTerminalReceiptEvent(new string[] { receipt.ToString() });
        }
        private void closeBatchData(int item, String text, String data)
        {
            String reportRow;

            if (text.Length == 0)
                reportRow = data;  // Default use text as is
            else
            {
                // May need formatting depending on paperwith and so on.
                reportRow = text + data;
            }
            
            switch (item)
            {
                case (int)ResultDataValues.rdEnd:
                    reportRow = "SLUT";        // Not what you think...
                    break;
            }
        }
        public void statusChangeEvent(int newStatus)
        {

            try
            {


                string status = null;
                switch (newStatus)
                {
                    case 0:
                        status = "Terminalen frånkopplad.";
                        //closeButton.Text = "Öppna";
                        //initButton.Text = "Återanslut";
                        break;

                    case 1:
                        status = "Terminal ansluten och klar att användas.";
                        //initButton.Text = "Koppla ifrån";
                        break;

                    case 2:
                        status = "Status öppen.";
                        Opened = true;
                        //closeButton.Text = "Stäng";
                        break;

                    case 3:
                        status = "Status stängd.";
                        Opened = false;
                        //closeButton.Text = "Öppna";
                        TransactionStarted = false;
                        CurrentTransactionType = -1;
                        break;

                    case 4:
                        status = "Transaktion startad.";
                        TransactionStarted = true;
                        CurrentTransactionType = PendingTransactionType;
                        break;

                    case 5:
                        status = "Transaktion avslutad.";
                        TransactionStarted = false;
                        SignatureNeeded = true;
                        CurrentTransactionType = -1;
                        break;

                    case 6:
                        status = "statusChangeEvent med värde=" + new string('0', newStatus);
                        break;

                    case 7:
                        status = "Kommunikation med terminalen bruten. Anropa connect()?";
                        TransactionStarted = false;
                        CurrentTransactionType = -1;
                        Opened = false;
                        //closeButton.Text = "Öppna";
                        //initButton.Text = "Återanslut";
                        break;

                }


                RaiseTerminalInformationEvent(new string[] {status});


                if (newStatus == 5)
                {
                    RaiseTerminalTransactionEndedEvent();

                    if (SignatureNeeded)
                    {
                        RaiseTerminalSignatureNeededEvent();
                    }
                }

            
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                SignatureNeeded = false;
            }
        }

        public void paymentCodeEvent(ref string text)
        {
            RaiseTerminalInformationEvent(new string[] { text });
        }
        #endregion

        #region Interface

        void RaiseTerminalDisplayEvent(string[] items)
        {
            EventHandler terminalEventhandler = TerminalDisplayEvent;

            DisplayEventArgs arg = new DisplayEventArgs();

            foreach (var iten in items)
            {
                arg.Items.Add(iten);
            }

            if (terminalEventhandler != null)
            {
                terminalEventhandler(this, arg);
            }
        }

        void RaiseTerminalReceiptEvent(string[] items)
        {
            EventHandler receiptEventhandler = TerminalReceiptEvent;
            DisplayEventArgs arg = new DisplayEventArgs();
            foreach (var iten in items)
            {
                arg.Items.Add(iten);
            }

            if (receiptEventhandler != null)
            {
                receiptEventhandler(this, arg);
            }
        }

        void RaiseTerminalInformationEvent(string[] items )
        {

            EventHandler displayEventhandler = TerminalInformationEvent;

            DisplayEventArgs arg = new DisplayEventArgs();
            
            foreach (var iten in items)
            {
                arg.Items.Add(iten);
            }
            
            if (displayEventhandler != null)
            {
                displayEventhandler(this, arg);
            }
        }

        void RaiseTerminalExceptionEvent(string[] items)
        {
            EventHandler exceptionEventhandler = TerminalExceptionEvent;

            DisplayEventArgs arg = new DisplayEventArgs();

            foreach (var iten in items)
            {
                arg.Items.Add(iten);
            }

            if (exceptionEventhandler != null)
            {
                exceptionEventhandler(this, arg);
            }
        }

        void RaiseTerminalTransactionEndedEvent()
        {
            EventHandler terminalTransactionEndedEvent = TerminalTransactionEndedEvent;
            TransactionEventArgs tranArg = new TransactionEventArgs();
            tranArg.TransactionNumber = TransactionNumber;
            tranArg.TransactionAccepted = TransactionAccepted;

            if (terminalTransactionEndedEvent != null)
            {
                terminalTransactionEndedEvent(this, tranArg);
            }
        }

        void RaiseTerminalSignatureNeededEvent()
        {
            EventHandler signatureNeededEventhandler = TerminalSignatureNeededEvent;
            
            if (signatureNeededEventhandler != null)
            {
                signatureNeededEventhandler(this, null);
            }
        }




        public event EventHandler TerminalDisplayEvent;
        public event EventHandler TerminalReceiptEvent;
        public event EventHandler TerminalInformationEvent;
        public event EventHandler TerminalExceptionEvent;
        public event EventHandler TerminalTransactionEndedEvent;
        public event EventHandler TerminalSignatureNeededEvent;



        public void Anslut(string terminalAdr, int? terminalPort, int? comPort)
        {
            try
            {
                if (!ConnectionInitiated)
                {

                    if (terminalAdr != null && terminalPort.HasValue)
                    {
                        API.initLan(terminalAdr, terminalPort.Value);
                    }
                    else if (comPort.HasValue)
                    {
                        API.initRs232(comPort.Value, "");
                    }
                }
                else
                {
                    API.connect();
                }

                ConnectionInitiated = true;
            }
            catch (Exception)
            {

                ConnectionInitiated = false;
            }
        }

        public void Betala(int belopp, int moms, int tillbaka)
        {
            TransactionNumber = string.Empty;
            API.start((int)TransactionTypes.LPP_PURCHASE);
            PendingTransactionType = (int)TransactionTypes.LPP_PURCHASE;
            API.sendAmounts(belopp, moms, tillbaka);
        }

        public void Återbetala(int belopp, int moms, int tillbaka)
        {
            TransactionNumber = string.Empty;
            API.start((int)TransactionTypes.LPP_REFUND);
            PendingTransactionType = (int)TransactionTypes.LPP_PURCHASE;
            API.sendAmounts(belopp, moms, tillbaka);
        }

        public void Avbryt()
        {
            TransactionNumber = string.Empty;
            API.cancel();
        }

        public void StängTerminal()
        {
            
            if (PendingTransactionType != 0)
            {
                TransactionNumber = string.Empty;
                API.end();
            }
        }

        public void Avsluta()
        {
            if (TransactionStarted)
            {
                API.endTransaction();
                TransactionNumber = string.Empty;
            }
        }

        #endregion

        #region Helpers
        public void Open()
        {
            if (Opened)
            {
                API.close();
            }
        }

        public void Close()
        {
            API.close();
        }
        #endregion
    }
}
