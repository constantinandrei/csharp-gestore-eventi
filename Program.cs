
using csharp_gestore_eventi.Exceptions;
string userChoise = "start";

while (!userChoise.Equals("esc"))
{
    Console.Clear();
    Console.WriteLine("1. Inserisci un evento");
    Console.WriteLine("2. Inserisci un programma di eventi");
    userChoise = MyUtilities.Chiedi("Scirvi 'esc' e premi Invio per uscire");

    switch (userChoise)
    {
        case "1":
            Evento.CreaEvento();
            break;
        case "2":
            ProgrammaEventi.CreaProgramma();
            break;
        default:
            if (!userChoise.Equals("esc"))
            {
                Console.WriteLine("Scelta non valida");
                MyUtilities.Continua();
            }
            break;
    }
}


