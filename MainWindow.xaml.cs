using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pr9
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Warrior currentWarrior;
        private Image warriorImage;
        private int currentImageIndex = 0;
        private List<string> lightWarriorImages = new List<string>();
        private List<string> heavyWarriorImages = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            InitializeImages();
        }

        private void InitializeImages()
        {
            lightWarriorImages.Add("C:/Users/student-a502/Desktop/pr9/img/l.jpg");
            lightWarriorImages.Add("C:/Users/student-a502/Desktop/pr9/img/l2.jpg"); 
            lightWarriorImages.Add("C:/Users/student-a502/Desktop/pr9/img/l3.jpg"); 

            heavyWarriorImages.Add("C:/Users/student-a502/Desktop/pr9/img/t.jpg");
            heavyWarriorImages.Add("C:/Users/student-a502/Desktop/pr9/img/t2.jpg"); 
            heavyWarriorImages.Add("C:/Users/student-a502/Desktop/pr9/img/t3.jpg");
        }

        private void btnCreateLightWarrior_Click(object sender, RoutedEventArgs e)
        {
            currentWarrior = new LightArmorWarrior();
            currentImageIndex = 0;
            CreateWarriorVisual(lightWarriorImages[currentImageIndex]);
            UpdateStatus();
        }

        private void btnCreateHeavyWarrior_Click(object sender, RoutedEventArgs e)
        {
            currentWarrior = new HeavyArmorWarrior();
            currentImageIndex = 0;
            CreateWarriorVisual(heavyWarriorImages[currentImageIndex]);
            UpdateStatus();
        }

        private void btnAttack_Click(object sender, RoutedEventArgs e)
        {
            if (currentWarrior != null)
            {
                currentWarrior.TakeDamage(10);
                UpdateStatus();
                UpdateWarriorOpacity();

                if (currentWarrior.Health <= 0)
                {
                    MessageBox.Show("Воин погиб!", "Битва окончена", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Сначала создайте воина!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CreateWarriorVisual(string imagePath)
        {
            warriorCanvas.Children.Clear();

            try
            {
                warriorImage = new Image
                {
                    Width = 200,
                    Height = 250,
                    Source = new BitmapImage(new Uri(imagePath)),
                    Stretch = Stretch.Uniform,
                    Cursor = Cursors.Hand
                };

                warriorImage.MouseDown += WarriorImage_MouseDown;

                Canvas.SetLeft(warriorImage, warriorCanvas.ActualWidth / 2 - 100);
                Canvas.SetTop(warriorImage, warriorCanvas.ActualHeight / 2 - 125);

                warriorCanvas.Children.Add(warriorImage);
            }
            catch (Exception ex)
            {
                CreateFallbackVisual();
            }
        }
        private void WarriorImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (currentWarrior != null && warriorImage != null)
            {
                ChangeWarriorImage();
            }
        }

        private void ChangeWarriorImage()
        {
            if (currentWarrior is LightArmorWarrior)
            {
                currentImageIndex = (currentImageIndex + 1) % lightWarriorImages.Count;
                warriorImage.Source = new BitmapImage(new Uri(lightWarriorImages[currentImageIndex]));
            }
            else if (currentWarrior is HeavyArmorWarrior)
            {
                currentImageIndex = (currentImageIndex + 1) % heavyWarriorImages.Count;
                warriorImage.Source = new BitmapImage(new Uri(heavyWarriorImages[currentImageIndex]));
            }
        }

        private void CreateFallbackVisual()
        {
            warriorCanvas.Children.Clear();
            TextBlock fallbackText = new TextBlock
            {
                Text = currentWarrior is LightArmorWarrior ? " Легкий Воин " : " Тяжелый Воин ",
                FontSize = 24,
                FontWeight = FontWeights.Bold,
                Foreground = currentWarrior is LightArmorWarrior ? Brushes.Green : Brushes.DarkBlue,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Cursor = Cursors.Hand
            };
            fallbackText.MouseDown += (s, e) => 
            {
                if (currentWarrior != null)
                {
                    if (currentWarrior is LightArmorWarrior && lightWarriorImages.Count > 0)
                    {
                        CreateWarriorVisual(lightWarriorImages[currentImageIndex]);
                    }
                    else if (currentWarrior is HeavyArmorWarrior && heavyWarriorImages.Count > 0)
                    {
                        CreateWarriorVisual(heavyWarriorImages[currentImageIndex]);
                    }
                }
            };

            Canvas.SetLeft(fallbackText, warriorCanvas.ActualWidth / 2 - 100);
            Canvas.SetTop(fallbackText, warriorCanvas.ActualHeight / 2 - 15);

            warriorCanvas.Children.Add(fallbackText);
        }

        private void UpdateStatus()
        {
            if (currentWarrior != null)
            {
                string armorType = currentWarrior is LightArmorWarrior ? "легких" : "тяжелых";
                string imageInfo = $" | Изображение: {currentImageIndex + 1}/{(currentWarrior is LightArmorWarrior ? lightWarriorImages.Count : heavyWarriorImages.Count)}";
                tbStatus.Text = $"Воин в {armorType} доспехах | Здоровье: {currentWarrior.Health:F1} | Состояние: {(currentWarrior.Health > 0 ? "Жив" : "Мертв")}{imageInfo}";
            }
        }

        private void UpdateWarriorOpacity()
        {
            if (warriorImage != null && currentWarrior != null)
            {
                double healthPercentage = currentWarrior.Health / 100.0;
                warriorImage.Opacity = healthPercentage;
            }
        }
    }
}