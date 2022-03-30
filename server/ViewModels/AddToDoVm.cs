namespace JustDoIt.ViewModels
{
    public class AddToDoVm
    {
        public string Name { get; set; } = "";
        public int Priority { get; set; }
        public string DueDate { get; set; } = "";
        public string DueTime { get; set; } = "";
        public bool Status { get; set; }
    }
}