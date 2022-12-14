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
        Console.WriteLine();
        Console.WriteLine(Titolo);
        StampaListaEventi(Eventi);
    }

    public static void ImportaSiNo()
    {
        if (MyUtilities.SiNo("Vuoi importare il programma precedente?"))
        {
            ImportaProgramma();
        } else
        {
            CreaProgramma();
        }
    }

    public static void CreaProgramma()
    {
        Console.Clear();
        string nomeProgramma = null;
        ProgrammaEventi programma = null;
        while (programma == null)
        {
            try
            {
                nomeProgramma = MyUtilities.Chiedi("Inserisci il nome del Programma eventi:");
                programma = new ProgrammaEventi(nomeProgramma);
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MyUtilities.Continua();
            }
        }
        new ProgrammaEventi(nomeProgramma);
        int numeroEventi = MyUtilities.ChiediInt("Quanti eventi vuoi aggiungere al tuo programma?");
        while (programma.Eventi.Count < numeroEventi)
        {
            Console.Clear();
            Console.WriteLine("Inserimento eventi per il programma: {0}", programma.Titolo);
            if (programma.Eventi.Count > 0){
                Console.WriteLine("Eventi già inseriti:");

                ProgrammaEventi.StampaListaEventi(programma.Eventi);
            }
            
            try
            {
                Console.WriteLine();
                Console.WriteLine("Inserimento evento {0} / {1}", programma.Eventi.Count() + 1, numeroEventi);
                
                bool eConferenza = MyUtilities.SiNo("Questo evento è una conferenza?");
                if (eConferenza)
                {
                    Conferenza conferenza =  Conferenza.CreaConferenza();
                    programma.AggiungiEvento((Evento)conferenza);
                } else
                {
                    Evento evento = Evento.ChiediDatiEvento();
                    programma.AggiungiEvento(evento);
                }
                
            } catch (Exception e){
                Console.WriteLine(e.Message);
                MyUtilities.Continua();
            }
        }

        Console.WriteLine("Il numero dei eventi in programma è: {0}", programma.Eventi.Count());
        programma.StampaProgramma();

        programma.StampaEventiPerData();

        Console.WriteLine();
        if (MyUtilities.SiNo("Vuoi esportare il programma?"))
        {
            programma.EsportaProgramma();
        }

        MyUtilities.Continua();

    }

    public void StampaEventiPerData()
    {
        DateTime data = MyUtilities.CreaData("Inserire una data per sapere che eventi ci saranno (dd/mm/yyyy)");
        ProgrammaEventi.StampaListaEventi(EventiInData(data));
    }

    public void EsportaProgramma()
    {
        StreamWriter stream = File.CreateText("C:\\Users\\andre\\source\\experis4\\csharp-gestore-eventi\\programma.csv");
        stream.WriteLine("titolo,data,capienza massima,posti prenotati,relatore,prezzo");

        foreach (Evento evento in Eventi)
        {
            stream.WriteLine(evento.ToExport());

        }

        stream.Close();
    }

    public static void ImportaProgramma()
    {
        ProgrammaEventi programma = new ProgrammaEventi(MyUtilities.Chiedi("Inserire il nome del programma"));
        StreamReader stream = File.OpenText("C:\\Users\\andre\\source\\experis4\\csharp-gestore-eventi\\programma.csv");
        stream.ReadLine();

        while (!stream.EndOfStream)
        {
            string[] values = stream.ReadLine().Split(',');
            string titolo = values[0];
            string data = values[1];
            int day = Convert.ToInt32(data.Substring(0, 2));
            int month = Convert.ToInt32(data.Substring(3, 2));
            int year = Convert.ToInt32(data.Substring(6, 4));
            DateTime nuovaData = new DateTime(year, month, day);
            int maxPosti = Convert.ToInt32(values[2]);
            if (values[3].Equals("-") && values[4].Equals("-"))
            {
                Evento evento = new Evento(titolo, nuovaData, maxPosti);
                programma.AggiungiEvento(evento);
            } else
            {
                string relatore = values[3];
                double prezzo = Convert.ToDouble(values[4]);

                Conferenza conferenza = new Conferenza(titolo, nuovaData, maxPosti, relatore, prezzo);
                programma.AggiungiEvento(conferenza);
            }
            
   
        }

        stream.Close();

        programma.StampaProgramma();
        MyUtilities.Continua();
    }
}

