using System;

namespace Momentum.Ekonomi.Payments.Terminal
{
    public interface ITermimal
    {



        event EventHandler TerminalDisplayEvent;
        event EventHandler TerminalReceiptEvent;
        event EventHandler TerminalInformationEvent;
        event EventHandler TerminalExceptionEvent;
        event EventHandler TerminalTransactionEndedEvent;
        event EventHandler TerminalSignatureNeededEvent;


        void Anslut(string terminalAdr, int? terminalPort, int? comPort);
        void Betala(int belopp, int moms, int tillbaka);        
        void Återbetala(int belopp, int moms, int tillbaka);
        void Avsluta();
        void Avbryt();
        void StängTerminal();
    }
}
