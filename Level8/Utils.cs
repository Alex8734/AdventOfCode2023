namespace Level8;

public class Utils
{
           /// <summary>
        /// Berechnet das kleinste gemeinsamme Vielfache von beliebig vielen Zahlen.
        /// </summary>
        /// <param name="numbers">Die zu verarbeitenden Zahlen.</param>
        /// <returns>Das kleinste gemeinsame Vielfache der Zahlen.</returns>
        public static ulong GetSmallestCommonMultiple(params long[] numbers)
        {
            if (numbers.Contains(0))
                return 0;

            ulong[][] lists = (from x in numbers
                               where x != 1 // 1 hat keine Auswirkungen
                               select GetPrimeFactors((ulong)(x < 0 ? x * -1 : x))).ToArray();//Alle Primfaktoren ermitteln
            Dictionary<ulong, uint> list = new Dictionary<ulong, uint>();

            foreach (ulong[] l in lists)//Alle Primfaktoren-Listen durchgehen
            {
                uint n = 1;
                ulong cur = 0;
                if (l.Length == 0)
                    continue;// Zahl ist eine 1
                foreach (ulong i in l)//Alle Primfaktoren durchgehen
                {
                    if (cur == i)
                        ++n;
                    else
                    {
                        if (cur != 0)
                        {
                            if (list.ContainsKey(cur))//Enthält die Liste bereits die Zahl?
                            {
                                if (list[cur] <= n)//Zahl bereits enthalten und bisherige Anzahl kleiner als die aktuelle
                                    list[cur] = n;//Neue Zuweisung der Anzahl.
                            }
                            else
                            {
                                list.Add(cur, n);//Neue Zahl zur Liste hinzufügen.
                            }
                        }
                        n = 1;
                        cur = i;
                    }
                }
                if (list.ContainsKey(cur))//Enthält die Liste bereits die Zahl?
                {
                    if (list[cur] <= n)//Zahl bereits enthalten und bisherige Anzahl kleiner als die aktuelle
                        list[cur] = n;//Neue Zuweisung der Anzahl.
                }
                else
                {
                    list.Add(cur, n);//Neue Zahl zur Liste hinzufügen.
                }
            }
            ulong result = 1;
            foreach (KeyValuePair<ulong, uint> l in list)//Alle Werte durchgehen
                result *= Pow(l.Key, l.Value);//Potenzieren und multiplizieren der Werte
            return result;//Wert zurück geben
        }

        /// <summary>
        /// Potenziert einen Wert mit dem angegebenen Exponenten.
        /// </summary>
        /// <param name="b">Die Basis.</param>
        /// <param name="e">Der Exponent.</param>
        /// <returns>Die Potenz der Basis <c>b</c> und dem Exponenten <c>e</c>.</returns>
        public static ulong Pow(ulong b, uint e)
        {
            ulong result = 1;
            for (uint i = 0; i < e; ++i)
                result *= b;
            return result;
        }

        /// <summary>
        /// Gibt die Primfaktoren einer Zahl zurück.
        /// </summary>
        /// <param name="x">Die Zahl, die in ihre Primfaktoren zerlegt werden soll.</param>
        /// <returns>Die Primfaktoren der Zahl</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Wird ausgelöst, wenn x kleiner gleich 1 ist.</exception>
        public static ulong[] GetPrimeFactors(ulong x)
        {
            if (x <= 1)
                throw new ArgumentOutOfRangeException("x", "x >= 2");
            List<ulong> list = new List<ulong>();

            //? 2 entfernen/auflisten
            while ((double)x / (double)2 == (ulong)((double)x / (double)2))//2 entfernen
            {
                list.Add(2);
                x /= 2;
            }

            //? Ungerade Zahlen auf Primzahlen prüfen und ggf. entfernen/auflisten
            for (ulong i = 3; i <= x && x != 1; )
            {
                if ((double)x / (double)i == (ulong)((double)x / (double)i)) // ist Zahl durch i Teilbar?
                {
                    list.Add(i); // Zur Liste hinzufügen
                    x /= i; // Faktor von Zahl entfernen
                }
                else
                {
                    i += 2; // Nächste ungerade Zahl
                }
            }
            return list.ToArray(); // Als Array zurück geben
        }
}