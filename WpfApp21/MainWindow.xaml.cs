using System.Linq;
using System.Windows;
using System;


namespace WpfApp21
{
    public partial class MainWindow : Window
    {
        private MaterialDefectEntities dbContext;

        public MainWindow()
        {
            InitializeComponent();
            dbContext = new MaterialDefectEntities();
            LoadPartners();
        }

        private void LoadPartners()
        {
            try
            {
                var partners = dbContext.Partners.Select(p => new
                {
                    PartnerType = p.PartnerType,
                    PartnerName = p.PartnerName,
                    Director = p.Director,
                    Rating = p.Rating
                }).ToList();

                PartnersDataGrid.ItemsSource = partners;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }

        private void CalculateDiscountButton_Click(object sender, RoutedEventArgs e)
        {
            if (PartnersDataGrid.SelectedItem != null)
            {
                dynamic selectedPartner = PartnersDataGrid.SelectedItem;
                int rating = selectedPartner.Rating;
                double discount = CalculateIndividualDiscount(rating);
                MessageBox.Show($"Индивидуальная скидка для партнера \"{selectedPartner.PartnerName}\": {discount:F2}%", "Скидка");
            }
            else
            {
                MessageBox.Show("Выберите партнера из списка.");
            }
        }

        private double CalculateIndividualDiscount(int rating)
        {
            if (rating >= 10) return 20.0; // Высокий рейтинг
            if (rating >= 5) return 10.0;  // Средний рейтинг
            return 5.0;                    // Низкий рейтинг
        }

        private void AddPartnerButton_Click(object sender, RoutedEventArgs e)
        {
            var partnerEditWindow = new PartnerEditWindow(dbContext);
            if (partnerEditWindow.ShowDialog() == true)
            {
                LoadPartners(); 
            }
        }

        private void EditPartnerButton_Click(object sender, RoutedEventArgs e)
        {
            if (PartnersDataGrid.SelectedItem is Partners selectedPartner)

            {
                var partnerEditWindow = new PartnerEditWindow(dbContext, selectedPartner);
                if (partnerEditWindow.ShowDialog() == true)
                {
                    LoadPartners();
                }
            }
            else
            {
                MessageBox.Show("Выберите партнёра для редактирования.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void OpenPartnerEditWindowButton_Click(object sender, RoutedEventArgs e)
        {
            var partnerEditWindow = new PartnerEditWindow(dbContext);
            if (partnerEditWindow.ShowDialog() == true)
            {
                LoadPartners(); 
            }
        }

    }
}
