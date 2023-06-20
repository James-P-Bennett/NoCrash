using Oxide.Core;
using Oxide.Core.Plugins;
using UnityEngine;

namespace Oxide.Plugins
{
    [Info("NoCrash", "Hazmad", "1.0.0")]
    [Description("Prevents minicopters and scrap helicopters from exploding when players are mounted.")]
    public class NoCrash : RustPlugin
    {
        private void OnServerInitialized()
        {
            foreach (var entity in UnityEngine.Object.FindObjectsOfType<ScrapTransportHelicopter>())
            {
                if (entity == null)
                    continue;
                OnEntitySpawned(entity);
            }

            foreach (var entity in UnityEngine.Object.FindObjectsOfType<MiniCopter>())
            {
                if (entity == null)
                    continue;
                OnEntitySpawned(entity);
            }
        }

        private void OnEntitySpawned(MiniCopter entity)
        {
            entity.explosionEffect.guid = null;
            entity.fireBall.guid = null;
        }

        private void OnEntitySpawned(ScrapTransportHelicopter entity)
        {
            entity.explosionEffect.guid = null;
            entity.fireBall.guid = null;
            entity.serverGibs.guid = null;
        }

        private void OnEntityTakeDamage(BaseCombatEntity entity, HitInfo hitInfo)
        {
            if (entity is BaseHelicopter || entity is ScrapTransportHelicopter)
            {
                if (entity.health <= 1f)
                {
                    var players = BasePlayer.activePlayerList;
                    foreach (var player in players)
                    {
                        if (player.GetMountedVehicle() == entity)
                        {
                            player.EndLooting();
                            player.transform.position += player.transform.right; // Move the player slightly to the side
                            PrintToChat(player, "You have been ejected from the vehicle before it was destroyed.");
                        }
                    }
                }
            }
        }
    }
}
