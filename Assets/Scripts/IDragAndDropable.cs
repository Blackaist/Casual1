namespace Project
{
	public interface IDragAndDropable : IDrag, IDropable
	{
		public bool IsDragging { get; }
	}

	public interface IDrag
	{
		void Drag();
	}

	public interface IDropable
	{
		void Drop();
	}
}
