using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinballDoubleMaxMP.Classes
{
	internal class Player
	{
		public int id = 0;
		public int score = 0;
		public int roundCount = 0;
		public int shadowRoundsScheduled = 0; // Number of shadow rounds scheduled for the player
		public bool isActive = false; // Indicates if the player is currently active in the game
		public List<Player> opponents = new List<Player>();
		public List<Machine> machines = new List<Machine>();
		public DateTime breakStart = new DateTime();
		public int breakLength = 0;
		public List<int> positionCount = new List<int>();

		// Constructor to initialize a player with a unique ID
		public Player(int playerId)
		{
			id = playerId;
			score = 0;
			roundCount = 0;
			opponents = new List<Player>();
			machines = new List<Machine>();
			breakStart = DateTime.MinValue; // Initialize to a default value
			positionCount = new List<int> { 0, 0, 0, 0 };
			isActive = false;
		}
	}
}
