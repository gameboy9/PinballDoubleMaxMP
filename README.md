# Double Max Matchplay for Pinball Tournaments
## Concept
The aim is to find a way to streamline 4-player Match play tournaments.  There have been many cases where people have to wait a really long time because of examples such as a machine (or two!) breaking down and/or all of the players playing spectacularly in a game, great as that is to see.

## The procedure

Note that this isn't fully implemented in code yet.  That said, here's how this would go:

- Round 1 begins with all matches created just like a traditional match play would.  This is one of two places where 3-player matches can take place.
- When a round completes, everyone goes to a queue and is given a 5-minute break. (by default)
  - When the 5-minute break ends, a new round for the player begins provided the software finds opponents they haven't played previously in the tournament.
    - This rule does not apply if the player has either played 75% of the field or all but four opponents, whichever happens first.  In that case, the opponents clear out and we start anew.
- After a certain number of rounds, you can call a break.  You can run your breaks in one of two ways:
  - A "rolling break":  The player receives their extended break after a certain number of rounds.  When that break ends, they're expected to be back because their next match has formed.
  - A "mass break":  Like traditional match plays, wait until all matches finish, and then everybody gets an extended break starting from the point of the end of the last match.
- When a player reaches the last round, the selection algorithm changes.  That player's next match will follow one of two branches:
  - If there are players who have not reached the last round, the player's last round will be against the three players who have played the least number of rounds, which serves as a "catch up" feature.
  - If all players have reached the last round, repeat what happened in round 1, where all of the remaining players are rolled into a traditional match play round.  This is the other one of the two places where 3-player matches can take place.
  - In either case, players can repeat opponents in this round to ensure that all players play their allotment of rounds.
