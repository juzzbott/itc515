using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrownAndAnchorGame
{
    class BelowLimitException : ArgumentException
    {
        public BelowLimitException() { }
        public BelowLimitException(string message) : base(message) { }
        public BelowLimitException(string message, Exception inner) : base(message, inner) { }

    }

    public class Player
    {
        private string name;
        public string Name { get { return name; } }
        private int balance;
        public int Balance { get { return balance; } }
        private int limit;
        public int Limit
        {
            get { return limit; }
            set
            {
                if (value < 0) throw new ArgumentException("Limit cannot be negative");
                if (value > balance) throw new ArgumentException("Limit cannot be greater than balance");
                this.limit = value;
            }
        }

        public Player(string name, int initialBalance)
        {
            if (initialBalance < 0) throw new ArgumentException("Initial balance cannot be negative");
            if (name == null || String.Equals(name, "", StringComparison.CurrentCulture))
                throw new ArgumentException("Name cannot be null or empty");

            this.name = name;
            this.balance = initialBalance;
            this.limit = 0;
        }

        public bool balanceExceedsLimit()
        {
            return (balance > limit);
        }

        public bool balanceExceedsLimitBy(int amount)
        {
            return (balance - amount > limit);
        }

        public void takeBet(int bet)
        {
            if (bet < 0) throw new ArgumentException("WTF? Bet cannot be negative");
            if (!balanceExceedsLimitBy(bet)) throw new ArgumentException("Placing bet would go below limit");
            balance = balance - bet;
        }

        public void receiveWinnings(int winnings)
        {
            if (winnings < 0) throw new ArgumentException("WTF? Winnings cannot be negative");
            balance = balance + winnings;
        }

        public override string ToString()
        {
            return string.Format("Player: {0}, Balance: {1}, Limit: {2}", name, balance, limit);
        }
    }
}
