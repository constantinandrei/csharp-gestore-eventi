public static class MyUtilities
{
   public static string Chiedi(string message)
    {
        Console.Write("{0} ", message);
        return Console.ReadLine();
    }

   public static int ChiediInt(string message)
    {
        Console.Write("{0} ", message);
        return Convert.ToInt32(Console.ReadLine());
    }

    public static double ChiediDouble(string message)
    {
        double nr = 0.0;
        while (nr == 0)
        {
            try
            {
                nr = Convert.ToDouble(MyUtilities.Chiedi(message));
            } catch (Exception e)
            {
                Console.WriteLine("Non è stato inserito un prezzo valido");
            }
        }

        return nr;
    }

   public static DateTime CreaData(string messaggio)
    {
        DateTime? nuovaData = null;
        while (nuovaData == null)
        {
            try
            {
                string data = MyUtilities.Chiedi(messaggio);
                int day = Convert.ToInt32(data.Substring(0, 2));
                int month = Convert.ToInt32(data.Substring(3, 2));
                int year = Convert.ToInt32(data.Substring(6, 4));
                nuovaData = new DateTime(year, month, day);
            } catch (Exception e) {
                Console.WriteLine("Data non inserita in un formato valido");
            }
        }
        
        return (DateTime)nuovaData;
    }

    public static void Continua()
    {
        Console.WriteLine("Premi Invio per Continuare");
        Console.ReadLine();
    }

    public static bool SiNo(string message)
    {
        bool invalidAnswer = true;
        string userAnswer = null;
        while (invalidAnswer)
        {
            userAnswer = Chiedi(message);
            if (userAnswer.Equals("si") || userAnswer.Equals("no"))
            {
                invalidAnswer = false;
                if (userAnswer.Equals("si"))
                {
                    return true;
                }
                    
                if (userAnswer.Equals("no"))
                {
                    return false;
                }
                    
            } 
            
             Console.WriteLine("Risposta non valida. Sceliere 'si' oppure 'no");
                 
        }

        return invalidAnswer;

    }
}