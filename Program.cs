using System;
using System.Collections.Generic;

public class Program
{
    static void Main()
    {
        var deck = new Deck();
        var playerHand = new Hand();
        var dealerHand = new Hand();

        playerHand.AddCard(deck.DrawCard());
        playerHand.AddCard(deck.DrawCard());

        dealerHand.AddCard(deck.DrawCard());

        while (true)
        {
            Console.WriteLine("Your hand: " + playerHand);
            Console.WriteLine("Dealer's hand: " + dealerHand);

            if (playerHand.Value > 21)
            {
                Console.WriteLine("You busted! Dealer wins.");
                break;
            }

            Console.Write("Do you want to hit or stand? ");
            var input = Console.ReadLine().ToLower();

            if (input == "hit")
            {
                playerHand.AddCard(deck.DrawCard());
            }
            else if (input == "stand")
            {
                while (dealerHand.Value < 17)
                {
                    dealerHand.AddCard(deck.DrawCard());
                }

                Console.WriteLine("Dealer's final hand: " + dealerHand);

                if (dealerHand.Value > 21)
                {
                    Console.WriteLine("Dealer busted! You win!");
                }
                else if (dealerHand.Value > playerHand.Value)
                {
                    Console.WriteLine("Dealer wins!");
                }
                else if (dealerHand.Value < playerHand.Value)
                {
                    Console.WriteLine("You win!");
                }
                else
                {
                    Console.WriteLine("It's a tie!");
                }

                break;
            }
        }
    }
}

public class Card
{
    public int Value { get; set; }
    public string Name { get; set; }

    public override string ToString()
    {
        return Name;
    }
}

public class Deck
{
    private List<Card> cards;

    public Deck()
    {
        cards = new List<Card>();

        for (int i = 2; i <= 11; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                var card = new Card { Value = i };

                if (i == 11)
                {
                    card.Name = "Ace";
                    card.Value = 11;
                }
                else if (i == 10)
                {
                    card.Name = j == 0 ? "10" : j == 1 ? "Jack" : j == 2 ? "Queen" : "King";
                }
                else
                {
                    card.Name = i.ToString();
                }

                cards.Add(card);
            }
        }

        var random = new Random();
        for (int i = 0; i < cards.Count; i++)
        {
            var temp = cards[i];
            var index = random.Next(cards.Count);
            cards[i] = cards[index];
            cards[index] = temp;
        }
    }

    public Card DrawCard()
    {
        var card = cards[0];
        cards.RemoveAt(0);
        return card;
    }
}

public class Hand
{
    private List<Card> cards = new List<Card>();

    public int Value
    {
        get
        {
            var value = 0;
            var aces = 0;

            foreach (var card in cards)
            {
                value += card.Value;
                if (card.Name == "Ace")
                {
                    aces++;
                }
            }

            while (value > 21 && aces > 0)
            {
                value -= 10;
                aces--;
            }

            return value;
        }
    }

    public void AddCard(Card card)
    {
        cards.Add(card);
    }

    public override string ToString()
    {
        return string.Join(", ", cards) + " (" + Value + ")";
    }
}
