using System;

namespace CommonData.Entities {

    public class EquippedSkinsTuple : Tuple<int, int> {

        public EquippedSkinsTuple(int item1, int item2) : base(item1, item2) {
        }

        public int CharacterId { get { return this.Item1; } }
        public int SkinId { get { return this.Item2; } }
    }
}