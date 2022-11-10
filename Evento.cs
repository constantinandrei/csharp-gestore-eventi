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
            if (value.Date < DateTime.Now.Date)
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
    public bool PrenotaPosti(int posti)
    {
        if (DateTime.Now > Data)
            throw new GestoreEventiException("Questo evento è già terminato, non è più possibile prenotare posti");
        if (PostiDisponibili() < posti)
            throw new GestoreEventiException("Non ci sono abbastanza posti diponibili per questo evento");
        PostiPrenotati += posti;
        return true;
    }

    public bool DisdiciPosti(int posti)
    {
        if (posti < 0)
            throw new GestoreEventiException("Il numero di posti da disdire non può essere negativo");
        if (DateTime.Now > Data)
               throw new GestoreEventiException("Questo evento è già terminato, non è più possibile disdire le prenotazioni");
        if (PostiPrenotati < posti)
            throw new GestoreEventiException("Non è possibilie disdire più posti di quelli prenotati");
        
        PostiPrenotati -= posti;
        return true;
    }

    public override string ToString()
    {
        return Data.ToString("dd/MM/yyyy") + " - " + Titolo;
    }

    public static Evento ChiediDatiEvento()
    {
        Evento evento = null;
        while (evento == null)
        {
            try
            {
                string titolo = MyUtilities.Chiedi("Inserisci il nome dell'evento:");
                DateTime dataEvento = MyUtilities.CreaData("Inserisci la data dell'evento (gg/mm/yyyy):");
                int postiTotali = MyUtilities.ChiediInt("Inserisci il numero di posti totali:");
                evento = new Evento(titolo, dataEvento, postiTotali);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        Console.WriteLine("Evento Inserito correttamente");
        Console.WriteLine(evento.ToString());
        Console.WriteLine();
        MyUtilities.Continua();
        return evento;
    }

    public static Evento GestionePostiEvento(Evento evento)
    {
        if (ChiediPrenotaDisdici("prenotare"))
        {
            
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

        return evento;
    }

    public static Evento CreaEvento()
    {
        
        Evento evento = ChiediDatiEvento();
        return GestionePostiEvento(evento);
        
    }

    // chiede all'utente se vuole disdire/prenotare e ritorna la risposta in booleano
    public static bool ChiediPrenotaDisdici(string arg)
    {
        string risp = MyUtilities.Chiedi("Vuoi " + arg + " dei posti?(si/no)");

        while (!risp.Equals("si") && !risp.Equals("no"))
        {
            Console.WriteLine("Inserire si oppure no");
            risp = MyUtilities.Chiedi("Vuoi " + arg + " dei posti?(si/no)");
        }
        if (risp.Equals("si"))
            return true;
        return false;
    }

    // chiede all'utente quanti posti vuole disdire/prenotare e poi stampa i posti
    public static void PrenotaDisdici(string arg, Evento evento)
    {
        bool operazioneEffettuata = false;
        while (!operazioneEffettuata)
        {
            try
            {
                int risp = MyUtilities.ChiediInt("Quanti posti vuoi " + arg + "?");
                if (arg.Equals("prenotare"))
                {
                   operazioneEffettuata = evento.PrenotaPosti(risp);
                }

                if (arg.Equals("disdire"))
                {
                   operazioneEffettuata = evento.DisdiciPosti(risp);
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
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




