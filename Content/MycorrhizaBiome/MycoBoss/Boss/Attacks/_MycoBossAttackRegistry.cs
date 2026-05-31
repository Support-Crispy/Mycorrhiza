using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mycorrhiza.Content.MycorrhizaBiome.MycoBoss.Boss.Attacks
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    namespace Mycorrhiza.Content.MycorrhizaBiome.MycoBoss.Boss.Attacks
    {
        internal static class _MycoBossAttackRegistry
        {
            private static Dictionary<MycoBoss_State, Type>? _attackTypes;

            public static IReadOnlyDictionary<MycoBoss_State, Type> AttackTypes
            {
                get
                {
                    _attackTypes ??= LoadAttackTypes();
                    return _attackTypes;
                }
            }

            private static Dictionary<MycoBoss_State, Type> LoadAttackTypes()
            {
                Dictionary<MycoBoss_State, Type> attacks = new();

                Type baseType = typeof(_MycoBoss_Attack);
                Assembly assembly = baseType.Assembly;

                foreach (Type type in assembly.GetTypes())
                {
                    if (type.IsAbstract || type.IsInterface)
                        continue;

                    if (!baseType.IsAssignableFrom(type))
                        continue;

                    if (Activator.CreateInstance(type) is not _MycoBoss_Attack attack)
                        continue;

                    if (attacks.ContainsKey(attack.ID))
                        throw new Exception($"Duplicate MycoBoss attack registered for state {attack.ID}: {type.FullName}");

                    attacks.Add(attack.ID, type);
                }

                return attacks;
            }

            public static _MycoBoss_Attack Create(MycoBoss_State state)
            {
                if (!AttackTypes.TryGetValue(state, out Type type))
                    throw new KeyNotFoundException($"No MycoBoss attack is registered for state {state}");

                if (Activator.CreateInstance(type) is not _MycoBoss_Attack attack)
                    throw new Exception($"Failed to create MycoBoss attack instance for state {state}");

                return attack;
            }
        }
    }
}
