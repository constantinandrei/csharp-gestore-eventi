using csharp_gestore_eventi.Exceptions;

public class Evento
{
    public string Titolo { get; set; }
    private DateTime _data;
    public DateTime Data { get
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
    public int MaxPosti { get; }
    public int PostiPrenotati { get; }
}




