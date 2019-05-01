using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace Laboratory1
{
    class Program 
	{
        static void Main(string[] args) {
			SudokuState theState = new SudokuState("800030000930007000071520900005010620000050000046080300009076850060100032000040006"); 
            SudokuSearch theSearcher = new SudokuSearch(theState); // tworzymy szukacz na podstawie pierwszego stanu
            theSearcher.DoSearch(); // szukacz wykonuje algorytm A*  znajduje droge od pierwszego stanu do rozwiazania
			List<SudokuState> states = new List<SudokuState>(); // tworzymy liste stanow
            SudokuState theResult = (SudokuState)theSearcher.Solutions[0]; // do zmiennej theResult przypisujemy stan z rozwiazaniem znalezionym przez szukacz
            while (theResult != null) // do kolejnych elementow listy states wpisujemy kolejne stany po drodze od stanu poczatkowego do rozwiazania
            {
                states.Add(theResult);
                theResult = (SudokuState)theResult.Parent;
            }
			for (int i = states.Count()-1 ; i > 0; --i) // od tylu wypisujemy stany; od poczatku do rozwiazania
            {
				states.ElementAt (i).Write ();
				Console.WriteLine ("Jeszcze " + i + " stanow do rozwiazania");
				Thread.Sleep (300);
                Console.Clear(); // czyscimy konsole
                
            }
			states.ElementAt (0).Write (); // wypisujemy ostateczne rozwiazanie na sam koniec
        }
    }
}
