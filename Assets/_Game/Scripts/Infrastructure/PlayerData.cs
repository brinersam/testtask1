namespace Game.Infrastructure
{
    public class PlayerData
    {
        public Entity Entity { get; }
        //public Deck Deck { get; }

        public PlayerData(Entity entity)//, Deck deck)
        {
            Entity = entity;
            //Deck = deck;
        }
    }
}