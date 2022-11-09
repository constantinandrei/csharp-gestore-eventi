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
        }
    }
    public int PostiPrenotati { get; }
    public Evento(string titolo, DateTime data, int maxPosti)
    {
        Titolo = titolo;
        Data = data;
        MaxPosti = maxPosti;
        PostiPrenotati = 0;
    }
}




