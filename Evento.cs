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
}




