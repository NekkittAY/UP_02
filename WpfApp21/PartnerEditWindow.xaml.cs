using System;
using System.Windows;

namespace WpfApp21
{
    public partial class PartnerEditWindow : Window
    {
        private Partners _partner;
        private MaterialDefectEntities _dbContext;

        public PartnerEditWindow(MaterialDefectEntities dbContext, Partners partner = null)
        {
            InitializeComponent();
            _dbContext = dbContext;
            _partner = partner;

            if (_partner != null)
            {
                // Заполняем поля для редактирования
                PartnerNameTextBox.Text = _partner.PartnerName;
                PartnerTypeComboBox.SelectedItem = _partner.PartnerType;
                RatingTextBox.Text = _partner.Rating.ToString();
                AddressTextBox.Text = _partner.LegalAddress;
                DirectorTextBox.Text = _partner.Director;
                PhoneTextBox.Text = _partner.Phone;
                EmailTextBox.Text = _partner.Email;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Валидация данных
                if (string.IsNullOrWhiteSpace(PartnerNameTextBox.Text) ||
                    string.IsNullOrWhiteSpace(RatingTextBox.Text) ||
                    !int.TryParse(RatingTextBox.Text, out int rating) || rating < 0)
                {
                    MessageBox.Show("Введите корректные данные. Рейтинг должен быть неотрицательным числом.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Создание или обновление партнёра
                if (_partner == null)
                {
                    _partner = new Partners();
                    _dbContext.Partners.Add(_partner);
                }

                _partner.PartnerName = PartnerNameTextBox.Text;
                _partner.PartnerType = PartnerTypeComboBox.Text;
                _partner.Rating = rating;
                _partner.LegalAddress = AddressTextBox.Text;
                _partner.Director = DirectorTextBox.Text;
                _partner.Phone = PhoneTextBox.Text;
                _partner.Email = EmailTextBox.Text;

                _dbContext.SaveChanges();
                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
