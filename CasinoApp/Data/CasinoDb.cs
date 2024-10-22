using CasinoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CasinoApp.Data
{
    public class CasinoDb : IContext
    {
        AppDbContext _db;
        public CasinoDb(AppDbContext c) => _db = c;

        #region Get Functions
        public async Task<List<CardGameScore>> GetCardGameScores()
        {
            return  await _db.CardGameScores.Include(s => s.Player).ToListAsync();
        }
        public async Task<List<DiceGameScore>> GetDiceGameScores()
        {
            return await _db.DiceGameScores.Include(s=>s.Player).ToListAsync();
        }
        public async Task<List<DiceGameScore>> GetDiceGameScoresByName(string name)
        {
            return await _db.DiceGameScores.Where(s => s.Player.UserName.ToUpper() == name.ToUpper()).Include(s=>s.Player).Select(s => s).ToListAsync();
        }
        public async Task<List<Message>> GetMessages()
        {
            return  await _db.Messages.Include(m=>m.Sndr).Include(m=>m.Rcpnt).ToListAsync<Message>();
        }
        public async Task<List<Message>> GetMessagesById(string id)
        {
            return await _db.Messages.Include(m => m.Sndr).Include(m => m.Rcpnt).Where(m=>m.Rcpnt.Id == id).ToListAsync<Message>();
        }
        public async Task<Message> GetMessageById(int id)
        {
            return await _db.Messages.Include(m => m.Sndr).Include(m => m.Rcpnt).Where(m => m.MsgId == id).FirstOrDefaultAsync();
        }
        public async Task<int> GetDiceGameById(int id)
        {
            var score = _db.DiceGameScores.Where(d => d.Key == id).FirstOrDefault();
            _db.DiceGameScores.Remove(score);
            return await _db.SaveChangesAsync();
        }
        public async Task<int> GetCardGameById(int id)
        {
            var score = _db.CardGameScores.Where(d => d.Key == id).FirstOrDefault();
            _db.CardGameScores.Remove(score);
            return await _db.SaveChangesAsync();
        }
        #endregion

        #region Store Functions
        public async Task<int> Save()
        {
            return await _db.SaveChangesAsync();
        }
        public async Task<int> StoreMessageAsync(Message m) {
            await _db.Messages.AddAsync(m);
            return await _db.SaveChangesAsync();
        }
        public async Task<int> StoreDiceGameAsync(DiceGameScore score)
        {
            await _db.DiceGameScores.AddAsync(score);
            return await _db.SaveChangesAsync();
        }
        public async Task<int> StoreCardGameAsync(CardGameScore score)
        {
            await _db.CardGameScores.AddAsync(score);
            return await _db.SaveChangesAsync();
        }
        
        #endregion

        #region Delete Functions
        public async Task<int> DeleteDiceGameAsync(DiceGameScore score)
        {
            _db.DiceGameScores.Remove(score);
            return await _db.SaveChangesAsync();
        }
        public async Task<int> DeleteCardGameAsync(CardGameScore score)
        {
            _db.CardGameScores.Remove(score);
            return await _db.SaveChangesAsync();
        }
        public async Task<int> DeleteGamesByUser(string id)
        {
            var games = await _db.CardGameScores.Where(c=>c.Player.Id == id).ToListAsync();
            games.ForEach(g => { _db.CardGameScores.Remove(g); _db.SaveChangesAsync(); });
            var games2 = await _db.DiceGameScores.Where(c => c.Player.Id == id).ToListAsync();
            games2.ForEach(g => { _db.DiceGameScores.Remove(g); _db.SaveChangesAsync(); });
            return await _db.SaveChangesAsync();
        }
        public async Task<int> DeleteAllMsgByUserId(string id)
        {
            var messages = await _db.Messages.Where(m => m.Sndr.Id == id).Include(m=>m.Replies).ToListAsync();
            messages.ForEach(m => { _db.Messages.Remove(m); });
            return await _db.SaveChangesAsync();
        }
        public async Task<int> DeleteMessageAsync(Message m) { 
           _db.Messages.Remove(m);
            return await _db.SaveChangesAsync();
        }
        public async Task<int> DeleteMessageByIdAsync(int id)
        {
            var m = await _db.Messages.Where(m=>m.MsgId==id).Include(m=>m.Replies).FirstAsync();
            _db.Messages.Remove(m);
            return await _db.SaveChangesAsync();
        }
        #endregion

        #region Update Functions
        public async Task<int> UpdateMessageAsync(Message m) {
            _db.Messages.Update(m);
            return await _db.SaveChangesAsync();
        }

        #endregion
    }
}
