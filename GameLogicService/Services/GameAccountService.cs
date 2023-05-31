using GameLogicService.Messaging.Interfaces;
using GameLogicService.Models.Entity;
using GameLogicService.Models.Enum;
using GameLogicService.Models.Messaging;
using GameLogicService.Models.Response;
using GameLogicService.Repositories.Entity.Interfaces;
using GameLogicService.Services.Interfaces;

namespace GameLogicService.Services
{
    public class GameAccountService : BaseService, IGameAccountService
    {
        private readonly IGameAccountRepository _gameAccountRepository;
        private readonly IMessageSender _messageSender;

        public GameAccountService(
            IGameAccountRepository gameAccountRepository,
            IMessageSender messageSender)
        {
            _gameAccountRepository = gameAccountRepository ?? throw new ArgumentNullException(nameof(gameAccountRepository));
            _messageSender = messageSender ?? throw new ArgumentNullException(nameof(messageSender));
        }

        public async Task<ServiceProduct<string>> ProcessUser(GameAccountResponse gameAccountResponse)
        {
            var existingGameAccount = await _gameAccountRepository.GetByUsernameAndEmailAsync(gameAccountResponse.Username, gameAccountResponse.EmailAddress);
            if (existingGameAccount != null)
            {
                if(existingGameAccount.UserId is null && gameAccountResponse.UserId is not null)await UpdateAuthUserID(existingGameAccount, gameAccountResponse.UserId);
                SendNewUsersData(gameAccountResponse.Username, gameAccountResponse.EmailAddress);
                return $"{gameAccountResponse.Username}, welcome back!";
            }
            var addNewgameAccount = await _gameAccountRepository.AddAsync(new GameAccount()
            {
                UserId= gameAccountResponse.UserId,
                Username = gameAccountResponse.Username,
                EmailAddress = gameAccountResponse.EmailAddress
            });
            if(addNewgameAccount == null) return Reject<string>(RejectionCode.General, "Failed adding game account to the system.");
            SendNewUsersData(gameAccountResponse.Username, gameAccountResponse.EmailAddress);
            return $"{gameAccountResponse.Username}, welcome to the QUIZ Game!";
            
        }

        private async Task UpdateAuthUserID(GameAccount gameAccount, string userID)
        {
            gameAccount.UserId = userID;
            await _gameAccountRepository.UpdateAsync(gameAccount);
        }

        private void SendNewUsersData(string username, string emailAddress)
        {
            _messageSender.NewRegisteredUser(new NewPlayerScoreEntity() 
            {
                Username = username,
                EmailAddress = emailAddress
            });
        }

        //public async Task<ServiceProduct<string>> DeleteUser(GameAccountResponse gameAccountResponse)
        //{


        //}

        //private void NotifyData(string emailAddress)
        //{
        //    _messageSender.DeleteUserData(emailAddress);
        //}

    }
}
