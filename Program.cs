
using csharp_gestore_eventi.Exceptions;

Evento evento = CreaEvento();

Console.WriteLine(evento.ToString());

Evento CreaEvento()
{
    string titolo = Chiedi("Inserisci il nome dell'evento:");
    string dataStringa = Chiedi("Inserisci la data dell'evento (gg/mm/yyyy):");
    DateTime dataEvento = CreaData(dataStringa);
    int postiTotali = ChiediInt("Inserisci il numero di posti totali:");

    return new Evento(titolo, dataEvento, postiTotali);

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