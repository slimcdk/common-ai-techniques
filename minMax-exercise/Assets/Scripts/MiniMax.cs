using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMax {

    // minimax for the O player, returns an array with [value, move] information
    public static int[] Minimax(string[] state, int depth, bool maximizingPlayer)
    {
        // Debug.Log("MINMAX");
        //IMPLEMENT minimax
        // Debug.Log(depth)
        if(depth == 0 || IsTerminal(state))
        {
            return new int[] {HeuristicValueOfState(state, "O"), -1};
        }

        int bestMove = -1;
        int bestValue;


        if(maximizingPlayer)
        {
            bestValue = int.MinValue;
            foreach(int move in PossibleMoves(state))
            {
                string[] childState = CalculateNewState(state, "O", move);
                int[] v = Minimax(childState, depth-1, false);
                //bestValue = v[0] > bestValue ? v[0] : v[1];
                //bestMove = v[0] > bestValue ? v[1] : v[0];

                if(v[0] > bestValue)
                {
                    bestValue = v[0];
                    bestMove = move;
                }
            }
        }
        else {
            bestValue = int.MaxValue;
            foreach(int move in PossibleMoves(state))
            {
                string[] childState = CalculateNewState(state, "X", move);
                int[] v = Minimax(childState, depth-1, true);
                if(v[0] < bestValue)
                {
                    bestValue = v[0];
                    bestMove = move;
                }
            }
        }
        return new int[] { bestValue, bestMove};
    }

    // calculate the heuristic value of the state
    static int HeuristicValueOfState(string[] state, string player)
    {
        // Debug.Log("HEURISTIC");
        //IMPLEMENT A HEURISTIC
        if(IsTerminal(state, player))
        {
            return 1;
        }

        string opponent = player == "X" ? "O" : "X";
        if(IsTerminal(state, opponent))
        {
            return -1;
        }

        return 0;
    }

    //Calculate new state starting from the current one, the player making the move, and where it wants to place its token
    public static string[] CalculateNewState(string[] state, string player, int tile)
    {
        string[] clone = (string[])state.Clone();

        //IMPLEMENT: calculate new state
        clone[tile] = player;

        return clone;
    }

    // returns a list of all possible moves that can be currently made
    static List<int> PossibleMoves(string[] state)
    {
        // Debug.Log("MOVES");
        List<int> moves = new List<int>();
        //IMPLEMENT: calculate all possible moves that can be made from the specified state (the squares in which you can put something in)
        for (int i = 0; i < state.Length; i++)
        {
            if (state[i] == ".")
            {
                moves.Add(i);
            }
        }

        return moves;
    }

    //returns true if a player has won or if there is a draw, so the state cannot be expanded anymore
    static bool IsTerminal(string[] state)
    {
        if (IsTerminal(state, "X"))
            return true;
        if (IsTerminal(state, "O"))
            return true;

        bool foundEmptySpot = false;
        foreach (string s in state)
        {
            if (s == ".")
            {
                foundEmptySpot = true;
                break;
            }
        }
        if (!foundEmptySpot)
            return true;
        return false;
    }

    //returns true if specified player has won
    static bool IsTerminal(string[] state, string player)
    {
        if (state[0] == player && state[1] == player && state[2] == player)
        {
            return true;
        }
        else if (state[3] == player && state[4] == player && state[5] == player)
        {
            return true;
        }
        else if (state[6] == player && state[7] == player && state[8] == player)
        {
            return true;
        }
        else if (state[0] == player && state[3] == player && state[6] == player)
        {
            return true;
        }
        else if (state[1] == player && state[4] == player && state[7] == player)
        {
            return true;
        }
        else if (state[2] == player && state[5] == player && state[8] == player)
        {
            return true;
        }
        else if (state[0] == player && state[4] == player && state[8] == player)
        {
            return true;
        }
        else if (state[2] == player && state[4] == player && state[6] == player)
        {
            return true;
        }
        return false;
    }
}
