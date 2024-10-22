using CasinoApp.Models;

namespace CasinoApp.Data
{
    public class TestCasinoDb : IContext
    {
        List<AppUser> users = new List<AppUser>();
        List<DiceGameScore> diceScores=new List<DiceGameScore>();
        List<CardGameScore> cardScores=new List<CardGameScore>();
        List<Message> messages=new List<Message>();
        public TestCasinoDb() { }

        public List<CardGameScore> GetCardGameScores()
        {
            return cardScores;
        }
        public async Task<Message> GetMessageById(int id) { return new Message(); }
        public List<DiceGameScore> GetDiceGameScores()
        {
            return diceScores;
        }

        public List<Message> GetMessages()
        {
            return messages;
        }

        public List<AppUser> GetUsers()
        {
            return users;
        }
        public Object UpdateUser(AppUser user)
        {
            int i = 0;
            bool isFound = false;
            foreach (var item in users)
            {
                if(item.Name == user.Name)
                {
                    users[i] = user;
                    isFound = true;
                }
                i++;
            }
            return (isFound ? user : new {});

        }
        public async Task<int> Save()
        {
            
            return new Int32();
        }

        public int SaveUser(AppUser user)
        {
            throw new NotImplementedException();
        }

        Task<List<Message>> IContext.GetMessages()
        {
            throw new NotImplementedException();
        }

        Task<List<DiceGameScore>> IContext.GetDiceGameScores()
        {
            throw new NotImplementedException();
        }

        Task<List<CardGameScore>> IContext.GetCardGameScores()
        {
            throw new NotImplementedException();
        }

        public Task<List<DiceGameScore>> GetDiceGameScoresByName(string name)
        {
            throw new NotImplementedException();
        }

        Task<int> IContext.GetDiceGameById(int id)
        {
            throw new NotImplementedException();
        }

        Task<int> IContext.GetCardGameById(int id)
        {
            throw new NotImplementedException();
        }

        Task<int> IContext.StoreMessageAsync(Message m)
        {
            throw new NotImplementedException();
        }

        Task<int> IContext.StoreDiceGameAsync(DiceGameScore score)
        {
            throw new NotImplementedException();
        }

        Task<int> IContext.StoreCardGameAsync(CardGameScore score)
        {
            throw new NotImplementedException();
        }

        Task<int> IContext.DeleteDiceGameAsync(DiceGameScore score)
        {
            throw new NotImplementedException();
        }

        Task<int> IContext.DeleteCardGameAsync(CardGameScore score)
        {
            throw new NotImplementedException();
        }

        Task<int> IContext.DeleteMessageAsync(Message m)
        {
            throw new NotImplementedException();
        }

        Task<int> IContext.UpdateMessageAsync(Message m)
        {
            throw new NotImplementedException();
        }

        Task<List<Message>> IContext.GetMessagesById(string id)
        {
            throw new NotImplementedException();
        }

        Task<int> IContext.DeleteMessageByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<int> IContext.DeleteGamesByUser(string id)
        {
            throw new NotImplementedException();
        }

        Task<int> IContext.DeleteAllMsgByUserId(string id)
        {
            throw new NotImplementedException();
        }
    }
}
