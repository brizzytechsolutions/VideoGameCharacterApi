using Microsoft.AspNetCore.Mvc;
using VideoGameCharacterApi.DTO;
using VideoGameCharacterApi.Interfaces;
using VideoGameCharacterApi.Models;

namespace VideoGameCharacterApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VideoCharacterController(IVideoGameCharacter characters) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<CharacterResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<CharacterResponseDto>>> GetAllCharacters(CancellationToken ct)
    {
        var result = await characters.GetAllAsync(ct);
        return Ok(result.Select(ToResponse).ToList());
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CharacterResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CharacterResponseDto>> GetCharacterById(int id, CancellationToken ct)
    {
        var result = await characters.GetByIdAsync(id, ct);
        return result is null ? NotFound() : Ok(ToResponse(result));
    }

    [HttpPost]
    [ProducesResponseType(typeof(CharacterResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CharacterResponseDto>> CreateCharacter(
        [FromBody] CharacterRequestDto request,
        CancellationToken ct)
    {
        var created = await characters.CreateAsync(ToEntity(request), ct);
        var response = ToResponse(created);
        return CreatedAtAction(nameof(GetCharacterById), new { id = response.Id }, response);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(CharacterResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CharacterResponseDto>> UpdateCharacter(
        int id,
        [FromBody] CharacterRequestDto request,
        CancellationToken ct)
    {
        var updated = await characters.UpdateAsync(id, ToEntity(request), ct);
        return updated is null ? NotFound() : Ok(ToResponse(updated));
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCharacter(int id, CancellationToken ct)
    {
        var deleted = await characters.DeleteAsync(id, ct);
        return deleted ? NoContent() : NotFound();
    }

    private static CharacterResponseDto ToResponse(Character character) => new()
    {
        Id = character.Id,
        Name = character.Name,
        Game = character.Game,
        Role = character.Role
    };

    private static Character ToEntity(CharacterRequestDto request) => new()
    {
        Name = request.Name,
        Game = request.Game,
        Role = request.Role
    };
}
