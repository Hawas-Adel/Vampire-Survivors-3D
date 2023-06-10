public interface IDamageSource : IStatsHolder
{
	static System.Action<IDamageSource, IDamageable, float> OnGlobalDamageDealt { get; }
	System.Action<IDamageable, float> OnDamageDealt { get; }
}
