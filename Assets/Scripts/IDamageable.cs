public interface IDamageable
{
	event DamageEvent OnTakeDamage;
	void TakeDamage(int damage);
	void Kill();
}