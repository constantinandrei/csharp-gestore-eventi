
using csharp_gestore_eventi.Exceptions;

Evento evento = new Evento("Evento", new DateTime(2022, 11, 21, 21, 00, 00), 9);
evento.PrenotaPosti(7);
evento.DisdiciPosti(3);
Console.WriteLine(evento.PostiPrenotati);
Console.WriteLine(evento.ToString());



