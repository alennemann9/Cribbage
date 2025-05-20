namespace LAL
{
    class Game
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            StartUp();
            int round = FirstCrib();
            String cribholder;
            Human player = new Human();
            Computer computer = new Computer();
            while (player.score < 121 && computer.score < 121)
            {
                if (round % 2 == 1)
                {
                    cribholder = "your";
                }
                else
                {
                    cribholder = "the computer's";
                }

                Deck deck = new Deck();
                player.NewHand(deck);
                computer.NewHand(deck);
                Console.Write("Your hand is: ");
                Console.WriteLine(string.Join(" ", player.hand));
                Console.WriteLine($"It is {cribholder} crib");
                List<Card> crib = new List<Card>();
                crib.AddRange(player.Crib());
                crib.AddRange(computer.Crib());
                List<Card> playerFullHand = new List<Card>(player.hand);
                List<Card> computerFullHand = new List<Card>(computer.hand);
                Card cutCard = deck.Draw();
                Console.WriteLine($"The cut card is  {cutCard}");
                Pegging(round, player, computer);
                Scoring.PrintBoard(player.score, computer.score);
                round++;
                showDecks(computerFullHand, playerFullHand, cutCard, crib, player, computer, round);
            } 
            if (player.score > computer.score)
            {
                Console.WriteLine("Congratulations!!! You won!");
            }
            else
            {
                Console.WriteLine("I'm sorry, you lost.");
            }
            
        }

        private static void showDecks(List<Card> computerFullHand, List<Card> playerFullHand, Card cutCard, List<Card> crib, Human player, Computer computer, int round)
        {
             int handPoints;
                int cribPoints;
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                if (round % 2 == 0)
                {
                    Console.WriteLine("The computer's hand is:");
                    Console.WriteLine(string.Join(" ", computerFullHand));
                    handPoints = Scoring.ScoreHand(computerFullHand, cutCard);
                    Console.WriteLine($"The computer scores {handPoints} from hand");
                    computer.score += handPoints;
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    Console.WriteLine("Your hand is:");
                    Console.WriteLine(string.Join(" ", playerFullHand));
                    handPoints = Scoring.ScoreHand(playerFullHand, cutCard);
                    Console.WriteLine($"You score {handPoints} from hand");
                    player.score += handPoints;
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    Console.WriteLine("Your crib is:");
                    Console.WriteLine(string.Join(" ", crib));
                    cribPoints = Scoring.ScoreHand(crib, cutCard);
                    Console.WriteLine($"You score {cribPoints} from your crib");
                    player.score += cribPoints;
                }
                else
                {
                    Console.WriteLine("Your hand is:");
                    Console.WriteLine(string.Join(" ", playerFullHand));
                    handPoints = Scoring.ScoreHand(playerFullHand, cutCard);
                    Console.WriteLine($"You score {handPoints} from hand");
                    player.score += handPoints;
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    Console.WriteLine("The Computer's hand is:");
                    Console.WriteLine(string.Join(" ", computerFullHand));
                    handPoints = Scoring.ScoreHand(computerFullHand, cutCard);
                    Console.WriteLine($"Computer scores {handPoints} from hand");
                    computer.score += handPoints;
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    Console.WriteLine("The Computer's crib is:");
                    Console.WriteLine(string.Join(" ", crib));
                    cribPoints = Scoring.ScoreHand(crib, cutCard);
                    Console.WriteLine($"Computer scores {cribPoints} from crib");
                    computer.score += cribPoints;
                }
                Scoring.PrintBoard(player.score, computer.score);
                
            }

private static void StartUp()
        {
            Console.WriteLine("Welcome to a new game of cribbage!");
            Console.WriteLine("You will be playing a computer today!");
            Console.WriteLine("Lets start by determining who starts with the crib.");
        }

        private static int FirstCrib()
        {
            Console.WriteLine("Would you like to guess heads(h) or tails(t)");
            string line = Console.ReadLine();
            while (line != "h" && line != "t")
            {
                Console.WriteLine("That is not a valid choice.");
                Console.WriteLine("Please type an 'h' for heads or a 't' for tails.");
                line = Console.ReadLine();
            }

            Random rand = new Random();
            int number = rand.Next(0, 2);

            string guess = line == "h" ? "heads" : "tails";
            string other = line == "h" ? "tails" : "heads";

            if ((line == "h" && number == 0) || (line == "t" && number == 1))
            {
                Console.WriteLine($"You guessed {guess}");
                Console.WriteLine($"And the coin landed on {guess}! You start with the crib");
                return 1;
            }
            else
            {
                Console.WriteLine($"You guessed {guess}");
                Console.WriteLine($"And the coin landed on {other}! The computer starts with the crib");
                return 2;
            }
        }

        private static void Pegging(int round, Human player, Computer computer)
        {
            string lastPlayer = null;
            bool playerTurn = round % 2 == 1;
            int count = 0;
            bool playerGo = false;
            bool computerGo = false;
            List<Card> prev = new List<Card>();

            while (player.hand.Count > 0 || computer.hand.Count > 0)
            {
                Console.WriteLine($"The count is {count}");

                Card played = null;

                if (playerTurn)
                {
                    Console.WriteLine(string.Join(" ", player.hand));
                    played = player.Play(count);

                    if (played != null)
                    {
                        lastPlayer = "You";
                        Console.WriteLine($"You played: {played}");
                        player.hand.Remove(played);
                        count += Math.Min(played.Rank, 10);
                        player.score += Scoring.PeggingScore(prev, played, count);
                        prev.Add(played);
                        playerGo = false;

                        Console.WriteLine("Press Enter to continue...");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("You must go");
                        playerGo = true;

                        if (!computerGo && player.hand.Count > 0)
                        {
                            Console.WriteLine("The computer gets 1 point for go");
                            computer.score++;
                        }
                    }
                }
                else
                {
                    played = computer.Play(count, prev);

                    if (played != null)
                    {
                        lastPlayer = "The computer";
                        Console.WriteLine($"The computer played: {played}");
                        computer.hand.Remove(played);
                        count += Math.Min(played.Rank, 10);
                        computer.score += Scoring.PeggingScore(prev, played, count);
                        prev.Add(played);
                        computerGo = false;

                        Console.WriteLine("Press Enter to continue...");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Computer must go");
                        computerGo = true;

                        if (!playerGo && computer.hand.Count > 0)
                        {
                            Console.WriteLine("You get 1 point for go");
                            player.score++;
                        }
                    }
                }

                playerTurn = !playerTurn;

                if (playerGo && computerGo)
                {
                    Console.WriteLine("Neither player can play, the play resets.");
                    count = 0;
                    prev.Clear();
                    playerGo = false;
                    computerGo = false;
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    continue;
                }
            }

            if (lastPlayer == "You")
            {
                player.score++;
                Console.WriteLine("You get 1 point for last card");
            }
            else if (lastPlayer == "The computer")
            {
                computer.score++;
                Console.WriteLine("The computer gets 1 point for last card");
            }
        }
    }
}