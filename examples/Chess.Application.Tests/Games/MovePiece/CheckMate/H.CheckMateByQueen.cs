﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Kingo.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kingo.Samples.Chess.Games.MovePiece.CheckMate
{
    [TestClass]
    public sealed class CheckMateByQueen : MovePieceScenario
    {
        protected override IEnumerable<IMessageSequence> Given()
        {
            yield return base.Given().Concatenate();
            yield return WhitePlayerMove("e2", "e4");
            yield return BlackPlayerMove("f7", "f6");
            yield return WhitePlayerMove("d2", "d3");
            yield return BlackPlayerMove("g7", "g5");
        }

        protected override MessageToHandle<MovePieceCommand> When()
        {
            return WhitePlayerMove("d1", "h5");
        }

        [TestMethod]
        public override async Task ThenAsync()
        {
            await ExpectPieceMovedEvent(GameState.CheckMate);
        }
    }
}
