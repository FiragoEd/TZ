public class SpawnMobMessage : Message
{
	public const int MELEE = 0;
	public const int RANGE = 1;
	public const int MAGIC = 2;
	
	public int Type;
}