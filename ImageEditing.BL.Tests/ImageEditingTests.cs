using ImageEditingBL;

namespace ImageEditing.BL.Tests
{
    [TestClass]
    public class ImageEditingTests
    {
        private List<Image> images = new List<Image>();
        private List<ImageEditor> editors = new List<ImageEditor>();
        private EditingManager manager;

        [TestInitialize]
        public void TestInitialize()
        {
            int amountImages = 1000;
            for (int i = 1; i <= amountImages; i++)
            {
                images.Add(new Image(i, "Test"));
            }

            editors.Add(new ImageEditor("Person 1", 2));
            editors.Add(new ImageEditor("Person 2", 3));
            editors.Add(new ImageEditor("Person 3", 4));


            manager = new(images, editors);
        }

        [TestMethod]
        public void CalculateTotalTime_923returned()
        {
            // Arrange

            // Act
            int actual = manager.CalculateTotalTime();

            // Assert
            Assert.AreEqual(923, actual);
        }

        [TestMethod]
        public void isAllImagesCompleted_false()
        {
            // Arrange

            // Act
            bool actual = manager.isAllImagesCompleted();

            // Assert
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void isAllImagesCompleted_true()
        {
            // Arrange

            // Act
            foreach (var image in images)
            {
                image.MakeCompleted();
            }

            bool actual = manager.isAllImagesCompleted();

            // Assert
            Assert.AreEqual(true, actual);
        }
    }
}