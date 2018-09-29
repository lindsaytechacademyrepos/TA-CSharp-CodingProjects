using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blackjack_game
{
    public class Deck
    {

        public List<Card> Cards { get; set; }

        public Deck() //THIS IS A CONSTRUCTOR
        {

            Cards = new List<Card>();

            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Card card = new Card();
                    card.Face = (Face)i;
                    card.Suit = (Suit)j;
                    Cards.Add(card);

                }


            }


        }

        public /*static*/ /*Deck*/void Shuffle(/*Deck deck,*/ /*out int timesShuffled,*/ int times = 1)
        {

            //timesShuffled = 0;
            for (int i = 0; i < times; i++)
            {
                //timesShuffled++;
                List<Card> TempList = new List<Card>();
                Random random = new Random();

                while (Cards.Count > 0)
                {
                    int randomIndex = random.Next(0, Cards.Count);
                    TempList.Add(Cards[randomIndex]);
                    Cards.RemoveAt(randomIndex);
                }
                Cards = TempList;

            }

            return;


        }



    }
}
