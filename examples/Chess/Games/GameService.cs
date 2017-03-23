﻿using System.Threading.Tasks;

namespace Kingo.Samples.Chess.Games
{    
    public sealed class GameService : ServiceProcessor, IGameService
    {
        #region [====== Write Methods ======]

        public Task MovePiece(MovePieceCommand command)
        {
            return HandleAsync(command);
        }

        public Task ForfeitGameAsync(ForfeitGameCommand command)
        {
            return HandleAsync(command);
        }

        #endregion

        #region [====== Read Methods ======]

        public Task<GetActiveGamesResponse> GetActiveGames(GetActiveGamesRequest request)
        {
            return ExecuteAsync(ActiveGamesTable.SelectByPlayerAsync, request);
        }        

        #endregion
    }
}