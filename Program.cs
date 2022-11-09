
using csharp_gestore_eventi.Exceptions;

Evento evento = CreaEvento();

Console.WriteLine(evento.ToString());

// chiede all'utente se vuole disdire/prenotare e ritorna la risposta in booleano
bool ChiediPrenotaDisdici(string arg)
{
    string risp = Chiedi("Vuoi " + arg + " dei posti?(si/no)");
    if (risp.Equals("si"))
        return true;
    return false;
}

// chiede all'utente quanti posti vuole disdire/prenotare e poi stampa i posti
void PrenotaDisdici(string arg, Evento evento)
{
    int risp = ChiediInt("Quanti posti vuoi " + arg + "?");
    if (arg.Equals("prenotare"))
    {
        evento.PrenotaPosti(risp);
    }

    if (arg.Equals("disdire"))
    {
        evento.DisdiciPosti(risp);
    }

    StampaPosti(evento);
}

// stampa lo stato dei posti di un evento
void StampaPosti(Evento evento)
{
    Console.WriteLine("Numero di posti prenotati = {0}", evento.PostiPrenotati);
    Console.WriteLine("Numero di posti disponibili = {0}", evento.PostiDisponibili());
}

Evento CreaEvento()
{
    string titolo = Chiedi("Inserisci il nome dell'evento:");
    string dataStringa = Chiedi("Inserisci la data dell'evento (gg/mm/yyyy):");
    DateTime dataEvento = CreaData(dataStringa);
    int postiTotali = ChiediInt("Inserisci il numero di posti totali:");

    evento =  new Evento(titolo, dataEvento, postiTotali);
    // chiedo all'utente se vuole prenotare dei posti 

    if (ChiediPrenotaDisdici("prenotare"))
    {
        // chiedo se vuole disdire dei posti e poi sottrago fino a quando non risponde no
        PrenotaDisdici("prenotare", evento);
        bool disdire = true;
        while (disdire)
        {
            disdire = ChiediPrenotaDisdici("disdire");
            if (disdire)
            {
                PrenotaDisdici("disdire", evento);
            }
        }
        return evento;
    }
    // se risponde no allora non faccio vedere la schermata della disdetta dei posti
    return evento;
    
    
}

string Chiedi(string message)
{
    Console.Write("{0} ", message);
    return Console.ReadLine();
}

int ChiediInt(string message)
{
    Console.Write("{0} ", message);
    return Convert.ToInt32(Console.ReadLine());
}

DateTime CreaData(string data)
{
    int day = Convert.ToInt32(data.Substring(0, 2));
    int month = Convert.ToInt32(data.Substring(3, 2));
    int year = Convert.ToInt32(data.Substring(6, 4));

    return new DateTime(year, month, day);
}