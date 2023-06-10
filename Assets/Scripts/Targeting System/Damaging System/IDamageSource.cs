public interface IDamageSource : IStatsHolder
{
	static System.Action<IDamageSource, IDamageable, float, bool> OnGlobalDamageDealt;
	System.Action<IDamageable, float, bool> OnDamageDealt { get; }
}
