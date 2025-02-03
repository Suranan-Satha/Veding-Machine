using vending_machine;

namespace veding_machine
{
    public partial class Form1 : Form
    {
        MenuItem itemBlackCoffee = new MenuItem();
        MenuItem itemLatte = new MenuItem();
        MenuItem itemMocha = new MenuItem();
        MenuItem itemChocolate = new MenuItem();
        MenuItem itemWater = new MenuItem();
        MenuItem itemCoffee = new MenuItem();
        MenuItem itemMilk = new MenuItem();
        MenuItem itemChocolatMix = new MenuItem();

        public Form1()
        {
            InitializeComponent();

            itemBlackCoffee.Name = "Black Coffee";
            itemBlackCoffee.Price = 50;
            itemBlackCoffee.Quantity = 0;
            itemBlackCoffee.Ingredients.Add("Water", 300);
            itemBlackCoffee.Ingredients.Add("Coffee", 20);

            itemLatte.Name = "Latte";
            itemLatte.Price = 60;
            itemLatte.Quantity = 0;
            itemLatte.Ingredients.Add("Water", 300);
            itemLatte.Ingredients.Add("Coffee", 20);
            itemLatte.Ingredients.Add("Milk", 10);

            itemMocha.Name = "Mocha";
            itemMocha.Price = 65;
            itemMocha.Quantity = 0;
            itemMocha.Ingredients.Add("Water", 300);
            itemMocha.Ingredients.Add("Coffee", 20);
            itemMocha.Ingredients.Add("Chocolate", 10);

            itemChocolate.Name = "Chocolate";
            itemChocolate.Price = 70;
            itemChocolate.Quantity = 0;
            itemChocolate.Ingredients.Add("Water", 300);
            itemChocolate.Ingredients.Add("Chocolate", 20);

            
            tb_black_coffee_price.Text = itemBlackCoffee.Price.ToString();
            tb_black_coffee_quantity.Text = itemBlackCoffee.Quantity.ToString();

            tb_latte_price.Text = itemLatte.Price.ToString();
            tb_latte_quantity.Text = itemLatte.Quantity.ToString();

            tb_mocha_price.Text = itemMocha.Price.ToString();
            tb_mocha_quantity.Text = itemMocha.Quantity.ToString();

            tb_chocolat_price.Text = itemChocolate.Price.ToString();
            tb_chocolat_quantity.Text = itemChocolate.Quantity.ToString();

            itemWater.Name = "Water Mix";
            itemWater.Quantity = 10000;

            itemCoffee.Name = "Coffee Mix";
            itemCoffee.Quantity = 10000;

            itemMilk.Name = "Milk Mix";
            itemMilk.Quantity = 10000;

            itemChocolatMix.Name = "Chocolat Mix";
            itemChocolatMix.Quantity = 10000;

            tb_water_mix.Text = itemWater.Quantity.ToString();
            tb_coffee_mix.Text = itemCoffee.Quantity.ToString();
            tb_milk_mix.Text = itemMilk.Quantity.ToString();
            tb_chocolat_mix.Text = itemChocolatMix.Quantity.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double dCash = double.Parse(tb_crash.Text);
                double dTotal = 0;

                Dictionary<string, Ingredient> availableIngredients = new Dictionary<string, Ingredient>
                {
                    { "Water", new Ingredient("Water", 10000) },
                    { "Coffee", new Ingredient("Coffee", 10000) },
                    { "Milk", new Ingredient("Milk", 10000) },
                    { "Chocolate", new Ingredient("Chocolate", 10000) }
                };

                if (chb_blackcoffee.Checked)
                {
                    itemBlackCoffee.Quantity = int.Parse(tb_black_coffee_quantity.Text);
                    dTotal += itemBlackCoffee.GetTotalPrice();
                    itemBlackCoffee.UseIngredients(availableIngredients);

                    
                    tb_water_mix.Text = availableIngredients["Water"].Quantity.ToString();
                    tb_coffee_mix.Text = availableIngredients["Coffee"].Quantity.ToString();
                }

               
                if (chb_latte.Checked)
                {
                    itemLatte.Quantity = int.Parse(tb_latte_quantity.Text);
                    dTotal += itemLatte.GetTotalPrice();
                    itemLatte.UseIngredients(availableIngredients);

                    tb_water_mix.Text = availableIngredients["Water"].Quantity.ToString();
                    tb_coffee_mix.Text = availableIngredients["Coffee"].Quantity.ToString();
                    tb_milk_mix.Text = availableIngredients["Milk"].Quantity.ToString();
                }

                if (chb_mocha.Checked)
                {
                    itemMocha.Quantity = int.Parse(tb_mocha_quantity.Text);
                    dTotal += itemMocha.GetTotalPrice();
                    itemMocha.UseIngredients(availableIngredients);

                    tb_water_mix.Text = availableIngredients["Water"].Quantity.ToString();
                    tb_coffee_mix.Text = availableIngredients["Coffee"].Quantity.ToString();
                    tb_chocolat_mix.Text = availableIngredients["Chocolate"].Quantity.ToString();
                }

                if (chb_chocolat.Checked)
                {
                    itemChocolate.Quantity = int.Parse(tb_chocolat_quantity.Text);
                    dTotal += itemChocolate.GetTotalPrice();
                    itemChocolate.UseIngredients(availableIngredients);

                    tb_water_mix.Text = availableIngredients["Water"].Quantity.ToString();
                    tb_chocolat_mix.Text = availableIngredients["Chocolate"].Quantity.ToString();
                }

                if (dCash < dTotal)
                {
                    MessageBox.Show("Insufficient cash", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                if (chb_blackcoffee.Checked)
                {
                    itemBlackCoffee.Quantity = int.Parse(tb_black_coffee_quantity.Text);
                    dTotal += itemBlackCoffee.GetTotalPrice();

                    try
                    {
                        itemBlackCoffee.UseIngredients(availableIngredients);
                        tb_water_mix.Text = availableIngredients["Water"].Quantity.ToString();
                        tb_coffee_mix.Text = availableIngredients["Coffee"].Quantity.ToString();
                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                double dChange = dCash - dTotal;
                tb_total.Text = dTotal.ToString("F2");
                tb_change.Text = dChange.ToString("F2");

                CalculateChangeDenominations(dChange);
            }
            catch (FormatException)
            {
                MessageBox.Show("Please fill in the numbers correctly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculateChangeDenominations(double change)
        {
            double[] denominations = { 10, 5, 1, 0.50, 0.25 };
            int[] changeCount = new int[denominations.Length];
            double remainChange = change;

            for (int i = 0; i < denominations.Length; i++)
            {
                changeCount[i] = (int)(remainChange / denominations[i]);
                remainChange %= denominations[i];
            }

            tb_10.Text = changeCount[0].ToString();
            tb_5.Text = changeCount[1].ToString();
            tb_1.Text = changeCount[2].ToString();
            tb_05.Text = changeCount[3].ToString();
            tb_025.Text = changeCount[4].ToString();
        }
    }
}
