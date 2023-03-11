namespace InventorySystemWebApi.Models
{
    public class PageWrapper<T>
    {
        public IEnumerable<T> Items { get; set; }

        public int ItemsCount { get; set; }

        public PageWrapper(IEnumerable<T> items, int itemsCount)
        {
            Items = items;
            ItemsCount = itemsCount;
        }
    }
}
