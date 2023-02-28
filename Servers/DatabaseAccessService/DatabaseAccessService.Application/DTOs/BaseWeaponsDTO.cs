using CommonData.PlayerSendData;

namespace DatabaseAccessService.Application.DTOs {
    public record BaseWeaponsDTO {
        public List<BaseWeapon> BaseWeapons { get; set; } = new();
    }
}