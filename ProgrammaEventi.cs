using csharp_gestore_eventi.Exceptions;


public class ProgrammaEventi
{
    private string _titolo;
    public List<Evento> Eventi { get; }

    public ProgrammaEventi(string titolo)
    {
        Titolo = titolo;
        Eventi = new List<Evento>();
    }

    public string Titolo
    {
        get
        {
            return _titolo;
        }
        set
        {
            if (value.Equals("") || value == null)
                throw new GestoreEventiException("Il titolo del programma non può essere lasciato vuoto");
            _titolo = value;
        }
    }

    public void AggiungiEvento(Evento evento)
    {
        Eventi.Add(evento);
    }

    public List<Evento> EventiInData(DateTime data)
    {
        List<Evento> list = new List<Evento>();
        foreach (Evento evento in Eventi)
        {
            if (data.Date == evento.Data.Date)
            {
                list.Add(evento);
            }
        }
        return list;
    }

    public static void StampaListaEventi(List<Evento> eventi)
    {
        foreach (Evento evento in eventi)
        {
            Console.WriteLine(evento.ToString());
        }
    }

    public int NumeroEventi()
    {
        return Eventi.Count;
    }

    public void StampaProgramma()
    {
        Console.WriteLine(Titolo);
        StampaListaEventi(Eventi);
    }
}

