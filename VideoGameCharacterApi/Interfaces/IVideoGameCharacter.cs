using VideoGameCharacterApi.Models;

namespace VideoGameCharacterApi.Interfaces
{
    public interface IVideoGameCharacter
    {
        Task<IReadOnlyList<Character>> GetAllAsync(CancellationToken ct = default);
        Task<Character?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<Character> CreateAsync(Character character, CancellationToken ct = default);
        Task<Character?> UpdateAsync(int id, Character character, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}
