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

   public static DateTime CreaData(string data)
    {
        int day = Convert.ToInt32(data.Substring(0, 2));
        int month = Convert.ToInt32(data.Substring(3, 2));
        int year = Convert.ToInt32(data.Substring(6, 4));

        return new DateTime(year, month, day);
    }

    public static void Continua()
    {
        Console.WriteLine("Premi Invio per Continuare");
        Console.ReadLine();
    }
}