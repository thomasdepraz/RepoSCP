namespace SCP.Data
{
    public class SCPModel
    {
        public string ID { get; private set; }
        public string Name { get; private set; }
        public DangerLevel DangerLevel { get; private set; }
        public SCPType  Type { get; private set; }
        public Rarity Rarity { get; private set; }
        public int Size { get; private set; }

        public SCPModel(SCPData data)
        {
            ID = data.ID;
            Name = data.name;
            DangerLevel = data.dangerLevel;
            Type = data.type;
            Rarity = data.rarity;
            Size = data.size;
        }
    }

}
