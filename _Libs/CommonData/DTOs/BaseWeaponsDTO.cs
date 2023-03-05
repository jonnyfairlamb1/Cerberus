using CommonData.PlayerSendData;
using System.Collections.Generic;

namespace CommonData.DTOs {
    public record BaseWeaponsDTO {
        public List<BaseWeapon> BaseWeapons { get; set; } = new();
    }
}