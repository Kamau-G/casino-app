using CasinoApp.Data;
using CasinoApp.Models;
namespace CasinoApp.Data
{
    public interface IContext
    {
        public  Task<Message> GetMessageById(int id);
        public Task<List<Message>> GetMessages();
        public Task <List<DiceGameScore>> GetDiceGameScores();
        public Task<List<CardGameScore>> GetCardGameScores();
        public Task<List<DiceGameScore>> GetDiceGameScoresByName(string name);
        public Task<int> GetDiceGameById(int id);
        public Task<int> GetCardGameById(int id);
        public Task<int> StoreMessageAsync(Message m);
        public Task<int> StoreDiceGameAsync(DiceGameScore score);
        public  Task<int> StoreCardGameAsync(CardGameScore score);
        public  Task<int> DeleteDiceGameAsync(DiceGameScore score);
        public  Task<int> DeleteCardGameAsync(CardGameScore score);
        public Task<int> DeleteGamesByUser(string id);
        public  Task<int> DeleteMessageAsync(Message m);
        public Task<int> DeleteMessageByIdAsync(int id);
        public Task<int> DeleteAllMsgByUserId(string id);
        public Task<int> UpdateMessageAsync(Message m);
        public Task<List<Message>> GetMessagesById(string id);
        public Task<int> Save();
    }
}
