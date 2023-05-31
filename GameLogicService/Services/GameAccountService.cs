using GameLogicService.Messaging.Interfaces;
using GameLogicService.Models.Entity;
using GameLogicService.Models.Enum;
using GameLogicService.Models.Messaging;
using GameLogicService.Models.Response;
using GameLogicService.Repositories.Entity.Interfaces;
using GameLogicService.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

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
            Console.WriteLine($"27 userid? {gameAccountResponse.sub}");
            Console.WriteLine($"27 username? {gameAccountResponse.Username}");
            Console.WriteLine($"27 email? {gameAccountResponse.EmailAddress}");
            var existingGameAccount = await _gameAccountRepository.GetByUsernameAndEmailAsync(gameAccountResponse.Username, gameAccountResponse.EmailAddress);
            if (existingGameAccount != null)
            {
                Console.WriteLine("30 userid?", existingGameAccount.UserId);
                Console.WriteLine("31 userid?", gameAccountResponse.sub);
                if((String.IsNullOrEmpty(existingGameAccount.UserId) || existingGameAccount.UserId == "1")  && !String.IsNullOrEmpty(gameAccountResponse.sub)) existingGameAccount = await UpdateAuthUserID(existingGameAccount, gameAccountResponse.sub);
                Console.WriteLine("33 userid?", existingGameAccount.UserId);
                SendNewUsersData(gameAccountResponse.Username, gameAccountResponse.EmailAddress);
                return $"{gameAccountResponse.Username}, welcome back!";
            }
            var addNewgameAccount = await _gameAccountRepository.AddAsync(new GameAccount()
            {
                UserId= gameAccountResponse.sub,
                Username = gameAccountResponse.Username,
                EmailAddress = gameAccountResponse.EmailAddress
            });
            if(addNewgameAccount == null) return Reject<string>(RejectionCode.General, "Failed adding game account to the system.");
            SendNewUsersData(gameAccountResponse.Username, gameAccountResponse.EmailAddress);
            return $"{gameAccountResponse.Username}, welcome to the QUIZ Game!";
            
        }

        private async Task<GameAccount> UpdateAuthUserID(GameAccount gameAccount, string userID)
        {
            gameAccount.UserId = userID;
            var result = await _gameAccountRepository.UpdateAsync(gameAccount);
            if (result is not null) return result;
            return gameAccount;
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
