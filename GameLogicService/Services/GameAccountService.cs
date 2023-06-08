using GameLogicService.Messaging.Interfaces;
using GameLogicService.Models.Entity;
using GameLogicService.Models.Enum;
using GameLogicService.Models.Messaging;
using GameLogicService.Models.Response;
using GameLogicService.Repositories.Entity.Interfaces;
using GameLogicService.RestClientRequests.Interfaces;
using GameLogicService.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace GameLogicService.Services
{
    public class GameAccountService : BaseService, IGameAccountService
    {
        private readonly IGameAccountRepository _gameAccountRepository;
        private readonly IGameAccountAuthRepository _gameAccountAuthRepository;
        private readonly IMessageSender _messageSender;
        private readonly IAuthAPIRequests _authAPIRequests;
        private readonly IDeleteUserDataAPIRequests _deleteUserDataAPIRequests;

        public GameAccountService(
            IGameAccountRepository gameAccountRepository,
            IGameAccountAuthRepository gameAccountAuthRepository,
            IMessageSender messageSender,
            IAuthAPIRequests authAPIRequests,
            IDeleteUserDataAPIRequests deleteUserDataAPIRequests)
        {
            _gameAccountRepository = gameAccountRepository ?? throw new ArgumentNullException(nameof(gameAccountRepository));
            _gameAccountAuthRepository = gameAccountAuthRepository ?? throw new ArgumentNullException(nameof(gameAccountAuthRepository));
            _messageSender = messageSender ?? throw new ArgumentNullException(nameof(messageSender));
            _authAPIRequests = authAPIRequests ?? throw new ArgumentNullException(nameof(authAPIRequests));
            _deleteUserDataAPIRequests = deleteUserDataAPIRequests ?? throw new ArgumentNullException(nameof(deleteUserDataAPIRequests));
        }

        public async Task<ServiceProduct<string>> ProcessUser(GameAccountResponse gameAccountResponse)
        {
            var existingGameAccount = await _gameAccountRepository.GetByUsernameAndEmailAsync(gameAccountResponse.Username, gameAccountResponse.EmailAddress);
            if (existingGameAccount != null)
            {
                var accountAuth = await _gameAccountAuthRepository.GetByAccountIdAsync(existingGameAccount.Id);
                if (accountAuth is null && !String.IsNullOrEmpty(gameAccountResponse.userId)) accountAuth = await UpdateAuthUserID(existingGameAccount.Id, gameAccountResponse.userId);
                if (accountAuth is not null) Console.WriteLine(accountAuth.AuthId.ToString());
                SendNewUsersData(gameAccountResponse.Username, gameAccountResponse.EmailAddress);
                return $"{gameAccountResponse.Username}, welcome back!";
            }
            var addNewgameAccount = await _gameAccountRepository.AddAsync(new GameAccount()
            {
                Username = gameAccountResponse.Username,
                EmailAddress = gameAccountResponse.EmailAddress
            });
            if (addNewgameAccount == null) return Reject<string>(RejectionCode.General, "Failed adding game account to the system.");
            SendNewUsersData(gameAccountResponse.Username, gameAccountResponse.EmailAddress);
            return $"{gameAccountResponse.Username}, welcome to the QUIZ Game!";

        }

        private async Task<GameAccountAuth> UpdateAuthUserID(int gameAccountId, string userAuthId)
        {
            var result = await _gameAccountAuthRepository.AddAsync(new GameAccountAuth()
            {
                AccountId = gameAccountId,
                AuthId = userAuthId
            });
            if (result is not null) return result;
            return null;
        }

        private void SendNewUsersData(string username, string emailAddress)
        {
            _messageSender.NewRegisteredUser(new NewPlayerScoreEntity()
            {
                Username = username,
                EmailAddress = emailAddress
            });
        }

        public async Task<ServiceProduct<bool>> DeleteUser(DeleteAccountDataResponse deleteAccountDataResponse)
        {
            var existingGameAccount = await _gameAccountRepository.GetByUsernameAndEmailAsync(deleteAccountDataResponse.Username, deleteAccountDataResponse.EmailAddress);
            if (existingGameAccount is null) return Reject<bool>(RejectionCode.General, "No such account exists in game logic service.");
            var accountAuth = await _gameAccountAuthRepository.GetByAccountIdAsync(existingGameAccount.Id);
            if (accountAuth is null) return Reject<bool>(RejectionCode.General, "No such account exists in auth0.");

            var deletionResult = await _deleteUserDataAPIRequests.DeleteUserData(deleteAccountDataResponse.EmailAddress);
            if (String.IsNullOrEmpty(deletionResult) || deletionResult == "true") return Reject<bool>(RejectionCode.General, "Could not delete user from game scoreboard service");
            //NotifyDeleteUserData(deleteAccountDataResponse.EmailAddress);

            var auth0DeletionResult = await _authAPIRequests.DeleteAuthUserData(accountAuth.AuthId);
            if(!auth0DeletionResult) return Reject<bool>(RejectionCode.General, "Could not delete user from auth0");

            var gameAccountDeletionResult = await _gameAccountRepository.DeleteAsync(existingGameAccount.Id);
            if (!gameAccountDeletionResult) return Reject<bool>(RejectionCode.General, "Could not delete user from game logic service");
            return true;
        }

        private void NotifyDeleteUserData(string emailAddress)
        {
            _messageSender.DeleteUserData(emailAddress);
        }
    }
}
