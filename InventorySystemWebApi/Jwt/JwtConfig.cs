namespace InventorySystemWebApi.Jwt
{
    public class JwtConfig
    {
        public string Secret { get; set; } = default!;
        public int ExpireHours { get; set; }
    }
}
