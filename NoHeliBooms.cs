using Oxide.Core;
using Oxide.Core.Plugins;
using UnityEngine;

namespace Oxide.Plugins
{
    [Info("NoHeliBooms", "Hazmad", "1.0.0")]
    [Description("Prevents flying vehicles from exploding when decayed or destroyed.")]
    public class NoHeliBooms : RustPlugin
    {
        private void OnServerInitialized()
        {
            foreach (var entity in UnityEngine.Object.FindObjectsOfType<ScrapTransportHelicopter>())
            {
                if (entity == null)
                    continue;
                OnEntitySpawned(entity);
            }

            foreach (var entity in UnityEngine.Object.FindObjectsOfType<Minicopter>())
            {
                if (entity == null)
                    continue;
                OnEntitySpawned(entity);
            }

            foreach (var entity in UnityEngine.Object.FindObjectsOfType<AttackHelicopter>())
            {
                if (entity == null)
                    continue;
                OnEntitySpawned(entity);
            }
        }

        private void OnEntitySpawned(Minicopter entity)
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
        private void OnEntitySpawned(AttackHelicopter entity)
        {
            entity.explosionEffect.guid = null;
            entity.fireBall.guid = null;
            entity.serverGibs.guid = null;    
         }
    }
}
