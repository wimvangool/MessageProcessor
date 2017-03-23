﻿using System;
using System.Collections.Generic;
using System.Linq;
using Kingo.Messaging.Domain;

namespace Kingo.Samples.Chess.Games
{
    internal sealed class Bishop : Piece
    {
        public Bishop(IDomainEventBus<Guid, int> eventBus, ColorOfPiece color)
        {
            EventBus = eventBus;
            Color = color;
        }

        protected override IDomainEventBus<Guid, int> EventBus
        {
            get;
        }

        protected override ColorOfPiece Color
        {
            get;
        }

        protected override TypeOfPiece Type
        {
            get { return TypeOfPiece.Bishop; }
        }

        protected override IEnumerable<Square> GetPossibleSquaresToMoveTo(Square from)
        {
            return PossibleSquaresToMoveTo(from);
        }

        internal static IEnumerable<Square> PossibleSquaresToMoveTo(Square from)
        {
            return PossibleMovesUpLeft(from)
                .Concat(PossibleMovesUpRight(from))
                .Concat(PossibleMovesDownRight(from))
                .Concat(PossibleMovesDownLeft(from));
        }

        private static IEnumerable<Square> PossibleMovesUpLeft(Square from)
        {
            int fileSteps = -1;
            int rankSteps = 1;
            Square to;

            while (from.TryAdd(fileSteps, rankSteps, out to))
            {
                fileSteps--;
                rankSteps++;

                yield return to;
            }
        }

        private static IEnumerable<Square> PossibleMovesUpRight(Square from)
        {
            int fileSteps = 1;
            int rankSteps = 1;
            Square to;

            while (from.TryAdd(fileSteps, rankSteps, out to))
            {
                fileSteps++;
                rankSteps++;

                yield return to;
            }
        }

        private static IEnumerable<Square> PossibleMovesDownRight(Square from)
        {
            int fileSteps = 1;
            int rankSteps = -1;
            Square to;

            while (from.TryAdd(fileSteps, rankSteps, out to))
            {
                fileSteps++;
                rankSteps--;

                yield return to;
            }
        }

        private static IEnumerable<Square> PossibleMovesDownLeft(Square from)
        {
            int fileSteps = -1;
            int rankSteps = -1;
            Square to;

            while (from.TryAdd(fileSteps, rankSteps, out to))
            {
                fileSteps--;
                rankSteps--;

                yield return to;
            }
        }

        protected override bool IsSupportedMove(ChessBoard board, Move move, ref Func<PieceMovedEvent> eventFactory)
        {
            if (base.IsSupportedMove(board, move, ref eventFactory))
            {
                return move.IsCrossPath && move.IsEmptyPath(board);
            }
            return false;
        }        
    }
}