using System.ComponentModel.DataAnnotations;

namespace VideoGameCharacterApi.DTO;

public class CharacterRequestDto
{
    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string Game { get; set; } = string.Empty;

    [Required, MaxLength(50)]
    public string Role { get; set; } = string.Empty;
}
