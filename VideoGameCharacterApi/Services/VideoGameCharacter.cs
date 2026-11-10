using Microsoft.EntityFrameworkCore;
using VideoGameCharacterApi.Data;
using VideoGameCharacterApi.Interfaces;
using VideoGameCharacterApi.Models;

namespace VideoGameCharacterApi.Services
{
    public class VideoGameCharacter(AppDbContext db) : IVideoGameCharacter
    {
        public async Task<Character> CreateAsync(Character character, CancellationToken ct = default)
        {
            db.Characters.Add(character);
            await db.SaveChangesAsync(ct);
            return character;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var character = await db.Characters.FindAsync([id], ct);
            if (character == null)
            {
                return false;
            }

            db.Characters.Remove(character);
            await db.SaveChangesAsync(ct);
            return true;
        }

        public async Task<IReadOnlyList<Character>> GetAllAsync(CancellationToken ct = default)
        {
            return await db.Characters.AsNoTracking().ToListAsync(ct);
        }

        public async Task<Character?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await db.Characters.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id, ct);
        }

        public async Task<Character?> UpdateAsync(int id, Character character, CancellationToken ct = default)
        {
            var existingCharacter = await db.Characters.FindAsync([id], ct);
            if (existingCharacter == null)
            {
                return null;
            }

            existingCharacter.Name = character.Name;
            existingCharacter.Game = character.Game;
            existingCharacter.Role = character.Role;

            await db.SaveChangesAsync(ct);
            return existingCharacter;
        }
    }
}