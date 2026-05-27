using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mycorrhiza.Content.MycorrhizaBiome.MycoBoss.Boss.Attacks
{
    internal static class MycoBossAttackRegistry
    {
        private static Dictionary<MycoBoss_State, MycoBoss_Attack> _attacks;

        public static IReadOnlyDictionary<MycoBoss_State, MycoBoss_Attack> Attacks
        {
            get
            {
                _attacks ??= LoadAttacks();
                return _attacks;
            }
        }

        private static Dictionary<MycoBoss_State, MycoBoss_Attack> LoadAttacks()
        {
            Dictionary<MycoBoss_State, MycoBoss_Attack> attacks = new();

            Type baseType = typeof(MycoBoss_Attack);
            Assembly assembly = baseType.Assembly;

            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsAbstract || type.IsInterface)
                    continue;

                if (!baseType.IsAssignableFrom(type))
                    continue;

                if (Activator.CreateInstance(type) is not MycoBoss_Attack attack)
                    continue;

                if (attacks.ContainsKey(attack.ID))
                    throw new Exception($"Duplicate SolarKing attack registered for state {attack.ID}: {type.FullName}");

                attacks.Add(attack.ID, attack);
            }

            return attacks;
        }

        public static MycoBoss_Attack Get(MycoBoss_State state)
        {
            if (!Attacks.TryGetValue(state, out MycoBoss_Attack attack))
                throw new KeyNotFoundException($"No SolarKingAttack is registered for state {state}");

            return attack;
        }
    }
}
