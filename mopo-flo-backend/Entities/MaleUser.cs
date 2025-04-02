namespace violet.backend.Entities
{
    public sealed class MaleUser : User
    {
        public MaleProfile MaleProfile { get; set; } = new();
    }
}
