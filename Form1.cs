using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaobabRidge
{

    internal partial class Form1 : Form
    {
        private readonly AnimalRepository _repository = new AnimalRepository();
        private List<Animal> _animals = new List<Animal>();
        public Form1()
        {
            InitializeComponent();
        }
        private void AddGridColumn(string header, string propertyName)
        {
            dgvAnimals.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = header,
                DataPropertyName = propertyName,
                SortMode = DataGridViewColumnSortMode.NotSortable // keeps file order
            });
        }

        private void ConfigureGrid()
        {
            dgvAnimals.AutoGenerateColumns = false;
            dgvAnimals.Columns.Clear();
            AddGridColumn("Animal ID", nameof(Animal.AnimalId));
            AddGridColumn("Name", nameof(Animal.Name));
            AddGridColumn("Species", nameof(Animal.Species));
            AddGridColumn("Age", nameof(Animal.Age));
            AddGridColumn("Recovery Score", nameof(Animal.RecoveryScore));
            AddGridColumn("Status", nameof(Animal.Status));
            AddGridColumn("Housing Unit", nameof(Animal.HousingUnit));

            dgvAnimals.ReadOnly = true;
            dgvAnimals.AllowUserToAddRows = false;
            dgvAnimals.AllowUserToDeleteRows = false;
            dgvAnimals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAnimals.MultiSelect = false; 
            dgvAnimals.RowHeadersVisible = false;
            dgvAnimals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void LoadAnimals(bool showSkippedMessage)
        {
            if (!_repository.TryLoadAll(out List<Animal> loaded, out int skipped, out string error))
            {
                MessageBox.Show(error, "File Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _animals = loaded;
            RefreshGrid();

            if (showSkippedMessage && skipped > 0)
                MessageBox.Show($"{skipped} malformed line(s) in animals.txt were skipped.",
                    "Some Records Skipped", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void RefreshGrid()
        {
            dgvAnimals.DataSource = null;
            dgvAnimals.DataSource = new BindingList<Animal>(_animals);
            dgvAnimals.ClearSelection();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!AnimalValidator.TryValidateId(txtAnimalId.Text, out string id, out string error))
            { ShowValidationError(error, txtAnimalId); return; }

            if (_animals.Any(a => a.AnimalId == id))
            { ShowValidationError("Animal ID already exists. Each Animal ID must be unique.", txtAnimalId); return; }

            if (!AnimalValidator.TryValidateName(txtName.Text, out string name, out error))
            { ShowValidationError(error, txtName); return; }

            if (!AnimalValidator.TryValidateSpecies(txtSpecies.Text, out string species, out error))
            { ShowValidationError(error, txtSpecies); return; }

            if (!AnimalValidator.TryValidateAge(txtAge.Text, out int age, out error))
            { ShowValidationError(error, txtAge); return; }

            if (!AnimalValidator.TryValidateScore(txtRecoveryScore.Text, out int score, out error))
            { ShowValidationError(error, txtRecoveryScore); return; }

            var animal = new Animal { AnimalId = id, Name = name, Species = species, Age = age, RecoveryScore = score };
            animal.ApplyClassification();

            if (!_repository.TryAppend(animal, out error))
            {
                MessageBox.Show(error, "File Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }

            _animals.Add(animal);
            RefreshGrid();
            MessageBox.Show($"{animal.Name} ({animal.AnimalId}) was added.\n\nStatus: {animal.Status}\nHousing Unit: {animal.HousingUnit}",
                "Animal Added", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ClearInputs();
        }
        private void ShowValidationError(string message, TextBox field)
        {
            MessageBox.Show(message, "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            field.Focus();
            field.SelectAll();
        }
        private void ClearInputs()
        {
            txtAnimalId.Clear();
            txtName.Clear();
            txtSpecies.Clear();
            txtAge.Clear();
            txtRecoveryScore.Clear();
            txtAnimalId.Focus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadAnimals(showSkippedMessage: true);
        }
    }
}
