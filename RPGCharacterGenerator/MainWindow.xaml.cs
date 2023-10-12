using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
namespace RPGCharacterGenerator;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>


public partial class MainWindow : Window
{
    List<Player> _allcharacters = new();

    int _currentId = 1;

    List<string> classes = new()
    {
        "Fighter",
        "Wizard",
    };

    public MainWindow()
    {

        InitializeComponent();

        //Lägg till våra classes strängar i comboboxen
        cbRole.ItemsSource = classes;

    }

    private void cbRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Kolla om vi har aselectat Fighter eller Wizard
        string selectedRole = (string)cbRole.SelectedItem;

        // Byt Content i lablen för speciel stat till rätt namn
        if (selectedRole == "Fighter")
        {
            lblArmorMana.Content = "Armor";
        }
        else if (selectedRole == "Wizard")
        {
            lblArmorMana.Content = "Mana";
        }
        // Visa labeln för speciell stat och dess inputruta

        lblArmorMana.Visibility = Visibility.Visible;
        txtArmorMana.Visibility = Visibility.Visible;
    }

    private void btnRollAbilityScore_Click(object sender, RoutedEventArgs e)
    {
        // Autogenerera strength
        // Set strength

        txtStrength.Text = GenerateStat().ToString();

        // Autogenerera intelligence
        // Set Intelligence

        txtIQ.Text = GenerateStat().ToString();
    }

    private int GenerateStat()
    {
        return new Random().Next(3, 19);
    }

    private bool ValidateStringConvertion(string stringToValidate)
    {
        bool validateOk = int.TryParse(stringToValidate, out int result);

        return validateOk;
    }

    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
        string strength = txtStrength.Text;
        string intelligence = txtIQ.Text;
        string name = txtName.Text;
        string armorMana = txtArmorMana.Text;
        string role = (string)cbRole.SelectedItem;

        if (strength != "" && intelligence != "" && name != "" && armorMana != "" && role != "" && ValidateStringConvertion(armorMana))
        {
            // Lägg till karaktär

            lstPlayers.Items.Add(new
            {
                Id = _currentId,
                Name = name,
                Strength = strength,
                Intelligence = intelligence,
                Role = role,
                Ability = armorMana
            });

            // Lägg till karaktären i "databasen"

            if (role == "Fighter")
            {
                // Gör en ny fighter

                Fighter newFighter = new(_currentId, name, int.Parse(strength), int.Parse(intelligence), int.Parse(armorMana));

                _allcharacters.Add(newFighter);
            }
            else if (role == "Wizard")
            {
                // Gör en ny wizard

                Wizard newWizard = new(_currentId, name, int.Parse(strength), int.Parse(intelligence), int.Parse(armorMana));

                _allcharacters.Add(newWizard);
            }

            _currentId++;

            txtName.Clear();
            txtArmorMana.Clear();
            txtStrength.Clear();
            txtIQ.Clear();
            cbRole.SelectedIndex = -1;
            lblArmorMana.Visibility = Visibility.Hidden;
            txtArmorMana.Visibility = Visibility.Hidden;
        }
        else
        {
            // Visa varning!
            MessageBox.Show("Please add all the required information in the correct format!", "Warning");
        }
    }
}









//ListBox players = new();

//List<Player> existingPlayers = new List<Player>()
//{
//    new Fighter ("Elias", 3000, 7500, 8000),
//    new Wizard ("Oliver", 1, 1, 100)
//};

//foreach (Player player in existingPlayers)
//{
//    ListBoxItem item = new();
//    item.Content = player.GetInfo();
//    item.Tag = player;

//    lbPlayers.Items.Add(item);
//}