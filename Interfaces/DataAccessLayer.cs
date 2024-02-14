namespace ExtremeRecycler.Interfaces
{
	public interface DataAccessLayer<T>
	{
		public List<T> GetAll();
		public T Get(int id);
		public bool Remove(int id);
		public bool Add(T item);
		public void Update(T item);
	}
}
