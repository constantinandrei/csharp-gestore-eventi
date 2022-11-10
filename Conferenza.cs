using csharp_gestore_eventi.Exceptions;
public class Conferenza : Evento
{
    string _relatore;
    public string Relatore
    {
        get
        {
            return _relatore;
        }
        set
        {
            if (value == "" || value == null)
            {
                throw new GestoreEventiException("Il nome del relatore non può essere lasciato vuoto");
            }

            _relatore = value; 
        }
    }

    double _prezzo;
    public double Prezzo
    {
        get
        {
            return _prezzo;
        }
        set
        {
            if (value < 0)
                throw new GestoreEventiException("Il prezzo non può essere negativo");
            _prezzo = value;
        }
    }
    public Conferenza(string titolo, DateTime data, int maxPosti, string relatore, double prezzo) : base(titolo, data, maxPosti)
    {
        Relatore = relatore;
        Prezzo = prezzo;
    }

    public string PrezzoFormat()
    {
        return Prezzo.ToString("0.00");
    }

    public override string ToString()
    {
        return base.ToString() + " - " + Relatore + " - " + PrezzoFormat() + " euro";
    }

    public static Conferenza CreaConferenza()
    {
        Evento evento = Evento.ChiediDatiEvento();
        Conferenza conferenza = null;
        while (conferenza == null)
        {
            try
            {
                string relatore = MyUtilities.Chiedi("Inserisci il nome del relatore");
                double prezzo = MyUtilities.ChiediDouble("Inserisci il prezzo del biglietto");

                conferenza = new Conferenza(evento.Titolo, evento.Data, evento.MaxPosti, relatore, prezzo);
            } 
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        return conferenza;
    }


}
