﻿using csharp_gestore_eventi.Exceptions;


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

    
}

