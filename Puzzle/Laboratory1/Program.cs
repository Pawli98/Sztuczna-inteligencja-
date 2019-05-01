using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Laboratory1 
{
    class Program 
	{
        static void Main(string[] args) 
		{
			PuzzleState theState = new PuzzleState("1234567890"); // tworzymy puzzle w stanie rozwiazanym
            PuzzleSearch theSearcher = new PuzzleSearch(theState); // tworzymy szukacz
			theState.Shuffle(); // mieszamy puzzle
            //theState.Write();
            theSearcher.DoSearch(); // wykonujemy szukanie drogi do rozwiazania
            PuzzleState theResult = (PuzzleState)theSearcher.Solutions[0]; // przypisujemy stan rozwiazany od szukacza
            List<PuzzleState> states = new List<PuzzleState>(); // tworzymy liste kolejnych stanow
            while (theResult != null)
            {
                states.Add(theResult); // przypisujemy kolejne stany od zmieszania az do rozwiazania
                theResult = (PuzzleState)theResult.Parent;
            }

           int k = states.Count();
                for (int i = states.Count()-1 ; i >= 0; --i)
            {
                
                Console.Clear();
                Console.WriteLine("Manhattan stany to:" +k);
                Console.WriteLine ("Pozostalo " + i + " stanow do rozwiazania"); // wypisujemy kolejne stany na konsoli
                states[i].Write();
				Thread.Sleep (200);
            }
        }
    }
}
