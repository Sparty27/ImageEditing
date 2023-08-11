namespace ImageEditingBL
{
    public class Image
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public bool isCompleted { get; private set; }

        public Image(int id, string name)
        {
            Id=id;
            Name=name;
        }

        //Represents work
        public void MakeCompleted()
        {
            isCompleted = true;
        }
    }
}