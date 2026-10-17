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

        // update feature 

        //Id of the animal that is loaded in the form for editing , If Null form is in add mode
        private string _editingAnimalId = null;

        // Loads the animal into input boxes and switches form to edit mode 
        //add is disabled and save changes is enabledd

        private void EnterEditMode(Animal animal)
        {
            txtAnimalId.Text = animal.AnimalId;
            txtName.Text = animal.Name;
            txtSpecies.Text = animal.Species;
            txtAge.Text = animal.Age.ToString();
            txtRecoveryScore.Text = animal.RecoveryScore.ToString();
            txtAnimalId.ReadOnly = true; // Animal ID should not be changed
            btnAdd.Enabled = false;
            btnSaveChanges.Enabled = true;
            _editingAnimalId = animal.AnimalId;
            txtName.Focus(); // Put cursor in name  

        }

        // enables add mode and disabbles save changes and clears bboxes 

        private void EnterAddMode()
        {

            txtAnimalId.ReadOnly = false;
            btnAdd.Enabled = true;
            btnSaveChanges.Enabled = false;
            _editingAnimalId = null;
            txtSearch.Clear();
            dgvAnimals.ClearSelection();
            ClearInputs();
        }
        // cancels edit and goes back to addd mode 
        private void btnClear_Click(object sender, EventArgs e)
        {
            EnterAddMode();

        }

        // loads clicked animal into the form for editing

        private void dgvAnimals_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return; // when coloum header is clicked and not the animal row 
            }

            // The animal object behind the row , empty new row has none so we check for null and cast to Animal
            if (dgvAnimals.Rows[e.RowIndex].DataBoundItem is Animal animal)
            {
                EnterEditMode(animal);
            }

        }


        //Finds an animal by exact ID and loads for editing if found, otherwise shows message box
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchId = txtSearch.Text.Trim().ToUpperInvariant(); // removes spaces and makes it so that its not case sensitive 
            if (searchId.Length == 0)
            {
                MessageBox.Show("Please enter an Animal ID to search.", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSearch.Focus();
                return;
            }

            // find the animal in the list with its ID , null if there is no match

            Animal found = null;
            foreach (Animal a in _animals)
            {
                if (a.AnimalId == searchId)
                {
                    found = a;
                    break; // ids are unique so stop at first match
                }
            }
            if (found == null)
            {
                MessageBox.Show($"No animal with ID {searchId} was found. ", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            else
            {
                EnterEditMode(found);
            }


        }
        // saves the edited animal , and rewrites animals.txt so only the animals line changes
        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            if (_editingAnimalId == null)
            {
                MessageBox.Show("Select an animal to edit first.", "Nothing selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // same validation as add 

            if (!AnimalValidator.TryValidateName(txtName.Text, out string name, out string error))
            { ShowValidationError(error, txtName); return; }

            if (!AnimalValidator.TryValidateSpecies(txtSpecies.Text, out string species, out error))
            { ShowValidationError(error, txtSpecies); return; }

            if (!AnimalValidator.TryValidateAge(txtAge.Text, out int age, out error))
            { ShowValidationError(error, txtAge); return; }

            if (!AnimalValidator.TryValidateScore(txtRecoveryScore.Text, out int score, out error))
            { ShowValidationError(error, txtRecoveryScore); return; }

            // reload from the file so that the change is made to the latest version of the animal list and not an outdated one

            if (!_repository.TryLoadAll(out List<Animal> animals, out _, out error))
            {
                MessageBox.Show(error, "File Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // doesnt write the file after a failed load to avoid overwriting the file with an empty list
            }

            // find the animal that is being edited in the loaded list 
            Animal target = null;
            foreach (Animal a in animals)
            {
                if (a.AnimalId == _editingAnimalId)
                {
                    target = a;
                    break; // stop at first match  
                }
            }
            // if the animal is not found in the file , it may have been deleted by another instance of the program or manually
            if (target == null)
            {
                MessageBox.Show($"{_editingAnimalId} is no longer in animals.txt. It may have been deleted.", "Animal Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LoadAnimals(showSkippedMessage: false); // refresh the grid with the latest data from the file
                EnterAddMode();
                return;
            }

            //Save the new values and work out the status and housing unit again

            target.Name = name;
            target.Species = species;
            target.Age = age;
            target.RecoveryScore = score;
            target.ApplyClassification();

            if (!_repository.TryRewriteAll(animals, out error))
            {
                MessageBox.Show(error, "File Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // stay in edit mode 
            }

            MessageBox.Show($"{target.Name} ({target.AnimalId}) was updated.\n\n Status: {target.Status}\nHousing Unit: {target.HousingUnit}",
                "Animal Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadAnimals(showSkippedMessage: false);
            EnterAddMode();
        }

        // delete feature 
        // delete the selected animal after a confirmation and rewrites the animals.txt file without the deleted animal
        private void btnDelete_Click(object sender, EventArgs e)
        {
            // nothing selected or empty row selected
            if (dgvAnimals.SelectedCells.Count == 0 || dgvAnimals.CurrentRow == null || dgvAnimals.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Please select an animal in the grid to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // read the id and name from selected row
            DataGridViewRow row = dgvAnimals.CurrentRow;
            string deleteId = row.Cells[0].Value.ToString();
            string deleteName = row.Cells[1].Value.ToString();

            // confirmation dialog
            DialogResult answer = MessageBox.Show($"Delete {deleteId} ({deleteName})? This cannot be undone.", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (answer != DialogResult.Yes)
            {
                return; // user cancelled
            }

            // reload from the file so that the change is made to the latest version of the animal list and not an outdated one

            if (!_repository.TryLoadAll(out List<Animal> animals, out _, out string error))
            {
                MessageBox.Show(error, "File Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // doesnt write the file after a failed load to avoid overwriting the file with an empty list
            }

            // Find the animal to delete by ID

            Animal target = null;
            foreach (Animal a in animals)
            {
                if (a.AnimalId == deleteId)
                {
                    target = a;
                    break; // stop at first match  
                }
            }

            if (target == null)
            {
                MessageBox.Show($"{deleteId} is no longer in animals.txt. It may have been deleted.", "Animal Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LoadAnimals(showSkippedMessage: false); // refresh the grid with the latest data from the file
                EnterAddMode();
                return;
            }

            animals.Remove(target); // remove the animal from the list


            if (!_repository.TryRewriteAll(animals, out error))
            {
                MessageBox.Show(error, "File Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // file is unchanged, so the animal is still there
            }

            // regresh the from the file 
            LoadAnimals(showSkippedMessage: false);

            if (_editingAnimalId == deleteId)
            {
                EnterAddMode(); // if the deleted animal was being edited , go back to add mode

            }
        }

        // Feature 5: calculate totals from animals.txt, show them on the form, and save summary.txt
        private void btnGenerateSummary_Click(object sender, EventArgs e)
        {
            if (!_repository.TryLoadAll(out List<Animal> animals, out _, out string error))
            {
                MessageBox.Show(error, "File Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int total = animals.Count;
            int ageSum = 0;
            int scoreSum = 0;
            int critical = 0;
            int serious = 0;
            int stable = 0;
            int recovering = 0;
            int releaseReady = 0;

            foreach (Animal a in animals)
            {
                ageSum += a.Age;
                scoreSum += a.RecoveryScore;

                if (a.Status == "Critical") critical++;
                else if (a.Status == "Serious") serious++;
                else if (a.Status == "Stable") stable++;
                else if (a.Status == "Recovering") recovering++;
                else if (a.Status == "Release-Ready") releaseReady++;
            }

            // empty file: do not divide by zero
            string avgAgeText = "N/A";
            string avgScoreText = "N/A";
            if (total > 0)
            {
                avgAgeText = ((double)ageSum / total).ToString("0.00");
                avgScoreText = ((double)scoreSum / total).ToString("0.00");
            }

            txtTotalAnimals.Text = total.ToString();
            txtAverageAge.Text = avgAgeText;
            txtAverageScore.Text = avgScoreText;
            txtCountCritical.Text = critical.ToString();
            txtCountSerious.Text = serious.ToString();
            txtCountStable.Text = stable.ToString();
            txtCountRecovering.Text = recovering.ToString();
            txtCountReleaseReady.Text = releaseReady.ToString();

            string report =
                "Total animals: " + total + Environment.NewLine +
                "Average age: " + avgAgeText + Environment.NewLine +
                "Average recovery score: " + avgScoreText + Environment.NewLine +
                "Critical: " + critical + Environment.NewLine +
                "Serious: " + serious + Environment.NewLine +
                "Stable: " + stable + Environment.NewLine +
                "Recovering: " + recovering + Environment.NewLine +
                "Release-Ready: " + releaseReady + Environment.NewLine;

            if (!_repository.TryWriteSummary(report, out error))
            {
                MessageBox.Show(error, "File Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}

