namespace AncientMysteries.Items
{
    public interface IAMEquipment
    {
        public bool CanCrush { get; set; }

        public bool Destroyable { get; set; }

        public bool KnockOffOnHit { get; set; }

        public bool MakeDefaultHitEffects { get; set; }

        public bool BulletThroughNotEquipped { get; set; }

        public float EquipmentMaxHitPoints { get; set; }

        public float EquipmentHitPoints { get; set; }
    }
}