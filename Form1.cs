using PinballDoubleMaxMP.Classes;
using System.ComponentModel;
using System.IO;
using System.Numerics;

namespace PinballDoubleMaxMP
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void massBreak_CheckedChanged(object sender, EventArgs e)
		{
			if (massBreak.Checked)
			{
				// Enable the mass break functionality
				breakLabel.Text = "Minutes Played";
				break1Rounds.Value = 150;
				break2Rounds.Value = 360;
				break1Minutes.Value = 30;
				break2Minutes.Value = 60;
			}
			else
			{
				breakLabel.Text = "Rounds Played";
				break1Rounds.Value = 6;
				break2Rounds.Value = 13;
				break1Minutes.Value = 35;
				break2Minutes.Value = 70;
			}
		}

		private void runSimulation_Click(object sender, EventArgs e)
		{
			TextWriter textWriter = new StreamWriter("simulation_output.txt");
			const int first = 1;
			runSim(0, true, textWriter);
			//for (int loopy = first; loopy < first + 1; loopy++)
			//{
			//	runSim(loopy, loopy == first, textWriter);
			//}
			textWriter.Close();
			MessageBox.Show("Simulation completed. Results written to simulation_output.txt");
		}

		private void runSim(int rand, bool printText, TextWriter textWriter)
		{

			List<Player> players = new List<Player>();
			List<Match> matches = new List<Match>();
			List<Match> shadowMatchQueue = new List<Match>();
			List<Machine> machines = new List<Machine>();
			bool finalStretch = false;
			bool finalRoundActivated = false;
			bool massBreak1Taken = false;
			bool massBreak2Taken = false;
			int minShadowRound = (int)roundCount.Value;

			int totalMatches = 0;
			double machineLength = 0.0;

			DateTime startTime = new DateTime(2025, 10, 1, 11, 0, 0);
			DateTime currentTime = startTime; // Start time of the simulation
			Queue queue = new Queue();
			Queue shadowQueue = new Queue();
			Random r1;
			if (rand == 0)
			{
				r1 = new Random();
			}
			else
			{
				r1 = new Random(rand);
			}

			for (int i = 0; i < playerCount.Value; i++)
			{
				Player newPlayer = new Player(i);
				players.Add(newPlayer);
				queue.AddPlayer(newPlayer);
			}
			for (int i = 0; i < machineCount.Value; i++)
			{
				machines.Add(new Machine());
			}

			// Establish initial matches.  This is one of two times in the tournament where a 3-player match can occur.
			while (queue.players.Count > 0)
			{
				if (queue.players.Count >= 3)
				{
					Match newMatch = new Match();
					int numPlayers = 4; // Default to 4 players
										// If there isn't an even number of players, create a 3-player match.
					if (queue.players.Count % 4 != 0)
						numPlayers = 3;
					// Choose players randomly
					for (int j = 0; j < numPlayers; j++)
					{
						int randomIndex = r1.Next(queue.players.Count);
						newMatch.players.Add(queue.players[randomIndex]);
						queue.RemovePlayer(queue.players[randomIndex]);
						newMatch.players[j].isActive = true; // Mark the player as active
					}
					newMatch.start = currentTime;
					// Assign a machine to the match
					if (machines.Count > 0)
					{
						int machineID = r1.Next(machines.Count);
						// TODO:  Address duplicates.
						newMatch.machineID = machines[machineID].id; // Assuming machines have an ID property
						matches.Add(newMatch);
						//machines[machineID].isAvailable = false; // Mark the machine as unavailable

						//if (machines[machineID].isAvailable)
						//{
						//}
					}
					if (printText)
						textWriter.WriteLine("Opening match started with players: " + string.Join(", ", newMatch.players.Select(p => "P" + p.id)) + " on machine " + newMatch.machineID + " at " + currentTime.ToString("HH:mm:ss"));
				}
				else
				{
					break; // Not enough players to start a match
				}
			}

			// Now the clock is going to start running.  Conclude matches starting at 10 minutes, with a 97.5% chance of continuing into the next minute.  Multiply that chance by 0.9 for each minute after the first.
			bool qualifyingOver = false;
			while (!qualifyingOver)
			{
				currentTime = currentTime.AddMinutes(1); // Increment the time by one minute
				List<Match> matchesToRemove = matches.Where(m => m.start.AddMinutes(10) <= currentTime).ToList();
				foreach (Match match in matchesToRemove)
				{
					double chanceToContinue = 0.975 * Math.Pow(0.98, (currentTime - match.start).TotalMinutes - 10);
					// Check if the match can be concluded
					if (r1.NextDouble() < chanceToContinue)
					{
						// Continue the match
						continue;
					}
					else
					{
						// Conclude the match
						int rank = 0;
						foreach (Player player in match.players)
						{
							rank++;
							for (int i = 0; i < match.players.Count(); i++)
							{
								// Include the player itself as an opponent so it doesn't count as a new opponent later on.
								player.opponents.Add(match.players[i]);
							}
							// I don't care if there are duplicate ranks in this simulation.
							player.score += rank == 1 ? 7 : rank == 2 ? 5 : rank == 3 ? 3 : 1;
							player.roundCount++; // Increment round count
							player.positionCount[rank - 1]++; // Increment position count
							player.breakStart = currentTime; // Set break start time
							player.isActive = false; // Mark player as inactive
							player.breakLength = (int)minBreak.Value;
							if (flexBreak.Checked && longBreak1.Checked && player.roundCount == break1Rounds.Value)
								player.breakLength = (int)break1Minutes.Value;
							if (flexBreak.Checked && longBreak2.Checked && player.roundCount == break2Rounds.Value)
								player.breakLength = (int)break2Minutes.Value;
							if (massBreak.Checked && currentTime >= startTime.AddMinutes((int)break1Rounds.Value) && !massBreak1Taken)
								player.breakLength = 999;
							if (massBreak.Checked && currentTime >= startTime.AddMinutes((int)break2Rounds.Value) && !massBreak2Taken)
								player.breakLength = 999;

							if (match.shadowMatch) player.shadowRoundsScheduled--; // Decrement shadow rounds scheduled if this is a shadow match
																				   // Do NOT add the player back to the queue if there is a shadow round scheduled!
							if (player.shadowRoundsScheduled == 0)
								queue.AddPlayer(player);
							if (printText)
								textWriter.WriteLine("P" + player.id + " finished match on machine " + match.machineID + " with rank " + rank + " at " + currentTime.ToString("HH:mm:ss") + ". Total score: " + player.score);
						}
						totalMatches++;
						// Add to machineLength the difference between the current time and the match start time.
						machineLength += (currentTime - match.start).TotalMinutes;
						matches.Remove(match);
						machines.FirstOrDefault(m => m.id == match.machineID).isAvailable = true; // Mark machine as available again
					}
				}

				if (massBreak.Checked && currentTime >= startTime.AddMinutes((int)break1Rounds.Value) && !massBreak1Taken && matches.Count == 0)
				{
					massBreak1Taken = true; // Mark the first mass break as taken
					foreach (Player player in players)
					{
						player.breakStart = currentTime; // Set break start time for all players
						player.breakLength = (int)break1Minutes.Value;
					}
					if (printText)
						textWriter.WriteLine("******************************* Mass break 1 started for all players at " + currentTime.ToString("HH:mm:ss"));
				}

				if (massBreak.Checked && currentTime >= startTime.AddMinutes((int)break2Rounds.Value) && !massBreak2Taken && matches.Count == 0)
				{
					massBreak2Taken = true; // Mark the first mass break as taken
					foreach (Player player in players)
					{
						player.breakStart = currentTime; // Set break start time for all players
						player.breakLength = (int)break2Minutes.Value;
					}
					if (printText)
						textWriter.WriteLine("******************************* Mass break 2 started for all players at " + currentTime.ToString("HH:mm:ss"));
				}

				// Next review the Shadow Match Queue.  If there are any matches there, make sure all players are available.  If they are, add them to the matches list.  Otherwise, skip that for now.
				for (int k = 0; k < shadowMatchQueue.Count;)
				{
					Match shadowMatch = shadowMatchQueue[k];
					bool allPlayersAvailable = true;
					foreach (Player player in shadowMatch.players)
					{
						if (player.isActive || ((currentTime - player.breakStart).TotalMinutes < player.breakLength))
						{
							allPlayersAvailable = false; // Player is not available
							break;
						}
					}
					if (allPlayersAvailable)
					{
						shadowMatch.start = currentTime;
						matches.Add(shadowMatch); // Add the shadow match to the matches list
						foreach (Player player in shadowMatch.players)
						{
							player.isActive = true; // Mark player as active
							queue.RemovePlayer(player); // Remove player from the main queue if they're in there.
						}
						shadowMatchQueue.Remove(shadowMatch); // Remove from shadow match queue
						if (printText)
							textWriter.WriteLine("Shadow match started with players: " + string.Join(", ", shadowMatch.players.Select(p => "P" + p.id)) + " on machine " + shadowMatch.machineID + " at " + currentTime.ToString("HH:mm:ss"));
					}
					else
					{
						k++;
					}
				}

				// Remove players from the queues if they are in their last round or if they finished qualifying.
				for (int k = 0; k < queue.players.Count;)
				{
					Player player = queue.players[k]; // Get the first player in the playable queue

					// If a player completed their rounds, their qualifying is over.
					if (player.roundCount >= roundCount.Value - 1)
					{
						queue.RemovePlayer(player); // Remove from playable queue
						queue.RemovePlayer(player); // Remove from main queue
						continue;
					}
					k++;
				}

				// Now review the queue.  If there are players in the queue that have waited long enough, they can possibly be added to a new match.
				Queue playable1 = new Queue();
				foreach (Player player in queue.players)
				{
					if ((currentTime - player.breakStart).TotalMinutes >= (double)player.breakLength)
					{
						playable1.AddPlayer(player); // Player is ready to play
					}
				}

				int playersStillPlaying = players.Where(p => p.roundCount < roundCount.Value).Count();
				while (playable1.players.Count > 0)
				{
					Player player = playable1.players[0]; // Get the first player in the playable queue

					// Clear opponents if they have played against at least 75% of the players still playing or if there are 6 players or less that the player hasn't played against.
					if (player.opponents.Count >= playersStillPlaying * 3 / 4 || player.opponents.Count >= playersStillPlaying - 6)
					{
						player.opponents.Clear();
						player.opponents.Add(player); // Add the player itself as an opponent so it doesn't count as a new opponent later on.
					}

					// See if a player has enough opponents in the queue that they haven't played against yet.
					var opponentsNotPlayed = playable1.players.Except(player.opponents).ToList();
					if (opponentsNotPlayed.Count < 3)
					{
						playable1.RemovePlayer(player); // Remove the player from the playable queue
						continue; // Player has played against too many opponents... skip to the next player.
					}
					else
					{
						// Create a new match with the opponents not played against.  Remove those players from the queue.
						List<Player> matchPlayers = new List<Player> { player };

						bool success = false;
						List<Player> opponents = new List<Player>();
						for (int i = 0; i < 10; i++)
						{
							List<Player> oppNotPlayedCopy = opponentsNotPlayed.ToList();
							opponents = new List<Player>();
							for (int k = 0; k < 3; k++)
							{
								if (oppNotPlayedCopy.Count < 3 - k)
								{
									// Not enough opponents left to fill the match, so break out of the loop
									success = false;
									break;
								}
								int j = r1.Next(oppNotPlayedCopy.Count);
								Player opponent = oppNotPlayedCopy[j];
								opponents.Add(opponent); // Add the opponent to the list of opponents
								oppNotPlayedCopy.RemoveAt(j);
								oppNotPlayedCopy = oppNotPlayedCopy.Except(opponent.opponents).ToList(); // Remove any opponents that the new opponent has already played against
								if (k == 2) success = true; // If we successfully added all three opponents, set success to true
							}
							if (success) break; // If we successfully added all three opponents, break out of the loop
						}
						if (success)
						{
							for (int k = 0; k < 3; k++)
							{
								playable1.RemovePlayer(opponents[k]);
								queue.RemovePlayer(opponents[k]);
								matchPlayers.Add(opponents[k]);
								opponents[k].isActive = true; // Mark the opponent as active
							}
							player.isActive = true; // Mark the player as active
							playable1.RemovePlayer(player);
							queue.RemovePlayer(player);
						}
						else
						{
							playable1.RemovePlayer(player); // Remove the player from the playable queue
							continue; // Player has played against too many opponents... skip to the next player.
						}
						matches.Add(new Match
						{
							players = matchPlayers,
							start = currentTime,
							machineID = machines[r1.Next(machines.Count)].id // Assign a random machine.  TODO:  Address duplicates
						});
						if (printText)
							textWriter.WriteLine("Match started with players: " + string.Join(", ", matchPlayers.Select(p => "P" + p.id)) + " on machine " + matches.Last().machineID + " at " + currentTime.ToString("HH:mm:ss"));
					}
				}

				if (currentTime > new DateTime(2025, 10, 1, 21, 0, 0))
				{
					currentTime = currentTime;
				}

				// Any players remaining in the queue must wait at least an additional minute before they can be added to a match.

				// Now let's address players that are in their last round.  They're going to be placed in a "shadow match queue".
				// This queue will consist of the player's last round; their opponents will be the three people that have played the fewest matches so far.
				// This will allow those players who have fell behind to catch up.
				List<Player> lastRoundPlayers = players.Where(c => c.roundCount == roundCount.Value - 1 && c.shadowRoundsScheduled == 0).ToList();
				while (lastRoundPlayers.Count() > 0)
				{
					Player player = lastRoundPlayers[0];

					// Terminate the shadow queue if all players are on their last round or are about to be on their last round.
					if (players.Where(p => p.roundCount + p.shadowRoundsScheduled < roundCount.Value - 1).Count() == 0)
					{
						lastRoundPlayers.Clear();
						finalStretch = true; // Set final stretch flag
						break;
					}
					List<Player> shadowMatchPlayers = players.Where(p => p.roundCount + p.shadowRoundsScheduled < roundCount.Value - 1)
						.OrderBy(p => p.roundCount + p.shadowRoundsScheduled)
						.Take(3)
						.ToList();
					shadowMatchPlayers.Add(player); // Add the player to the shadow match
					foreach (Player p in shadowMatchPlayers)
					{
						if (p.roundCount < minShadowRound) minShadowRound = p.roundCount; // Update the minimum shadow round if necessary
						p.shadowRoundsScheduled++;
					}
					shadowMatchQueue.Add(new Match
					{
						players = shadowMatchPlayers,
						start = currentTime,
						shadowMatch = true, // Mark this as a shadow match
						machineID = machines[r1.Next(machines.Count)].id // Assign a random machine.  TODO:  Address duplicates
					});
					if (lastRoundPlayers.Count > 0)
						lastRoundPlayers.RemoveAt(0); // Remove the player from the shadow queue
				}

				// Now let's address the rest of the queue.  In that case, wait until all of the remaining players are at the last round.
				if (finalStretch && shadowMatchQueue.Count == 0 && matches.Count == 0 && !finalRoundActivated && players.Where(p => p.roundCount < roundCount.Value - 1).Count() == 0)
				{
					// All remaining players are at the last round, so we can conclude the simulation.
					// All remaining matches will go into 2, 3, or 4 player matches, depending on how many players are left.
					List<Player> remainingPlayers = players.Where(p => p.roundCount == roundCount.Value - 1).ToList();

					while (remainingPlayers.Count > 0)
					{
						int numPlayers = 4;
						// If there are 2 or 5 players remaining, set up a 2-player match.  Not optimal, but it will work.
						if (remainingPlayers.Count == 2 || remainingPlayers.Count == 5) numPlayers = 2;
						// If there are 3, 6, 7, or 9 players remaining, set up a 3-player match.
						else if (remainingPlayers.Count % 4 != 0) numPlayers = 3;

						List<Player> matchPlayers = new List<Player>();
						for (int i = 0; i < numPlayers; i++)
						{
							int j = r1.Next(remainingPlayers.Count);
							matchPlayers.Add(remainingPlayers[j]);
							remainingPlayers[j].isActive = true; // Mark the player as active
							remainingPlayers.RemoveAt(j);
						}
						matches.Add(new Match
						{
							players = matchPlayers,
							start = currentTime,
							machineID = machines[r1.Next(machines.Count)].id // Assign a random machine.  TODO:  Address duplicates
						});
						if (printText)
							textWriter.WriteLine("Final match started with players: " + string.Join(", ", matchPlayers.Select(p => "P" + p.id)) + " on machine " + matches.Last().machineID + " at " + currentTime.ToString("HH:mm:ss"));
						continue;
					}
					finalRoundActivated = true; // Set final round activated flag
				}

				// If there are no more matches and all players have completed their rounds, then the simulation is over.
				if (players.Where(p => p.roundCount < roundCount.Value).Count() == 0 || currentTime > new DateTime(2025, 10, 1, 23, 59, 0))
					qualifyingOver = true; // Set qualifying over flag
			}
			// Simulation is over.  Write final results to the text file.
			// Verify that all players have completed their rounds and haven't completed more rounds than they should have.
			if (printText)
			{
				textWriter.WriteLine("Simulation ended at " + currentTime.ToString("HH:mm:ss"));
				textWriter.WriteLine("Total matches played: " + totalMatches);
				textWriter.WriteLine("Average machine length: " + (machineLength / totalMatches).ToString("F2") + " minutes");
				textWriter.WriteLine("Final player scores:");

				foreach (Player player in players)
				{
					textWriter.WriteLine("P" + player.id + ": " + player.score + " points, " + player.roundCount + " rounds played, Positions: " + string.Join(", ", player.positionCount));
				}
			}

			if (players.Where(p => p.roundCount != roundCount.Value).Count() == 0)
			{
				textWriter.WriteLine("Seed " + rand + ":  No player round miscounts.  Min shadow round:  " + minShadowRound + " - End time:  " + currentTime.ToString("HH:mm:ss"));
			}
			foreach (Player player in players.Where(p => p.roundCount != roundCount.Value))
			{
				textWriter.WriteLine("WARNING on seed " + rand + ":  Player " + player.id + " has completed " + player.roundCount + " rounds, but should have completed " + roundCount.Value + " rounds.");
			}
		}
	}
}
