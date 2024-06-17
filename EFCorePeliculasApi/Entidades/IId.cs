namespace EFCorePeliculasApi.Entidades
{
	/*
	 se crea esta interface, porque automapper, no trabajo con
	ObservableCollection
	 */
	public interface IId
	{
        public int Id { get; set; }
    }
}
