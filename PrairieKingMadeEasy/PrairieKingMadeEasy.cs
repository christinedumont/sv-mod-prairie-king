using System.Reflection;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace PrairieKingMadeEasy
{
    public class PrairieKingMadeEasy : Mod
    {
        private const BindingFlags FieldFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

        private ModConfig Config;

        public override void Entry(IModHelper helper)
        {
            this.Config = helper.ReadConfig<ModConfig>();
            helper.Events.GameLoop.UpdateTicked += this.OnUpdateTicked;
        }

        private void OnUpdateTicked(object sender, UpdateTickedEventArgs e)
        {
            var minigame = Game1.currentMinigame;
            if (minigame == null || minigame.GetType().Name != "AbigailGame")
                return;

            var type = minigame.GetType();

            if (this.Config.infiniteLives)
                type.GetField("lives", FieldFlags)?.SetValue(minigame, 99);

            if (this.Config.infiniteCoins)
                type.GetField("coins", FieldFlags)?.SetValue(minigame, 99);

            if (this.Config.rapidFire)
                type.GetField("shootingDelay", FieldFlags)?.SetValue(minigame, 25);

            if (this.Config.alwaysInvincible)
                type.GetField("playerInvincibleTimer", FieldFlags)?.SetValue(minigame, 5000);
        }
    }
}
