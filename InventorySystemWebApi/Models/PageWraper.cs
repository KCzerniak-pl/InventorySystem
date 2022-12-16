namespace InventorySystemWebApi.Models
{
    public class PageWraper<T>
    {
        public IEnumerable<T> Items { get; set; }

        public int ItemsCount { get; set; }

        public PageWraper(IEnumerable<T> items, int itemsCount)
        {
            Items = items;
            ItemsCount = itemsCount;
        }
    }
}
