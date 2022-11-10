using csharp_gestore_eventi.Exceptions;

public class Evento
{
    private string _titolo;
    public string Titolo
    {

        get
        {
            return _titolo;
        }
        set
        {
            if (value == null || value.Equals(""))
            {
                throw new GestoreEventiException("Il nome del evento non può essere vuoto");
            }
            _titolo = value;
        }
    }
    private DateTime _data;
    public DateTime Data
    {
        get
        {
            return _data;
        }
        set
        {
            if (DateTime.Now > value)
                throw new GestoreEventiException("Non è possibile inserire un evento in una data passata");
            _data = value;
        }
    }
    private int _maxPosti;

    

    public int MaxPosti
    {
        get
        {
            return _maxPosti;
        }
        private set
        {
            if (value <= 0)
            {
                throw new GestoreEventiException("Il numero di posti disponibili deve essere almeno 1");
            }
            _maxPosti = value;
        }
    }
    public int PostiPrenotati { get; private set; }
    public Evento(string titolo, DateTime data, int maxPosti)
    {
        Titolo = titolo;
        Data = data;
        MaxPosti = maxPosti;
        PostiPrenotati = 0;
    }

    public int PostiDisponibili()
    {
        return MaxPosti - PostiPrenotati;
    }
    public void PrenotaPosti(int posti)
    {
        if (DateTime.Now > Data)
            throw new GestoreEventiException("Questo evento è già terminato, non è più possibile prenotare posti");
        if (PostiDisponibili() < posti)
            throw new GestoreEventiException("Non ci sono abbastanza posti diponibili per questo evento");
        PostiPrenotati += posti;
    }

    public void DisdiciPosti(int posti)
    {
        if (posti < 1)
            throw new GestoreEventiException("Il numero di posti deve essere almeno 1");
        if (DateTime.Now > Data)
               throw new GestoreEventiException("Questo evento è già terminato, non è più possibile disdire le prenotazioni");
        if (PostiPrenotati < posti)
            throw new GestoreEventiException("Non è possibilie disdire più posti di quelli prenotati");
        
        PostiPrenotati -= posti;
    }

    public override string ToString()
    {
        return Data.ToString("dd/MM/yyyy") + " - " + Titolo;
    }

    public static Evento CreaEvento()
    {
        string titolo = MyUtilities.Chiedi("Inserisci il nome dell'evento:");
        DateTime dataEvento = MyUtilities.CreaData("Inserisci la data dell'evento (gg/mm/yyyy):");
        int postiTotali = MyUtilities.ChiediInt("Inserisci il numero di posti totali:");

        Evento evento = new Evento(titolo, dataEvento, postiTotali);
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
            Console.WriteLine("Evento Inserito correttamente");
            Console.WriteLine(evento.ToString());
            Console.WriteLine();
            MyUtilities.Continua();
            return evento;
        }
        // se risponde no allora non faccio vedere la schermata della disdetta dei posti
        Console.WriteLine("Evento Inserito correttamente");
        Console.WriteLine(evento.ToString());
        Console.WriteLine();
        MyUtilities.Continua();
        return evento;
    }

    // chiede all'utente se vuole disdire/prenotare e ritorna la risposta in booleano
    public static bool ChiediPrenotaDisdici(string arg)
    {
        string risp = MyUtilities.Chiedi("Vuoi " + arg + " dei posti?(si/no)");
        if (risp.Equals("si"))
            return true;
        return false;
    }

    // chiede all'utente quanti posti vuole disdire/prenotare e poi stampa i posti
    public static void PrenotaDisdici(string arg, Evento evento)
    {
        int risp = MyUtilities.ChiediInt("Quanti posti vuoi " + arg + "?");
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
    public static void StampaPosti(Evento evento)
    {
        Console.WriteLine("Numero di posti prenotati = {0}", evento.PostiPrenotati);
        Console.WriteLine("Numero di posti disponibili = {0}", evento.PostiDisponibili());
    }
}




